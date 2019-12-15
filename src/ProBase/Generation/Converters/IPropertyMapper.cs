using System;
using System.Data;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Maps values to primitive types;
    /// </summary>
    internal interface IPropertyMapper
    {
        /// <summary>
        /// Maps the values of a <see cref="System.Data.DataRow"/> to a type's properties.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="row">The DataRow</param>
        /// <param name="entity">The object value</param>
        /// <param name="ignoreColumnCase">Whether to ignore the casing of the column name</param>
        void Map<TEntity>(DataRow row, object entity, bool ignoreColumnCase = false) where TEntity: new();
    }
}
