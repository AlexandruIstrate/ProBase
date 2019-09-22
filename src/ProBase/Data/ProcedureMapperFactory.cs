using System.Data.Common;

namespace ProBase.Data
{
    /// <summary>
    /// Provides a way for creating instances of <see cref="ProBase.Data.IProcedureMapper"/>.
    /// </summary>
    internal static class ProcedureMapperFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ProBase.Data.IProcedureMapper"/>.
        /// </summary>
        /// <returns>A procedure mapper instance</returns>
        public static IProcedureMapper Create(DbConnection connection)
        {
            return new ProcedureMapper(connection);
        }
    }
}
