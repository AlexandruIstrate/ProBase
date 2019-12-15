using System;

namespace ProBase.Utils
{
    /// <summary>
    /// Provides extensions for working with <see cref="System.Type"/>.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Checks whether a given type implements an interface.
        /// </summary>
        /// <param name="type">The type to check for</param>
        /// <param name="interfaceType">The interface</param>
        /// <returns>True if the type implements the interface, false otherwise</returns>
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Checks if a given type matches another.
        /// </summary>
        /// <param name="type">The type to check for</param>
        /// <param name="testType">The type to match</param>
        /// <returns>True if the two types match, false otherwise</returns>
        public static bool IsType(this Type type, Type testType)
        {
            return type == testType;
        }
    }
}
