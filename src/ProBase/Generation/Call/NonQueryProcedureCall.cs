using ProBase.Data;
using System;
using System.Reflection.Emit;

namespace ProBase.Generation.Call
{
    /// <summary>
    /// Generates calls for non query procedures.
    /// </summary>
    internal class NonQueryProcedureCall : IProcedureCall
    {
        /// <summary>
        /// Generates a non query procedure call.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="resultType">The result type of the method</param>
        /// <param name="generator">The generator to be used for code generation</param>
        public void Call(string procedureName, Type resultType, ILGenerator generator)
        {
            // Call the method
            generator.Emit(OpCodes.Callvirt, GeneratedClass.GetMethod<IProcedureMapper>(MethodName));
        }

        private const string MethodName = nameof(IProcedureMapper.ExecuteNonQueryProcedure);
    }
}
