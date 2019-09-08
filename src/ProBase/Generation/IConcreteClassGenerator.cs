using System;

namespace ProBase.Generation
{
    /// <summary>
    /// Defines an operation for generating a concrete class implementing an interface.
    /// </summary>
    internal interface IConcreteClassGenerator
    {
        /// <summary>
        /// Generates a concrete type implementing the given interface.
        /// </summary>
        /// <param name="interfaceType">The interface to implement</param>
        /// <returns>A <see cref="System.Type"/> implementing the given interface</returns>
        Type GenerateClassImplementingInterface(Type interfaceType);
    }
}
