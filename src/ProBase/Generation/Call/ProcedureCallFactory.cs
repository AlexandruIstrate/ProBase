using ProBase.Attributes;
using System;

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
        /// <returns>A generated object</returns>
        public static IProcedureCall Create(ProcedureType procedureType)
        {
            switch (procedureType)
            {
                case ProcedureType.Automatic:
                    break;
                case ProcedureType.Scalar:
                    break;
                case ProcedureType.NonQuery:
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}
