using ProBase.Utils;
using System.Data;
using System.Data.Common;
using System.Reflection;
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
        public DbConnection Connection
        {
            get => connection;
            set
            {
                connection = Preconditions.CheckNotNull(value, nameof(Connection));
                providerFactory = GetProviderFactory(value);
            }
        }

        public Database(DbConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Executes an SQL procedure against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>The number of rows affected</returns>
        public int ExecuteNonQueryProcedure(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Asynchronously executes an SQL procedure against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>The number of rows affected</returns>
        public Task<int> ExecuteNonQueryProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Executes an SQL procedure and returns a <see cref="System.Data.DataSet"/> containing the data returned from the database.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>A <see cref="System.Data.DataSet"/> containing the data returned from the database</returns>
        public DataSet ExecuteScalarProcedure(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);

            using (DataAdapter dataAdapter = providerFactory.CreateDataAdapter())
            {
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        /// <summary>
        /// Asynchronously executes an SQL procedure and returns a <see cref="System.Data.DataSet"/> containing the data returned from the database.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>A <see cref="System.Data.DataSet"/> containing the data returned from the database</returns>
        public Task<DataSet> ExecuteScalarProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Parameters.AddRange(parameters);

            using (DataAdapter dataAdapter = providerFactory.CreateDataAdapter())
            {
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return Task.FromResult(dataSet);
            }
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            Connection.Dispose();
        }

        private DbProviderFactory GetProviderFactory(DbConnection connection)
        {
            PropertyInfo factoryField = connection.GetType().GetProperty("DbProviderFactory", BindingFlags.NonPublic | BindingFlags.Instance);
            return (DbProviderFactory)factoryField.GetValue(connection);
        }

        private DbConnection connection;

        private DbProviderFactory providerFactory;
    }
}
