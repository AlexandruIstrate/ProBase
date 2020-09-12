using ProBase.Async;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
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
        /// <returns>True if the type is a Task, false otherwise</returns>
        public static bool IsTask(this Type type)
        {
            if (type == typeof(Task))
            {
                return true;
            }

            return type.IsGenericTypeDefinition(typeof(Task<>));
        }

        /// <summary>
        /// Checks whether a type is <see cref="ProBase.Async.AsyncOut{TParameter}"/>.
        /// </summary>
        /// <param name="type">The type to check for</param>
        /// <returns>True if the type is AsyncOut, false otherwise</returns>
        public static bool IsAsyncOut(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(AsyncOut<>);
        }

        /// <summary>
        /// Checks whether a type is <see cref="ProBase.Async.AsyncInOut{TParameter}"/>.
        /// </summary>
        /// <param name="type">The type to check for</param>
        /// <returns>True if the type is AsyncInOut, false otherwise</returns>
        public static bool IsAsyncInOut(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(AsyncInOut<>);
        }

        /// <summary>
        /// Checks whether a type is user defined.
        /// </summary>
        /// <param name="type">The type to check for</param>
        /// <returns>True if the type is user defined</returns>
        public static bool IsUserDefined(this Type type)
        {
            if (type.IsByRef)
            {
                return type.GetElementType().IsUserDefined();
            }

            if (type.IsAsyncOut())
            {
                return false;
            }

            if (type.IsPrimitive)
            {
                return false;
            }

            if (type == typeof(string))
            {
                return false;
            }

            if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the name of the current assembly.
        /// </summary>
        /// <returns>The name of the current assembly</returns>
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

        /// <summary>
        /// Converts a <see cref="System.Type"/> to a <see cref="System.Data.DbType"/>.
        /// </summary>
        /// <param name="type">The type to convert</param>
        /// <returns>The converted type</returns>
        public static DbType ConvertTypeToDbType(Type type)
        {
            // This has been copied from the StackExchange/Dapper GitHub repository
            // https://github.com/StackExchange/Dapper/blob/4fb1ea29d490d13251b0135658ecc337aeb60cdb/Dapper/SqlMapper.cs#L169
            var typeMap = new Dictionary<Type, DbType>(37)
            {
                [typeof(byte)] = DbType.Byte,
                [typeof(sbyte)] = DbType.SByte,
                [typeof(short)] = DbType.Int16,
                [typeof(ushort)] = DbType.UInt16,
                [typeof(int)] = DbType.Int32,
                [typeof(uint)] = DbType.UInt32,
                [typeof(long)] = DbType.Int64,
                [typeof(ulong)] = DbType.UInt64,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(bool)] = DbType.Boolean,
                [typeof(string)] = DbType.String,
                [typeof(char)] = DbType.StringFixedLength,
                [typeof(Guid)] = DbType.Guid,
                [typeof(DateTime)] = DbType.DateTime,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(TimeSpan)] = DbType.Time,
                [typeof(byte[])] = DbType.Binary,
                [typeof(byte?)] = DbType.Byte,
                [typeof(sbyte?)] = DbType.SByte,
                [typeof(short?)] = DbType.Int16,
                [typeof(ushort?)] = DbType.UInt16,
                [typeof(int?)] = DbType.Int32,
                [typeof(uint?)] = DbType.UInt32,
                [typeof(long?)] = DbType.Int64,
                [typeof(ulong?)] = DbType.UInt64,
                [typeof(float?)] = DbType.Single,
                [typeof(double?)] = DbType.Double,
                [typeof(decimal?)] = DbType.Decimal,
                [typeof(bool?)] = DbType.Boolean,
                [typeof(char?)] = DbType.StringFixedLength,
                [typeof(Guid?)] = DbType.Guid,
                [typeof(DateTime?)] = DbType.DateTime,
                [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                [typeof(TimeSpan?)] = DbType.Time,
                [typeof(object)] = DbType.Object
            };

            return typeMap[type];
        }
    }
}
