using System.Data.Common;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides an operation for converting between a method parameter and a database parameter.
    /// </summary>
    internal interface IMethodParameterConverter
    {
        /// <summary>
        /// Converts a method parameter to a <see cref="System.Data.Common.DbParameter"/>.
        /// </summary>
        /// <param name="parameterInfo">A reflection method parameter</param>
        /// <returns>A database parameter</returns>
        DbParameter ConvertParameter(ParameterInfo parameterInfo);
    }
}
