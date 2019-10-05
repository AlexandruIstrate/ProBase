using ProBase.Attributes;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides an operation for converting between a method parameter and an information-only representation of that parameter.
    /// </summary>
    internal class ParameterInfoConverter : IParameterConverter
    {
        /// <summary>
        /// Converts a method parameter to a <see cref="System.Data.Common.DbParameter"/>.
        /// </summary>
        /// <param name="parameterInfo">A reflection method parameter</param>
        /// <param name="value">The value of the parameter</param>
        /// <returns>A database parameter</returns>
        public DbParameter ConvertParameter(ParameterInfo parameterInfo, object value)
        {
            DbParameterInfo dbParameter = new DbParameterInfo
            {
                ParameterName = GetParameterName(parameterInfo),
                Direction = GetParameterDirection(parameterInfo),
                Value = value
            };
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

        private class DbParameterInfo : DbParameter
        {
            public override DbType DbType { get; set; }
            public override ParameterDirection Direction { get; set; }
            public override bool IsNullable { get; set; }
            public override string ParameterName { get; set; }
            public override int Size { get; set; }
            public override string SourceColumn { get; set; }
            public override bool SourceColumnNullMapping { get; set; }
            public override object Value { get; set; }

            public override void ResetDbType()
            {
                throw new NotSupportedException("This type only provides parameter information");
            }
        }
    }
}
