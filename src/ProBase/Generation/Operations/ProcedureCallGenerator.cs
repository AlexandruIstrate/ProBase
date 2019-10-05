using ProBase.Data;
using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Operations
{
    /// <summary>
    /// Handles the generation of a database procedure call.
    /// </summary>
    internal class ProcedureCallGenerator : IMethodCallGenerator
    {
        /// <summary>
        /// Generates the database procedure call.
        /// </summary>
        /// <param name="resultType">The result type of the method</param>
        /// <param name="generator">The generator to use</param>
        public void Generate(Type resultType, ILGenerator generator)
        {
            generator.Emit(OpCodes.Call, GetMapperMethod(resultType));
        }

        private MethodInfo GetMapperMethod(Type returnType)
        {
            // If the return type is void then execute a non-query procedure and discard the result
            if (returnType == typeof(void))
            {
                return GetMethod(nameof(IProcedureMapper.ExecuteNonQueryProcedure));
            }

            // If the method returns an int then it means its meant to be a non-query procedure
            if (returnType == typeof(int))
            {
                return GetMethod(nameof(IProcedureMapper.ExecuteNonQueryProcedure));
            }

            // If the method returns a DataSet then it means its meant to be a scalar procedure
            if (returnType == typeof(DataSet))
            {
                return GetMethod(nameof(IProcedureMapper.ExecuteScalarProcedure));
            }

            // If the return type is a custom type then it means it must be mapped from a DataSet
            return GetMethod(nameof(IProcedureMapper.ExecuteMappedProcedure));
        }

        private MethodInfo GetMethod(string methodName)
        {
            MethodInfo methodInfo = GetMapperType().GetMethod(methodName);

            if (methodInfo == null)
            {
                return GetMapperType().GetInterface(nameof(IDatabase)).GetMethod(methodName);
            }

            return methodInfo;
        }

        private Type GetMapperType() => typeof(IProcedureMapper);
    }
}
