using ProBase.Attributes;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Provides a way for generating method calls.
    /// </summary>
    internal interface IMethodCallGenerator
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
        void Generate(string procedureName, Type resultType, ProcedureType procedureType, LocalBuilder parameterArray, FieldInfo[] fields, ILGenerator generator);
    }
}
