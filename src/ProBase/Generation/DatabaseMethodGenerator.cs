using ProBase.Attributes;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
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

            GenerateMethodBody(procedureAttribute.ProcedureName, methodInfo.GetParameters(), methodBuilder.GetILGenerator());

            return methodBuilder;
        }

        private Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            return parameters.ToList().ConvertAll(item => item.ParameterType).ToArray();
        }

        private void GenerateMethodBody(string procedureName, ParameterInfo[] parameters, ILGenerator generator)
        {
            throw new NotImplementedException();
        }

        private readonly FieldInfo databaseField;
    }
}
