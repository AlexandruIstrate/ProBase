using ProBase.Attributes;
using System;
using System.Data;

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
                    return new ScalarProcedureCall();
                case ProcedureType.NonQuery:
                    return new NonQueryProcedureCall();
                default:
                    throw new NotSupportedException("The given procedure type is not supported");
            }
        }

        private static IProcedureCall CreateFromReturnType(Type returnType)
        {
            // For an int, we have an non-query call
            if (returnType == typeof(int))
            {
                return new NonQueryProcedureCall();
            }

            // For a DataSet, we have an unmapped scalar call
            if (returnType == typeof(DataSet))
            {
                return new ScalarProcedureCall();
            }

            // For void, assume we have a non-query
            if (returnType == typeof(void))
            {
                return new NonQueryProcedureCall();
            }

            // If we have any other type, then it must be mapped
            return new ScalarProcedureCall();
        }
    }
}
