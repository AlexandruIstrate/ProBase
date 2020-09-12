using ProBase.Attributes;
using ProBase.Data;
using ProBase.Generation.Converters;
using ProBase.Generation.Method;
using ProBase.Utils;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Class
{
    /// <summary>
    /// Provides a way for generating methods used for calling database procedures.
    /// </summary>
    internal class DbMethodGenerator : IMethodGenerator
    {
        /// <summary>
        /// Creates an instance using the given <see cref="ProBase.Generation.Method.ICollectionGenerator"/> for generating the parameter array.
        /// </summary>
        /// <param name="arrayGenerator">The array generator to use</param>
        /// <param name="procedureCallGenerator">The generator used for generating the procedure call</param>
        public DbMethodGenerator(ICollectionGenerator arrayGenerator, IMethodCallGenerator procedureCallGenerator)
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

            GenerateMethodBody(procedureAttribute.ProcedureName, methodInfo.GetParameters(), methodBuilder.ReturnType, procedureAttribute.ProcedureType, classFields, methodBuilder.GetILGenerator());

            return methodBuilder;
        }

        private Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            return parameters.ToList().ConvertAll(item => item.ParameterType).ToArray();
        }

        private void GenerateMethodBody(string procedureName, ParameterInfo[] parameters, Type returnType, ProcedureType procedureType, FieldInfo[] fields, ILGenerator generator)
        {
            ParameterCollection collection = PrepareParameters(procedureName, parameters, fields, generator);

            // Generate the procedure call
            procedureCallGenerator.Generate(procedureName, returnType, procedureType, collection.CollectionLocal, fields, generator);

            // Fill the out and ref parameters
            FillParameters(parameters, collection, generator);

            // Return from the method
            ReturnFromMethod(generator);
        }

        private void FillParameters(ParameterInfo[] parameters, ParameterCollection collection, ILGenerator generator)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];

                if (parameter.GetDbParameterDirection() == ParameterDirection.Input)
                {
                    continue;
                }

                LocalBuilder local = collection[parameter];

                IParameterFiller parameterFiller = ParameterFillerFactory.Create(parameter);
                parameterFiller.Fill(parameter, dbParameter: local.LocalIndex, generator);
            }
        }

        private static void ReturnFromMethod(ILGenerator generator)
        {
            // Return from the method
            generator.Emit(OpCodes.Ret);
        }

        private ParameterCollection PrepareParameters(string procedureName, ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator)
        {
            // Generate the parameter array
            ParameterCollection collection = arrayGenerator.Generate(parameters, fields, generator);

            // Load this object
            generator.Emit(OpCodes.Ldarg_0);

            // Load the field we use for calling the database procedure
            generator.Emit(OpCodes.Ldfld, ClassUtils.GetField<IProcedureMapper>(fields, GenerationConstants.ProcedureMapperFieldName));

            // Load the procedure name
            generator.Emit(OpCodes.Ldstr, procedureName);

            // Load the parameter array as a local
            generator.Emit(OpCodes.Ldloc, collection.CollectionLocal);

            return collection;
        }

        private readonly ICollectionGenerator arrayGenerator;
        private readonly IMethodCallGenerator procedureCallGenerator;
    }
}
