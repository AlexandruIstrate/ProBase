using ProBase.Generation.Converters;
using ProBase.Utils;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace ProBase.Data
{
    /// <summary>
    /// The default implementation of <see cref="ProBase.Data.IProcedureMapper"/>.
    /// </summary>
    internal class ProcedureMapper : Database, IProcedureMapper
    {
        /// <summary>
        /// Constructs a new <see cref="ProBase.Data.ProcedureMapper"/> instance.
        /// </summary>
        /// <param name="connection">The connection to use for communicating with the database</param>
        /// <param name="dataMapper">The mapper used for mapping operations</param>
        public ProcedureMapper(DbConnection connection, IDataMapper dataMapper) : base(connection)
        {
            this.dataMapper = dataMapper;
        }

        /// <summary>
        /// Runs the given procedure and maps its result to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            return MapProcedure<T>(ExecuteScalarProcedure(procedureName, parameters));
        }

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to an object of type type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        public async Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            return MapProcedure<T>(await ExecuteScalarProcedureAsync(procedureName, parameters));
        }

        /// <summary>
        /// Runs the given procedure and maps its result to an <see cref="System.Collections.Generic.IEnumerable{T}"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        public IEnumerable<T> ExecuteEnumerableMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            DataSet dataSet = ExecuteScalarProcedure(procedureName, parameters);

            MethodInfo mapMethod = ClassUtils.GetMethod<ProcedureMapper>(nameof(MapProcedureEnumerable));
            return (IEnumerable<T>)mapMethod.InvokeGenericMethod(new[] { typeof(T) }, this, new[] { dataSet });
        }

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to an <see cref="System.Collections.Generic.IEnumerable{T}"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped object</returns>
        public async Task<IEnumerable<T>> ExecuteEnumerableMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            DataSet dataSet = await ExecuteScalarProcedureAsync(procedureName, parameters);

            MethodInfo mapMethod = ClassUtils.GetMethod<ProcedureMapper>(nameof(MapProcedureEnumerable));
            return (IEnumerable<T>)mapMethod.InvokeGenericMethod(new[] { typeof(T) }, this, new[] { dataSet });
        }

        private T MapProcedure<T>(DataSet dataSet) where T : class, new()
        {
            // If there are no tables, then there is no data
            if (dataSet.Tables.Count == 0)
            {
                return null;
            }

            DataTable table = dataSet.Tables[0];

            if (table.Rows.Count == 0)
            {
                return null;
            }

            // If we expect only one return value, then we can assume that we only care about the first row
            return dataMapper.Map<T>(table.Rows[0]);
        }

        private IEnumerable<T> MapProcedureEnumerable<T>(DataSet dataSet) where T : class, new()
        {
            // If there are no tables, then there is no data
            if (dataSet.Tables.Count == 0)
            {
                return null;
            }

            // We expect only one data type, so use only the first table
            return dataMapper.Map<T>(dataSet.Tables[0]);
        }

        private readonly IDataMapper dataMapper;
    }
}
