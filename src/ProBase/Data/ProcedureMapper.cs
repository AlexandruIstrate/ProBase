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

        public T ExecuteMappedProcedure<T>(string procedureName, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteMappedProcedureAsync<T>(string procedureName, params DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
