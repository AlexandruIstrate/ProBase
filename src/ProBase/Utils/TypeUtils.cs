﻿using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProBase.Utils
{
    /// <summary>
    /// Provides utilities for working with types.
    /// </summary>
    public static class TypeUtils
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
        /// Checks whether a given type is a <see cref="System.Threading.Tasks.Task"/> or <see cref="System.Threading.Tasks.Task{TResult}"/>.
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if the type is a task, false otherwise</returns>
        public static bool IsTask(this Type type)
        {
            if (type == typeof(Task))
            {
                return true;
            }

            return type.IsGenericTypeDefinition(typeof(Task<>));
        }

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
