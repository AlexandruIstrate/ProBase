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
        /// <param name="methodName">The name of the generated method</param>
        /// <param name="parameters">The parameters of the generated method</param>
        /// <param name="returnType">The return type of the generated method</param>
        /// <param name="typeBuilder">The type to generate the method for</param>
        /// <returns>A <see cref="System.Reflection.Emit.MethodBuilder"/> representing the method</returns>
        MethodBuilder GenerateMethod(string methodName, ParameterInfo[] parameters, Type returnType, TypeBuilder typeBuilder);
    }
}
