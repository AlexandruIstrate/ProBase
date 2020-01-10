using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProBase.Generation
{
    /// <summary>
    /// Provides operations for working with generated classes.
    /// </summary>
    internal class GeneratedClass
    {
        /// <summary>
        /// Gets a field with a given type and name from a set of fields.
        /// </summary>
        /// <typeparam name="T">The type of the field</typeparam>
        /// <param name="fields">The fields to search in</param>
        /// <param name="fieldName">The name of the field</param>
        /// <returns>The field or null</returns>
        public static FieldInfo GetField<T>(IEnumerable<FieldInfo> fields, string fieldName)
        {
            IEnumerable<FieldInfo> typedFields = fields.Where(field => field.FieldType == typeof(T));

            if (typedFields.Count() == 0)
            {
                throw new CodeGenerationException($"The generated class does not contain any fields of type { nameof(T) }");
            }

            FieldInfo namedField = typedFields.ToList().Find(field => field.Name == fieldName);

            if (namedField == null)
            {
                throw new CodeGenerationException($"Could not find a field with name { fieldName }");
            }

            return namedField;
        }

        /// <summary>
        /// Gets the get method for a property of a class by name.
        /// </summary>
        /// <typeparam name="T">The class type</typeparam>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>The info for the get method</returns>
        public static MethodInfo GetPropertyGetMethod<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName).GetGetMethod();
        }

        /// <summary>
        /// Gets the set method for a property of a class by name.
        /// </summary>
        /// <typeparam name="T">The class type</typeparam>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>The info for the set method</returns>
        public static MethodInfo GetPropertySetMethod<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName).GetSetMethod();
        }
    }
}
