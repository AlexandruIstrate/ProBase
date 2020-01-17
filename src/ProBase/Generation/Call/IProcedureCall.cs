using System;
using System.Reflection.Emit;

namespace ProBase.Generation.Call
{
    /// <summary>
    /// Represents a class used for generating a procedure call.
    /// </summary>
    internal interface IProcedureCall
    {
        /// <summary>
        /// Generates a procedure call.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="resultType">The result type of the method</param>
        /// <param name="generator">The generator to be used for code generation</param>
        void Call(string procedureName, Type resultType, ILGenerator generator);
    }
}
