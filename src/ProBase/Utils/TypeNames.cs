using System;
using System.Reflection;

namespace ProBase.Utils
{
    /// <summary>
    /// Contains utilities for dealing with type names.
    /// </summary>
    internal static class TypeNames
    {
        /// <summary>
        /// Gets the name of the current assembly.
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// Gets the name of the current module.
        /// </summary>
        /// <returns></returns>
        public static string GetModuleName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates an unique type name for use in code generation.
        /// </summary>
        /// <returns></returns>
        public static string GenerateUniqueTypeName()
        {
            throw new NotImplementedException();
        }
    }
}
