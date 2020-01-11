using ProBase.Attributes;
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
        /// Generates a method call based on the return type of the procedure.
        /// </summary>
        /// <param name="resultType">The type the method should return</param>
        /// <param name="procedureType">The type of the procedure</param>
        /// <param name="generator">The generator used for code generation</param>
        public void Generate(Type resultType, ProcedureType procedureType, ILGenerator generator)
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

            // If the method returns an int, then it's meant to be a non-query procedure
            if (returnType == typeof(int))
            {
                return GetMethod(nameof(IProcedureMapper.ExecuteNonQueryProcedure));
            }

            // If the method returns a DataSet, then it's meant to be a scalar procedure
            if (returnType == typeof(DataSet))
            {
                return GetMethod(nameof(IProcedureMapper.ExecuteScalarProcedure));
            }

            // If the return type is a custom type, then it must be mapped from a DataSet
            return GetMethod(nameof(IProcedureMapper.ExecuteMappedProcedure));
        }

        private MethodInfo GetMethod(string methodName)
        {
            MethodInfo methodInfo = GetMapperType().GetMethod(methodName);

            if (methodInfo == null)
            {
                // If the method was not found on the mapper, then use the non-mapper database procedure
                return GetMapperType().GetInterface(nameof(IDatabase)).GetMethod(methodName);
            }

            return methodInfo;
        }

        private Type GetMapperType() => typeof(IProcedureMapper);
    }
}
