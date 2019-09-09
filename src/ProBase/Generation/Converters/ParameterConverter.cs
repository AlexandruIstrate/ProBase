using ProBase.Attributes;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    internal class ParameterConverter : IParameterConverter
    {
        public DbParameter ConvertParameter(ParameterInfo parameterInfo, object value)
        {
            DbParameter dbParameter = null;
            dbParameter.ParameterName = GetParameterName(parameterInfo);
            dbParameter.Direction = GetParameterDirection(parameterInfo);
            dbParameter.Value = value;
            return dbParameter;
        }

        private string GetParameterName(ParameterInfo parameterInfo)
        {
            return parameterInfo.GetCustomAttribute<ParameterAttribute>()?.ProcedureParameterName ?? parameterInfo.Name;
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
