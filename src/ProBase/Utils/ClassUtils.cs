using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProBase.Utils
{
    /// <summary>
    /// Provides operations for working with generated classes.
    /// </summary>
    internal class ClassUtils
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
                throw new Exception($"The generated class does not contain any fields of type { typeof(T).FullName }");
            }

            FieldInfo namedField = typedFields.ToList().Find(field => field.Name == fieldName);

            if (namedField == null)
            {
                throw new Exception($"Could not find a field with name { fieldName }");
            }

            return namedField;
        }

        /// <summary>
        /// Retrieves information about a method in a specified type.
        /// </summary>
        /// <typeparam name="T">The type to look in for the method</typeparam>
        /// <param name="name">The name of the method</param>
        /// <returns>Information about the method</returns>
        public static MethodInfo GetMethod<T>(string name)
        {
            Type type = typeof(T);

            const BindingFlags flags = BindingFlags.Public
                                       | BindingFlags.NonPublic
                                       | BindingFlags.Instance
                                       | BindingFlags.FlattenHierarchy;

            // Search class for the method
            MethodInfo classMethod = type.GetMethod(name, flags);

            if (classMethod != null)
            {
                return classMethod;
            }

            // Search interfaces for the method
            foreach (Type interf in type.GetInterfaces())
            {
                foreach (MethodInfo method in interf.GetMethods(flags))
                {
                    if (method.Name == name)
                    {
                        return method;
                    }
                }
            }

            return null;
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

        public static MethodInfo GetConvertMethod(Type type)
        {
            IEnumerable<MethodInfo> methods = typeof(Convert).GetMethods()
                .Where(m => m.Name == $"To{ type.Name }")
                .Where(m => m.GetParameters().Length == 1)
                .Where(m => m.GetParameters().First().ParameterType == typeof(object));

            return methods.First();
        }
    }
}
