using ProBase.Attributes;
using System.Data;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides extensions for working with method parameters that are used as database parameters.
    /// </summary>
    internal static class DbParameterInfoExtensions
    {
        /// <summary>
        /// Gets the name of this parameter.
        /// </summary>
        /// <param name="parameterInfo">The object to call this method on</param>
        /// <returns>The name of the parameter</returns>
        public static string GetDbParameterName(this ParameterInfo parameterInfo)
        {
            return parameterInfo.GetCustomAttribute<ParameterAttribute>()?.ParameterName ?? parameterInfo.Name;
        }

        /// <summary>
        /// Gets the direction of this parameter.
        /// </summary>
        /// <param name="parameterInfo">The object to call this method on</param>
        /// <returns>The direction of the parameter</returns>
        public static ParameterDirection GetDbParameterDirection(this ParameterInfo parameterInfo)
        {
            if (parameterInfo.IsOut)
            {
                return ParameterDirection.Output;
            }

            if (parameterInfo.ParameterType.IsByRef)
            {
                return ParameterDirection.InputOutput;
            }

            return ParameterDirection.Input;
        }
    }
}
