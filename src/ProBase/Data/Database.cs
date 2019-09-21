using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ProBase.Data
{
    /// <summary>
    /// The default implementaion of <see cref="ProBase.Data.IDatabase"/>.
    /// </summary>
    internal class Database : IDatabase
    {
        /// <summary>
        /// Gets or sets the connection used for operations.
        /// </summary>
        public IDbConnection Connection { get; set; }

        public Database(IDbConnection connection)
        {
            Connection = connection;
        }

        public int ExecuteNonQueryProcedure(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = null;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }

        public Task<int> ExecuteNonQueryProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = null;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQueryAsync();
        }

        public DataSet ExecuteScalarProcedure(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = null;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);

            using (DataAdapter dataAdapter = null)
            {
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        public Task<DataSet> ExecuteScalarProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = null;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);

            using (DataAdapter dataAdapter = null)
            {
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return Task.FromResult(dataSet);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
