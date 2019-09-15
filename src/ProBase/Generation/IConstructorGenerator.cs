using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Generates a constructor that initializes the given fields with the given values.
    /// </summary>
    internal interface IConstructorGenerator
    {
        /// <summary>
        /// Generates a default, initialization constructor and initializes the fields with the given values.
        /// </summary>
        /// <param name="fieldValues">A dictionary that maps a field to a value</param>
        /// <param name="typeBuilder">The TypeBuilder to use</param>
        /// <returns>A builder representing the constructor</returns>
        ConstructorBuilder GenerateDefaultConstructor(IDictionary<FieldInfo, ValueType> fieldValues, TypeBuilder typeBuilder);

        /// <summary>
        /// Generates a dependency inverted constructor that initializes all of the given fields using the constructor arguments.
        /// </summary>
        /// <param name="fields">An array of the fields that need initialization</param>
        /// <param name="typeBuilder">The TypeBuilder to use</param>
        /// <returns>A builder representing the constructor</returns>
        ConstructorBuilder GenerateDependencyConstructor(FieldInfo[] fields, TypeBuilder typeBuilder);
    }
}
