using ProBase.Generation.Converters;
using System.Collections.Generic;
using System.Data;
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
            dataMapper = DataMapperFactory.Create(DataMapperType.DataSet);
        }

        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            if (typeof(T) == typeof(IEnumerable<T>))
            {
                return (T)MapProcedureEnumerable<T>(ExecuteScalarProcedure(procedureName, parameters));
            }

            return MapProcedure<T>(ExecuteScalarProcedure(procedureName, parameters));
        }

        public async Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            if (typeof(T) == typeof(IEnumerable<T>))
            {
                return (T)MapProcedureEnumerable<T>(await ExecuteScalarProcedureAsync(procedureName, parameters));
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
