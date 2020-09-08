using System.Collections.Generic;
using System.Data;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides operations for mapping database primitives to objects.
    /// </summary>
    internal interface IDataMapper
    {
        /// <summary>
        /// Maps a row to an object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the object to map to</typeparam>
        /// <param name="row">The row entry to use</param>
        /// <returns>The mapped object</returns>
        TEntity Map<TEntity>(DataRow row) where TEntity : class, new();

        /// <summary>
        /// Maps a table to an enumeration of objects.
        /// </summary>
        /// <typeparam name="TEntity">The type of the object to map to</typeparam>
        /// <param name="table"></param>
        /// <returns>An enumeration of mapped objects</returns>
        IEnumerable<TEntity> Map<TEntity>(DataTable table) where TEntity : class, new();
    }
}