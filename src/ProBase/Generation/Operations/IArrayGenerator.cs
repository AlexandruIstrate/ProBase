using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Operations
{
    /// <summary>
    /// Provides a way of generating an array based on method parameters.
    /// </summary>
    internal interface IArrayGenerator
    {
        /// <summary>
        /// Generates an array.
        /// </summary>
        /// <param name="parameters">The parameters passed into the method</param>
        /// <param name="fields">The fields of the generated class</param>
        /// <param name="generator">The generator to use for generating MSIL instructions</param>
        /// <returns>The local builder associated with the array</returns>
        LocalBuilder Generate(ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator);
    }
}
