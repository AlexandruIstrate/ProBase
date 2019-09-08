using System;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Defines an operation for generating a field with the given options.
    /// </summary>
    internal interface IClassFieldGenerator
    {
        /// <summary>
        /// Generates a field with the given <paramref name="fieldName"/> and <paramref name="type"/>, optionally giving it a <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="fieldName">The name of the field to generate</param>
        /// <param name="type">The type of the field to generate</param>
        /// <param name="typeBuilder">The type to generate the field for</param>
        /// <param name="defaultValue">An optional default value</param>
        /// <returns>A <see cref="System.Reflection.Emit.FieldBuilder"/> representing the field</returns>
        FieldBuilder GenerateField(string fieldName, Type type, TypeBuilder typeBuilder, object defaultValue = null);
    }
}
