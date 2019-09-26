using ProBase.Attributes;
using ProBase.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Provides a way for generating methods used for calling database procedures.
    /// </summary>
    internal class DatabaseMethodGenerator : IMethodGenerator
    {
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

            GenerateMethodBody(procedureAttribute.ProcedureName, methodInfo.GetParameters(), methodBuilder.ReturnType, classFields, methodBuilder.GetILGenerator());

            return methodBuilder;
        }

        private Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            return parameters.ToList().ConvertAll(item => item.ParameterType).ToArray();
        }

        private void GenerateMethodBody(string procedureName, ParameterInfo[] parameters, Type returnType, FieldInfo[] fields, ILGenerator generator)
        {
            generator.Emit(OpCodes.Ldarg_0);

            // Load the field we use for calling the database procedures
            generator.Emit(OpCodes.Ldfld, GetField<IProcedureMapper>(GenerationConstants.ProcedureMapperFieldName, fields));

            // Load the procedure name
            generator.Emit(OpCodes.Ldstr, procedureName);
            generator.Emit(OpCodes.Ldc_I4, 0);

            // Create an array of parameters
            generator.Emit(OpCodes.Newarr, typeof(DbParameter));

            // Call the procedure using the coresponding virtual method
            generator.Emit(OpCodes.Callvirt, GetDataMapperMethod(returnType));

            // Return from the method
            generator.Emit(OpCodes.Ret);
        }

        private MethodInfo GetDataMapperMethod(Type returnType)
        {
            Type mapperType = typeof(IProcedureMapper);
            IEnumerable<MethodInfo> matchingReturnType = mapperType.GetMethods().Where(method => method.ReturnType == returnType);

            if (matchingReturnType.Count() == 0)
            {
                return mapperType.GetMethods().Where(method => method.IsGenericMethod).First();
            }

            return matchingReturnType.First();
        }

        private FieldInfo GetField<T>(string fieldName, IEnumerable<FieldInfo> fields)
        {
            IEnumerable<FieldInfo> typedFields = fields.Where(field => field.FieldType == typeof(T));

            if (typedFields.Count() == 0)
            {
                throw new CodeGenerationException($"The generated class does not contain a field of type { nameof(T) }");
            }

            // Realistically, there can only be one field with this name
            IEnumerable<FieldInfo> namedFields = typedFields.Where(field => field.Name == fieldName);

            if (namedFields.Count() == 0)
            {
                throw new CodeGenerationException($"Cannot find a field named { fieldName }");
            }

            if (namedFields.Count() > 1)
            {
                throw new CodeGenerationException($"Multiple fields with the name { fieldName } found");
            }

            return namedFields.First();
        }
    }
}
