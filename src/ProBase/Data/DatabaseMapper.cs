using System;
using System.Data;
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

        public DatabaseMapper(IDatabase database)
        {
            Database = database;
        }

        public int ExecuteNonQueryProcedure(string procedureName, params DbParameter[] parameters)
        {
            return Database.ExecuteNonQueryProcedure(procedureName, parameters);
        }

        public Task<int> ExecuteNonQueryProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            return Database.ExecuteNonQueryProcedureAsync(procedureName, parameters);
        }

        public DataSet ExecuteScalarProcedure(string procedureName, params DbParameter[] parameters)
        {
            return Database.ExecuteScalarProcedure(procedureName, parameters);
        }

        public Task<DataSet> ExecuteScalarProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            return Database.ExecuteScalarProcedureAsync(procedureName, parameters);
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
