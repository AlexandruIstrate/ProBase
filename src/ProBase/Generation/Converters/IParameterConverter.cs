using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides an operation for converting between a method parameter and a database parameter.
    /// </summary>
    /// <typeparam name="TResult">The type of the conversion result</typeparam>
    internal interface IParameterConverter<out TResult>
    {
        /// <summary>
        /// Converts a method parameter to a <see cref="System.Data.Common.DbParameter"/>.
        /// </summary>
        /// <param name="parameterInfo">A reflection method parameter</param>
        /// <param name="value">The value of the parameter</param>
        /// <returns>A database parameter</returns>
        TResult ConvertParameter(ParameterInfo parameterInfo, object value);
    }
}
