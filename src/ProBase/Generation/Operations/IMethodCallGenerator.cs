using ProBase.Attributes;
using System;
using System.Reflection.Emit;

namespace ProBase.Generation.Operations
{
    /// <summary>
    /// Provides a way for generating method calls.
    /// </summary>
    internal interface IMethodCallGenerator
    {
        /// <summary>
        /// Generates a method call based on the return type of the procedure.
        /// </summary>
        /// <param name="resultType">The type the method should return</param>
        /// <param name="procedureType">The type of the procedure</param>
        /// <param name="generator">The generator used for code generation</param>
        void Generate(Type resultType, ProcedureType procedureType, ILGenerator generator);
    }
}
