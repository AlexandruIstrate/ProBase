using ProBase.Attributes;
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
        public DataSetMapper(IPropertyMapper propertyMapper)
        {
            this.propertyMapper = propertyMapper;
        }

        /// <summary>
        /// Maps a <see cref="System.Data.DataRow"/> to an object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the object</typeparam>
        /// <param name="row">The row</param>
        /// <returns>The mapped object</returns>
        public TEntity Map<TEntity>(DataRow row) where TEntity : class, new()
        {
            // Step 1 - Get the column names
            List<string> columnNames = row.Table.Columns
                                                .Cast<DataColumn>()
                                                .Select(c => c.ColumnName)
                                                .ToList();

            // Step 2 - Get the properties
            List<PropertyInfo> properties = typeof(TEntity).GetProperties()
                                                           .Where(p => p.GetCustomAttributes(typeof(ColumnAttribute), inherit: true).Any())
                                                           .ToList();

            // Step 3 - Map the data
            TEntity entity = new TEntity();
            propertyMapper.Map<TEntity>(row, entity);

            return entity;
        }

        /// <summary>
        /// Maps a <see cref="System.Data.DataTable"/> to an <see cref="System.Collections.Generic.IEnumerable{T}"/> of objects.
        /// </summary>
        /// <typeparam name="TEntity">The type of the objects</typeparam>
        /// <param name="table">The table</param>
        /// <returns>The mapped enumeration</returns>
        public IEnumerable<TEntity> Map<TEntity>(DataTable table) where TEntity : class, new()
        {
            List<TEntity> entities = new List<TEntity>();

            // Map each DataRow
            foreach (DataRow row in table.Rows)
            {
                TEntity entity = new TEntity();
                propertyMapper.Map<TEntity>(row, entity);

                entities.Add(entity);
            }

            return entities;
        }

        private readonly IPropertyMapper propertyMapper;
    }
}
