using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ProBase.Data
{
    // TODO: Make this internal
    /// <summary>
    /// Provides a way to map a procedure's result to a given data type.
    /// </summary>
    public /*internal*/ interface IProcedureMapper : IDatabase
    {
        /// <summary>
        /// Runs the given procedure and maps its result to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new();

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to an object of type type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new();

        /// <summary>
        /// Runs the given procedure and maps its result to an <see cref="System.Collections.Generic.IEnumerable{T}"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        IEnumerable<T> ExecuteEnumerableMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new();

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to an <see cref="System.Collections.Generic.IEnumerable{T}"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        Task<IEnumerable<T>> ExecuteEnumerableMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new();
    }
}
