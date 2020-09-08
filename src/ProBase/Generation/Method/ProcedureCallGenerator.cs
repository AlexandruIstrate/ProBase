using ProBase.Attributes;
using ProBase.Generation.Call;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Handles the generation of a database procedure call.
    /// </summary>
    internal class ProcedureCallGenerator : IMethodCallGenerator
    {
        /// <summary>
        /// Generates a method call based on the return type of the procedure.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="resultType">The type the method should return</param>
        /// <param name="procedureType">The type of the procedure</param>
        /// <param name="parameterArray">The procedure parameters local</param>
        /// <param name="fields">The fields of the generated class</param>
        /// <param name="generator">The generator used for code generation</param>
        public void Generate(string procedureName, Type resultType, ProcedureType procedureType, LocalBuilder parameterArray, FieldInfo[] fields, ILGenerator generator)
        {
            IProcedureCall procedureCall = ProcedureCallFactory.Create(procedureType, resultType);
            procedureCall.Call(procedureName, resultType, generator);

            // If we don't return a value from this method, pop the result from the stack so we don't run into issues
            if (resultType == typeof(void))
            {
                generator.Emit(OpCodes.Pop);
            }
        }
    }
}
