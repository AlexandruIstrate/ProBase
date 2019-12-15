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
        /// <param name="ignoreColumnCase">Whether to ignore the casing of the column name</param>
        public void Map<TEntity>(DataRow row, object entity, bool ignoreColumnCase = false) where TEntity : new()
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties(ignoreColumnCase ? BindingFlags.IgnoreCase : BindingFlags.Default);

            foreach (PropertyInfo property in properties)
            {
                IEnumerable<DataColumn> columns = row.Table.Columns
                                                           .Cast<DataColumn>()
                                                           .Where(c => c.ColumnName == property.Name);

                if (columns.Count() == 0)
                {
                    // If the property is not a value type, then set its value to null
                    if (!property.GetType().IsValueType)
                    {
                        property.SetValue(entity, null);
                        continue;
                    }

                    throw new Exception("Column not found");
                }

                property.SetValue(entity, columns.First());
            }
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
    }
}
