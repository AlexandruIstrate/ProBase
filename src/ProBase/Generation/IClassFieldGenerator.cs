using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    /// <summary>
    /// Defines an operation for generating a class field with the given options.
    /// </summary>
    internal interface IClassFieldGenerator
    {
        /// <summary>
        /// Generates a field with the info given, using the <paramref name="typeBuilder"/>.
        /// </summary>
        /// <param name="fieldInfo">Information for generating thsi field</param>
        /// <param name="typeBuilder">The TypeBuilder to use</param>
        /// <returns>A <see cref="System.Reflection.FieldInfo"/> representing the field</returns>
        FieldBuilder GenerateField(FieldInfo fieldInfo, TypeBuilder typeBuilder);
    }
}
