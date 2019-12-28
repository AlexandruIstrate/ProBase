using ProBase.Attributes;
using ProBase.Data;
using ProBase.Generation.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Provides a way for generating methods used for calling database procedures.
    /// </summary>
    internal class DbMethodGenerator : IMethodGenerator
    {
        /// <summary>
        /// Creates an instance using the given <see cref="ProBase.Generation.Operations.IArrayGenerator"/> for generating the parameter array.
        /// </summary>
        /// <param name="arrayGenerator">The array generator to use</param>
        public DbMethodGenerator(IArrayGenerator arrayGenerator, IMethodCallGenerator procedureCallGenerator)
        {
            this.arrayGenerator = arrayGenerator;
            this.procedureCallGenerator = procedureCallGenerator;
        }

        /// <summary>
        /// Generates a method used for calling the database procedures.
        /// </summary>
        /// <param name="methodInfo">The method information</param>
        /// <param name="classFields">The fields this method has access to</param>
        /// <param name="typeBuilder">A type builder used for generating the method</param>
        /// <returns>A builder representing the method</returns>
        public MethodBuilder GenerateMethod(MethodInfo methodInfo, FieldInfo[] classFields, TypeBuilder typeBuilder)
        {
            ProcedureAttribute procedureAttribute = methodInfo.GetCustomAttribute<ProcedureAttribute>();
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Public | MethodAttributes.Virtual, methodInfo.ReturnType, GetParameterTypes(methodInfo.GetParameters()));

            if (procedureAttribute.ProcedureType == ProcedureType.Automatic)
            {
                // If the procedure type is Automatic, then let the generator figure out its type.
                GenerateMethodBody(
                    procedureAttribute.ProcedureName,
                    methodInfo.GetParameters(),
                    methodBuilder.ReturnType,
                    classFields,
                    methodBuilder.GetILGenerator());
            }
            else
            {
                // Otherwise, pass in the procedure type
                GenerateMethodBody(
                    procedureAttribute.ProcedureName,
                    methodInfo.GetParameters(),
                    methodBuilder.ReturnType,
                    procedureAttribute.ProcedureType,
                    classFields,
                    methodBuilder.GetILGenerator());
            }

            return methodBuilder;
        }

        private Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            return parameters.ToList().ConvertAll(item => item.ParameterType).ToArray();
        }

        private void GenerateMethodBody(string procedureName, ParameterInfo[] parameters, Type returnType, FieldInfo[] fields, ILGenerator generator)
        {
            PrepareParameters(procedureName, parameters, fields, generator);

            // Generate the procedure call
            procedureCallGenerator.Generate(returnType, generator);

            ReturnFromMethod(returnType, generator);
        }

        private void GenerateMethodBody(string procedureName, ParameterInfo[] parameters, Type returnType, ProcedureType procedureType, FieldInfo[] fields, ILGenerator generator)
        {
            PrepareParameters(procedureName, parameters, fields, generator);

            // Generate the procedure call
            procedureCallGenerator.Generate(procedureType, generator);

            ReturnFromMethod(returnType, generator);
        }

        private static void ReturnFromMethod(Type returnType, ILGenerator generator)
        {
            // If the method returns a value, then pop it from the stack after the call
            if (returnType != typeof(void))
            {
                generator.Emit(OpCodes.Pop);
            }

            // Return from the method
            generator.Emit(OpCodes.Ret);
        }

        private void PrepareParameters(string procedureName, ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator)
        {
            // Generate the parameter array
            arrayGenerator.Generate(parameters, generator);

            // Load this object
            generator.Emit(OpCodes.Ldarg_0);

            // Load the field we use for calling the database procedure
            generator.Emit(OpCodes.Ldfld, GetField<IProcedureMapper>(GenerationConstants.ProcedureMapperFieldName, fields));

            // Load the procedure name
            generator.Emit(OpCodes.Ldstr, procedureName);

            // Load the parameter array as a local
            generator.Emit(OpCodes.Ldloc, parameters.Length);
        }

        private FieldInfo GetField<T>(string fieldName, IEnumerable<FieldInfo> fields)
        {
            IEnumerable<FieldInfo> typedFields = fields.Where(field => field.FieldType == typeof(T));

            if (typedFields.Count() == 0)
            {
                throw new CodeGenerationException($"The generated class does not contain any fields of type { nameof(T) }");
            }

            FieldInfo namedField = typedFields.ToList().Find(field => field.Name == fieldName);

            if (namedField == null)
            {
                throw new CodeGenerationException($"Could not find a field with name { fieldName }");
            }

            return namedField;
        }

        private readonly IArrayGenerator arrayGenerator;
        private readonly IMethodCallGenerator procedureCallGenerator;
    }
}
