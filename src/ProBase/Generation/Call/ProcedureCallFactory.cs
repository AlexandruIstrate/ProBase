using ProBase.Attributes;
using ProBase.Data;
using ProBase.Utils;
using System;
using System.Data;
using System.Reflection;

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
                    return new ProcedureCall(GetNonQuerryMethod());
                default:
                    throw new NotSupportedException("This procedure type is not supported");
            }
        }

        private static IProcedureCall CreateFromReturnType(Type returnType)
        {
            // If we have a Task as return type, then we have an async method
            if (returnType.IsTask())
            {
                return new AsyncProcedureCall();
            }

            // For an int, we have an non-query call
            if (returnType == typeof(int))
            {
                return new ProcedureCall(GetNonQuerryMethod());
            }

            // For a DataSet, we have an unmapped scalar call
            if (returnType == typeof(DataSet))
            {
                return new MappedProcedureCall();
            }

            // For void, assume we have a non-query
            if (returnType == typeof(void))
            {
                return new ProcedureCall(GetNonQuerryMethod());
            }

            // If we have any other type, then it must be mapped
            return new MappedProcedureCall();
        }

        private static MethodInfo GetNonQuerryMethod()
        {
            return ClassUtils.GetMethod<IProcedureMapper>(nameof(IProcedureMapper.ExecuteNonQueryProcedure));
        }
    }
}
