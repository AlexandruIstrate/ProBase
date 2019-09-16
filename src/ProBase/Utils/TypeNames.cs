using System;
using System.Reflection;

namespace ProBase.Utils
{
    /// <summary>
    /// Contains utilities for dealing with type names.
    /// </summary>
    internal static class TypeNames
    {
        public static string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static string GetModuleName()
        {
            throw new NotImplementedException();
        }

        public static string GenerateUniqueTypeName()
        {
            throw new NotImplementedException();
        }
    }
}
