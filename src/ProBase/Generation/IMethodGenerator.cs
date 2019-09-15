using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Defines an operation for generating a method with the given options.
    /// </summary>
    internal interface IMethodGenerator
    {
        /// <summary>
        /// Generates a method using a signature defined by the parameters with the given <paramref name="typeBuilder"/>.
        /// </summary>
        /// <param name="methodInfo">The metadata required for generating this method</param>
        /// <param name="typeBuilder">The type to generate the method for</param>
        /// <returns>A <see cref="System.Reflection.Emit.MethodBuilder"/> representing the method</returns>
        MethodBuilder GenerateMethod(MethodInfo methodInfo, TypeBuilder typeBuilder);
    }
}
