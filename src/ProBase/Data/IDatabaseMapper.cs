using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ProBase.Data
{
    /// <summary>
    /// Provides a way to map a procedure's result to a given data type.
    /// </summary>
    internal interface IDatabaseMapper
    {
        /// <summary>
        /// Executes an SQL procedure against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>The number of rows affected</returns>
        int ExecuteNonQueryProcedure(string procedureName, params DbParameter[] parameters);

        /// <summary>
        /// Asynchronously executes an SQL procedure against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>The number of rows affected</returns>
        Task<int> ExecuteNonQueryProcedureAsync(string procedureName, params DbParameter[] parameters);

        /// <summary>
        /// Executes an SQL procedure and returns a <see cref="System.Data.DataSet"/> containing the data returned from the database.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>A <see cref="System.Data.DataSet"/> containing the data returned from the database</returns>
        DataSet ExecuteScalarProcedure(string procedureName, params DbParameter[] parameters);

        /// <summary>
        /// Asynchronously executes an SQL procedure and returns a <see cref="System.Data.DataSet"/> containing the data returned from the database.
        /// </summary>
        /// <param name="procedureName">The name of the procedure to execute</param>
        /// <param name="parameters">An array containing the parameters to be passed to the procedure</param>
        /// <returns>A <see cref="System.Data.DataSet"/> containing the data returned from the database</returns>
        Task<DataSet> ExecuteScalarProcedureAsync(string procedureName, params DbParameter[] parameters);

        /// <summary>
        /// Runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters);

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters);
    }
}
