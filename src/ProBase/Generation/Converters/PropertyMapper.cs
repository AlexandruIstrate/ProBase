using System;
using System.Data;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Maps values to primitive types;
    /// </summary>
    internal class PropertyMapper
    {
        public static void Map(Type type, DataRow row, PropertyInfo prop, object entity)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                ParsePrimitive(prop, entity, row[column.ColumnName]);
            }
        }

        private static void ParsePrimitive(PropertyInfo prop, object entity, object value)
        {
            prop.SetValue(entity, value, index: null);
        }
    }
}
