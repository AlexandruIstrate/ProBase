using ProBase.Attributes;
using ProBase.Data;
using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    internal class DatabaseMethodGenerator : IMethodGenerator
    {
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
            generator.DeclareLocal(typeof(DbParameter));
            generator.DeclareLocal(typeof(IDatabase));

            generator.Emit(OpCodes.Ldc_I4_0);

            generator.Emit(OpCodes.Newarr);
        }
    }
}
