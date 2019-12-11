using ProBase.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Maps a <see cref="System.Data.DataSet"/> to an object that has the same properties as the columns in the DataSet.
    /// </summary>
    internal class DataSetMapper : IDataMapper
    {
        public TEntity Map<TEntity>(DataRow row) where TEntity : class, new()
        {
            // Step 1 - Get the Column Names
            List<string> columnNames = row.Table.Columns
                                                .Cast<DataColumn>()
                                                .Select(c => c.ColumnName)
                                                .ToList();

            // Step 2 - Get the Properties
            List<PropertyInfo> properties = typeof(TEntity).GetProperties()
                                                           .Where(p => p.GetCustomAttributes(typeof(ColumnAttribute), true).Any())
                                                           .ToList();

            // Step 3 - Map the data
            TEntity entity = new TEntity();

            foreach (PropertyInfo property in properties)
            {
                PropertyMapper.Map(typeof(TEntity), row, property, entity);
            }

            return entity;
        }

        public IEnumerable<T> Map<T>(DataTable table) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
