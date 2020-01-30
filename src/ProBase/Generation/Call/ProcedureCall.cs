using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Call
{
    /// <summary>
    /// Generates calls for non query procedures.
    /// </summary>
    internal class ProcedureCall : IProcedureCall
    {
        /// <summary>
        /// The method that calls the procedure.
        /// </summary>
        public MethodInfo ProcedureMethod { get; set; }

        /// <summary>
        /// Creates a new instance of this class using the supplied <see cref="System.Reflection.MethodInfo"/>.
        /// </summary>
        /// <param name="procedureMethod">The method to call</param>
        public ProcedureCall(MethodInfo procedureMethod)
        {
            ProcedureMethod = procedureMethod;
        }

        /// <summary>
        /// Generates a non query procedure call.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="resultType">The result type of the method</param>
        /// <param name="generator">The generator to be used for code generation</param>
        public void Call(string procedureName, Type resultType, ILGenerator generator)
        {
            // Call the method
            generator.Emit(OpCodes.Callvirt, ProcedureMethod);
        }
    }
}
