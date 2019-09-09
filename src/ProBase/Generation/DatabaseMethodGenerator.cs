using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    internal class DatabaseMethodGenerator : IMethodGenerator
    {
        public MethodBuilder GenerateMethod(string methodName, ParameterInfo[] parameters, Type returnType, TypeBuilder typeBuilder)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, MethodAttributes.Public, returnType, GetParameterTypes(parameters));
            GenerateMethodBody(methodBuilder.GetILGenerator());
            return methodBuilder;
        }

        private Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            return parameters.ToList().ConvertAll(item => item.ParameterType).ToArray();
        }

        private void GenerateMethodBody(ILGenerator iLGenerator)
        {

        }
    }
}
