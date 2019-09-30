using ProBase.Attributes;
using ProBase.Utils;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Represents a database procedure parameter converter that uses language information to generate database parameters from method parameters.
    /// </summary>
    internal class ParameterConverter : IParameterConverter
    {
        public DbParameter ConvertParameter(ParameterInfo parameterInfo, object value)
        {
            Preconditions.CheckNotNull(parameterInfo, nameof(parameterInfo));

            DbParameter dbParameter = null;
            dbParameter.ParameterName = GetParameterName(parameterInfo);
            dbParameter.Direction = GetParameterDirection(parameterInfo);
            dbParameter.Value = value;
            return dbParameter;
        }

        private string GetParameterName(ParameterInfo parameterInfo)
        {
            return parameterInfo.GetCustomAttribute<ParameterAttribute>()?.ParameterName ?? parameterInfo.Name;
        }

        private ParameterDirection GetParameterDirection(ParameterInfo parameterInfo)
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
