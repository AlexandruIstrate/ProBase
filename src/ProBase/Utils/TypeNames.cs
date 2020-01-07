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
        /// Generates a name for a class prefixing the current assembly's name.
        /// </summary>
        /// <param name="className">The name of the class</param>
        /// <returns>A full class name, including the assembly name</returns>
        public static string GenerateFullClassName(string className)
        {
            return $"{ GetAssemblyName() }.{ className }";
        }
    }
}
