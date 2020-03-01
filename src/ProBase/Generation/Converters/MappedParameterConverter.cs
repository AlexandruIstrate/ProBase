using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using static ProBase.Generation.Converters.ParameterInfoConverter;

namespace ProBase.Generation.Converters
{
    internal class MappedParameterConverter : IParameterConverter<DbParameter[]>
    {
        public DbParameter[] ConvertParameter(ParameterInfo parameterInfo, object value)
        {
            List<DbParameter> result = new List<DbParameter>();

            foreach (PropertyInfo property in parameterInfo.ParameterType.GetProperties())
            {
                result.Add(ConvertProperty(property));
            }

            return result.ToArray();
        }

        private DbParameter ConvertProperty(PropertyInfo property)
        {
            return new DbParameterInfo
            {
                ParameterName = property.Name,
                Direction = ParameterDirection.Input,
                Type = property.PropertyType
            };
        }
    }
}
