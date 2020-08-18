using ProBase.Utils;
using System;
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
        public DbConnection Connection
        {
            get => connectionTemplate;
            set
            {
                connectionTemplate = Preconditions.CheckNotNull(value, nameof(Connection));
                providerFactory = value.GetProviderFactory();
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
            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionTemplate.ConnectionString;

            try
            {
                connection.Open();

                DbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Asynchronously executes an SQL procedure against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> ExecuteNonQueryProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionTemplate.ConnectionString;

            try
            {
                await connection.OpenAsync();

                DbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;
                command.Parameters.AddRange(parameters);
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Executes an SQL procedure and returns a <see cref="System.Data.DataSet"/> containing the data returned from the database.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>A <see cref="System.Data.DataSet"/> containing the data returned from the database</returns>
        public DataSet ExecuteScalarProcedure(string procedureName, params DbParameter[] parameters)
        {
            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionTemplate.ConnectionString;

            try
            {
                connection.Open();

                using (DbCommand command = providerFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;
                    command.Parameters.AddRange(parameters);

                    DataSet dataSet = new DataSet();

                    using (DbDataAdapter dataAdapter = providerFactory.CreateDataAdapter())
                    {
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(dataSet);
                    }

                    return dataSet;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Asynchronously executes an SQL procedure and returns a <see cref="System.Data.DataSet"/> containing the data returned from the database.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>A <see cref="System.Data.DataSet"/> containing the data returned from the database</returns>
        public async Task<DataSet> ExecuteScalarProcedureAsync(string procedureName, params DbParameter[] parameters)
        {
            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionTemplate.ConnectionString;

            try
            {
                await connection.OpenAsync();

                using (DbCommand command = providerFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;
                    command.Parameters.AddRange(parameters);

                    DataSet dataSet = new DataSet();

                    using (DbDataAdapter dataAdapter = providerFactory.CreateDataAdapter())
                    {
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(dataSet);
                    }

                    return dataSet;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            connectionTemplate.Dispose();
        }

        private DbConnection connectionTemplate;
        private DbProviderFactory providerFactory;
    }
}
