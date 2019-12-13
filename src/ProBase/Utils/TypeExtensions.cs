using System;
using System.Collections;

namespace ProBase.Utils
{
    internal static class TypeExtensions
    {
        public static bool IsEnumerable(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
        
        public static bool IsCollection(this Type type)
        {
            return typeof(ICollection).IsAssignableFrom(type);
        }
    }
}
