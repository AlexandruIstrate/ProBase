using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ProBase.Data
{
    /// <summary>
    /// The default implementation of <see cref="ProBase.Data.IProcedureMapper"/>.
    /// </summary>
    internal class ProcedureMapper : Database, IProcedureMapper
    {
        public ProcedureMapper(DbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        public Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
