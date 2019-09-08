using System;
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
        /// <typeparam name="T">The return type of the method</typeparam>
        /// <param name="methodName">The name of the method to generate</param>
        /// <param name="parameters">The parameters of the method to generate</param>
        /// <param name="methodBody">The code that will be invoked when the method is called</param>
        /// <param name="typeBuilder">The type to generate the method for</param>
        /// <returns>A <see cref="System.Reflection.Emit.MethodBuilder"/> representing the method</returns>
        MethodBuilder GenerateMethod<T>(string methodName, ParameterInfo[] parameters, Func<IReflectionContext, T> methodBody, TypeBuilder typeBuilder);
    }
}
