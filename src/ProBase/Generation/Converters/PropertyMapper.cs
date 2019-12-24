using ProBase.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Maps values to primitive types;
    /// </summary>
    internal class PropertyMapper : IPropertyMapper
    {
        /// <summary>
        /// Maps the values of a <see cref="System.Data.DataRow"/> to a type's properties.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="row">The DataRow</param>
        /// <param name="entity">The object value</param>
        public void Map<TEntity>(DataRow row, object entity) where TEntity : new()
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                SetPropertyValue<TEntity>(property, row, entity);
            }
        }

        private void SetPropertyValue<T>(PropertyInfo property, DataRow row, object entity)
        {
            ColumnAttribute columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

            string propertyName = columnAttribute?.ColumnName ?? property.Name;
            bool caseSensitive = columnAttribute?.CaseSensitive ?? false;

            IEnumerable<DataColumn> columns = row.Table.Columns
                                                       .Cast<DataColumn>()
                                                       .Where(c => c.ColumnName.Equals(propertyName, GetComparisonType(caseSensitive)));

            // No column could be found
            if (columns.Count() == 0)
            {
                // Handle the null value of the property
                HandleNullValue<T>(property, entity);
            }

            property.SetValue(entity, columns.First());
        }

        private void HandleNullValue<T>(PropertyInfo property, object entity)
        {
            if (property.GetType().IsValueType)
            {
                property.SetValue(entity, value: null);
            }
            else
            {
                property.SetValue(entity, default(T));
            }
        }

        private StringComparison GetComparisonType(bool caseSensitive) => caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
    }
}
