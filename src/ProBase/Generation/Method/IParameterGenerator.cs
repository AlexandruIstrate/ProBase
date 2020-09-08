using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Provides an operation for generating a set of <see cref="System.Data.Common.DbParameter"/> objects from a method parameter.
    /// </summary>
    internal interface IParameterGenerator
    {
        /// <summary>
        /// Generates a set of <see cref="System.Data.Common.DbParameter"/> based on a method parameter.
        /// </summary>
        /// <param name="parameter">The method parameter</param>
        /// <param name="providerFactory">A provider factory field for creating the parameter</param>
        /// <param name="generator">The generator to use</param>
        /// <returns>A set of database parameters</returns>
        LocalBuilder[] Generate(ParameterInfo parameter, FieldInfo providerFactory, ILGenerator generator);
    }
}
