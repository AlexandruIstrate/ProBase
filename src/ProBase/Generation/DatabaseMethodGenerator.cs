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
    /// Generates a method body used for calling the database procedures.
    /// </summary>
    internal class DatabaseMethodGenerator : IMethodGenerator
    {
        public DatabaseMethodGenerator(FieldInfo databaseField)
        {
            this.databaseField = databaseField;
        }

        public MethodBuilder GenerateMethod(MethodInfo methodInfo, TypeBuilder typeBuilder)
        {
            ProcedureAttribute procedureAttribute = methodInfo.GetCustomAttribute<ProcedureAttribute>();
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Public, methodInfo.ReturnType, GetParameterTypes(methodInfo.GetParameters()));

            GenerateMethodBody(procedureAttribute.ProcedureName, methodInfo.GetParameters(), methodBuilder.ReturnType, methodBuilder.GetILGenerator());

            return methodBuilder;
        }

        private Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            return parameters.ToList().ConvertAll(item => item.ParameterType).ToArray();
        }

        private void GenerateMethodBody(string procedureName, ParameterInfo[] parameters, Type returnType, ILGenerator generator)
        {
            generator.Emit(OpCodes.Ldarg_0);

            // Load the field we use for calling the database procedures
            generator.Emit(OpCodes.Ldfld, databaseField);

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
            Type mapperType = typeof(IDatabaseMapper);
            IEnumerable<MethodInfo> matchingReturnType = mapperType.GetMethods().Where(method => method.ReturnType == returnType);

            if (matchingReturnType.Count() == 0)
            {
                return mapperType.GetMethods().Where(method => method.IsGenericMethod).First();
            }

            return matchingReturnType.First();
        }

        private readonly FieldInfo databaseField;
    }
}
