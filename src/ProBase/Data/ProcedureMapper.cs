using ProBase.Generation.Converters;
using System;
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

        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters) where T : new()
        {
            return MapProcedure<T>(ExecuteScalarProcedure(procedureName, parameters));
        }

        public async Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : new()
        {
            return MapProcedure<T>(await ExecuteScalarProcedureAsync(procedureName, parameters));
        }

        private T MapProcedure<T>(DataSet dataSet) where T : new()
        {
            throw new NotImplementedException();
        }
        
        private IEnumerable<T> MapProcedureEnumerable<T>(DataSet dataSet) where T : new()
        {
            throw new NotImplementedException();
        }

        private readonly IDataMapper dataMapper;
    }
}
