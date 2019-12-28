using System.Data.Common;
using System.Threading.Tasks;

namespace ProBase.Data
{
    /// <summary>
    /// Provides a way to map a procedure's result to a given data type.
    /// </summary>
    internal interface IProcedureMapper : IDatabase
    {
        /// <summary>
        /// Runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new();
        
        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new();
    }
}
