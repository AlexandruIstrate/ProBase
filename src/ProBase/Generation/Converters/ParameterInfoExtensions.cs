using ProBase.Utils;
using System.Data;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides extensions for working with method parameters that are used as database parameters.
    /// </summary>
    internal static class ParameterInfoExtensions
    {
        /// <summary>
        /// Gets the direction of this parameter.
        /// </summary>
        /// <param name="parameterInfo">The object to call this method on</param>
        /// <returns>The direction of the parameter</returns>
        public static ParameterDirection GetDbParameterDirection(this ParameterInfo parameterInfo)
        {
            if (parameterInfo.IsOut || parameterInfo.ParameterType.IsAsyncOut())
            {
                return ParameterDirection.Output;
            }

            if (parameterInfo.ParameterType.IsByRef || parameterInfo.ParameterType.IsAsyncInOut())
            {
                return ParameterDirection.InputOutput;
            }

            return ParameterDirection.Input;
        }
    }
}
