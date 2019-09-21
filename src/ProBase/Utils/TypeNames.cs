using System;
using System.Linq;
using System.Reflection;
using System.Text;

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
            return Assembly.GetExecutingAssembly().Modules.First().Name;
        }

        /// <summary>
        /// Generates an unique type name for use in code generation.
        /// </summary>
        /// <returns></returns>
        public static string GenerateUniqueTypeName(string typeName)
        {
            const int length = 8;

            Random random = new Random();
            StringBuilder nameBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                nameBuilder.Append((char)random.Next('a', 'z'));
            }

            return $"{ typeName }_{ nameBuilder.ToString() }";
        }
    }
}
