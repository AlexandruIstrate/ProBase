using System;
using System.Reflection;

namespace ProBase.Utils
{
    /// <summary>
    /// Provides utilities for working with generic types.
    /// </summary>
    public static class GenericUtils
    {
        /// <summary>
        /// Checks if a generic type matches the type definition of another type.
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <param name="check">The other type</param>
        /// <returns>True if the definitions match, false otherwise</returns>
        public static bool IsGenericTypeDefinition(this Type type, Type check)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == check;
        }

        /// <summary>
        /// Invokes a generic method with the suplied generic parameters.
        /// </summary>
        /// <param name="methodInfo">The method information</param>
        /// <param name="genericTypes">An array of generic types</param>
        /// <param name="instance">The instance to call the method on</param>
        /// <param name="parameters">The method parameters</param>
        /// <returns>The method's return value</returns>
        public static object InvokeGenericMethod(this MethodInfo methodInfo, Type[] genericTypes, object instance, object[] parameters)
        {
            MethodInfo definition = methodInfo.GetGenericMethodDefinition();
            MethodInfo genericMethod = definition.MakeGenericMethod(genericTypes);
            return genericMethod.Invoke(instance, parameters);
        }
    }
}
