using ProBase.Generation.Converters;
using System;
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
        public ProcedureMapper(DbConnection connection) : base(connection)
        {
            dataMapper = DataMapperFactory.Create(DataMapperType.DataSet);
        }

        /// <summary>
        /// Runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            Type type = typeof(T);

            if (type.IsGenericType)
            {
                // Get the generic type definition of the generic type
                Type definition = type.GetGenericTypeDefinition();

                // If the definition is that of an IEnumerable<T>, we can call the coresponding map method
                if (definition == typeof(IEnumerable<>))
                {
                    // Execute the procedure
                    DataSet dataSet = ExecuteScalarProcedure(procedureName, parameters);

                    // Get the IEnumerable<T> map method
                    MethodInfo mapMethod = typeof(ProcedureMapper).GetMethod(nameof(MapProcedureEnumerable));

                    // Make the method generic with the enumerable's type parameter
                    MethodInfo genericMethod = mapMethod.MakeGenericMethod(type.GetGenericArguments());

                    // Invoke the generic method passing in the DataSet and casting the result to the IEnumerable<T> we have to return
                    return (T)genericMethod.Invoke(this, new object[] { dataSet });
                }
            }

            return MapProcedure<T>(ExecuteScalarProcedure(procedureName, parameters));
        }

        /// <summary>
        /// Asynchronously runs the given procedure and maps its result to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="procedureName">The name of the procedure to call</param>
        /// <param name="parameters">The parameters to pass into the procedure</param>
        /// <returns>The mapped type</returns>
        public async Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            Type type = typeof(T);

            if (type.IsGenericType)
            {
                // Get the generic type definition of the generic type
                Type definition = type.GetGenericTypeDefinition();

                // If the definition is that of an IEnumerable<T>, we can call the coresponding map method
                if (definition == typeof(IEnumerable<>))
                {
                    // Execute the procedure
                    DataSet dataSet = await ExecuteScalarProcedureAsync(procedureName, parameters);

                    // Get the IEnumerable<T> map method
                    MethodInfo mapMethod = typeof(ProcedureMapper).GetMethod(nameof(MapProcedureEnumerable));

                    // Make the method generic with the enumerable's type parameter
                    MethodInfo genericMethod = mapMethod.MakeGenericMethod(type.GetGenericArguments());

                    // Invoke the generic method passing in the DataSet and casting the result to the IEnumerable<T> we have to return
                    return (T)genericMethod.Invoke(this, new object[] { dataSet });
                }
            }

            return MapProcedure<T>(await ExecuteScalarProcedureAsync(procedureName, parameters));
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
