using System;
using System.Reflection.Emit;

namespace ProBase.Generation.Class
{
    /// <summary>
    /// Defines an operation for generating a class field with the given options.
    /// </summary>
    internal interface IClassFieldGenerator
    {
        /// <summary>
        /// Generates a field with the info given, using the <paramref name="typeBuilder"/>.
        /// </summary>
        /// <param name="fieldName">The name of the field</param>
        /// <param name="fieldType">The type of the field</param>
        /// <param name="typeBuilder">The TypeBuilder to use</param>
        /// <returns>A <see cref="System.Reflection.FieldInfo"/> representing the field</returns>
        FieldBuilder GenerateField(string fieldName, Type fieldType, TypeBuilder typeBuilder);
    }
}
