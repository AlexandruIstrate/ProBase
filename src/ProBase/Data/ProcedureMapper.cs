using ProBase.Generation.Converters;
using ProBase.Utils;
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
            if (typeof(T).IsEnumerable())
            {
                return (T)MapProcedureEnumerable<T>(ExecuteScalarProcedure(procedureName, parameters));
            }

            return MapProcedure<T>(ExecuteScalarProcedure(procedureName, parameters));
        }

        public async Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class, new()
        {
            if (typeof(T).IsEnumerable())
            {
                return (T)MapProcedureEnumerable<T>(await ExecuteScalarProcedureAsync(procedureName, parameters));
            }

            return MapProcedure<T>(await ExecuteScalarProcedureAsync(procedureName, parameters));
        }

        private T MapProcedure<T>(DataSet dataSet) where T : class, new()
        {
            if (dataSet.Tables.Count == 0)
            {
                return null;
            }

            DataTable table = dataSet.Tables[0];

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return dataMapper.Map<T>(table.Rows[0]);
        }
        
        private IEnumerable<T> MapProcedureEnumerable<T>(DataSet dataSet) where T : class, new()
        {
            if (dataSet.Tables.Count == 0)
            {
                return null;
            }

            return dataMapper.Map<T>(dataSet.Tables[0]);
        }

        private readonly IDataMapper dataMapper;
    }
}
