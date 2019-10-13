using ProBase.Utils;
using System.Data.Common;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Represents a database procedure parameter converter that uses language information to generate database parameters from method parameters.
    /// </summary>
    internal class ParameterConverter : IParameterConverter
    {
        public ParameterConverter(DbProviderFactory providerFactory)
        {
            this.providerFactory = providerFactory;
        }

        public DbParameter ConvertParameter(ParameterInfo parameterInfo, object value)
        {
            Preconditions.CheckNotNull(parameterInfo, nameof(parameterInfo));

            DbParameter dbParameter = providerFactory.CreateParameter();
            dbParameter.ParameterName = parameterInfo.GetDbParameterName();
            dbParameter.Direction = parameterInfo.GetDbParameterDirection();
            dbParameter.Value = value;
            return dbParameter;
        }

        private readonly DbProviderFactory providerFactory;
    }
}
