using ProBase.Generation.Converters;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ProBase.Data
{
    /// <summary>
    /// The default implementation of <see cref="ProBase.Data.IDatabaseMapper"/>.
    /// </summary>
    internal class DatabaseMapper : IDatabaseMapper
    {
        /// <summary>
        /// Gets or sets the <see cref="ProBase.Data.IDatabase"/> this mapper uses.
        /// </summary>
        public IDatabase Database { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ProBase.Generation.Converters.IDataMapper"/> this mapper uses.
        /// </summary>
        public IDataMapper DataMapper { get; set; }

        public DatabaseMapper(IDatabase database, IDataMapper dataMapper)
        {
            Database = database;
            DataMapper = dataMapper;
        }

        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
