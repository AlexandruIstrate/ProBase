﻿using ProBase.Attributes;
using ProBase.Utils;
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
                // Handle the missing value of the property
                HandleMissingValue<T>(property, entity);

                return;
            }

            object value = row[columns.First()];

            // The value in the DataRow is DBNull, meaning no value
            if (value.GetType() == typeof(DBNull))
            {
                // Set the value to null
                value = null;
            }

            property.SetValue(entity, value);
        }

        private void HandleMissingValue<T>(PropertyInfo property, object entity)
        {
            object value = null;

            if (property.PropertyType.IsValueType)
            {
                // If we have a value type, the use the default value
                value = default(T);

                if (property.PropertyType.IsGenericTypeDefinition(typeof(Nullable<>)))
                {
                    // For Nullable value types use a null value
                    value = null;
                }
            }

            property.SetValue(entity, value);
        }

        private StringComparison GetComparisonType(bool caseSensitive) => caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
    }
}
