using ProBase.Attributes;
using ProBase.Data;
using ProBase.Utils;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace ProBase.Generation.Call
{
    /// <summary>
    /// Aids in the creation of <see cref="ProBase.Generation.Call.IProcedureCall"/> objects.
    /// </summary>
    internal class ProcedureCallFactory
    {
        /// <summary>
        /// Creates an object of type <see cref="ProBase.Generation.Call.IProcedureCall"/> objects.
        /// </summary>
        /// <param name="procedureType">The type of the procedure</param>
        /// <param name="returnType">The return type of the method</param>
        /// <returns>A generated object</returns>
        public static IProcedureCall Create(ProcedureType procedureType, Type returnType)
        {
            switch (procedureType)
            {
                case ProcedureType.Automatic:
                    return CreateFromReturnType(returnType);
                case ProcedureType.Scalar:
                    return new MappedProcedureCall();
                case ProcedureType.NonQuery:
                    return new ProcedureCall(GeneratedClass.GetMethod<IProcedureMapper>(nameof(IProcedureMapper.ExecuteNonQueryProcedure)));
                default:
                    throw new NotSupportedException("The given procedure type is not supported");
            }
        }

        private static IProcedureCall CreateFromReturnType(Type returnType)
        {
            // For an int, we have an non-query call
            if (returnType == typeof(int))
            {
                return new ProcedureCall(GeneratedClass.GetMethod<IProcedureMapper>(nameof(IProcedureMapper.ExecuteNonQueryProcedure)));
            }

            // For a DataSet, we have an unmapped scalar call
            if (returnType == typeof(DataSet))
            {
                return new MappedProcedureCall();
            }

            // For void, assume we have a non-query
            if (returnType == typeof(void))
            {
                return new ProcedureCall(GeneratedClass.GetMethod<IProcedureMapper>(nameof(IProcedureMapper.ExecuteNonQueryProcedure)));
            }

            if (returnType.IsTask())
            {
                return new ProcedureCall(GetAsyncMethod(returnType));
            }

            // If we have any other type, then it must be mapped
            return new MappedProcedureCall();
        }

        private static MethodInfo GetAsyncMethod(Type returnType)
        {
            // The default is a non-query
            string methodName = nameof(IProcedureMapper.ExecuteNonQueryProcedureAsync);

            if (returnType == typeof(Task<DataSet>))
            {
                methodName = nameof(IProcedureMapper.ExecuteScalarProcedureAsync);
            }

            return GeneratedClass.GetMethod<IProcedureMapper>(methodName);
        }
    }
}
