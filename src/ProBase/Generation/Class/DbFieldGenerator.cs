using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Class
{
    /// <summary>
    /// Provides a way for generating fields inside a database access class.
    /// </summary>
    internal class DbFieldGenerator : IClassFieldGenerator
    {
        /// <summary>
        /// Generates the field inside the datab
        /// </summary>
        /// <param name="fieldName">The name of the field</param>
        /// <param name="fieldType">The type of the field</param>
        /// <param name="typeBuilder">A type builder used for generating the field</param>
        /// <returns>A builder that represents the field</returns>
        public FieldBuilder GenerateField(string fieldName, Type fieldType, TypeBuilder typeBuilder)
        {
            return typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Private | FieldAttributes.InitOnly);
        }
    }
}
