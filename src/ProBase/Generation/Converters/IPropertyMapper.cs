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
        /// <param name="row">The DataRow</param>
        /// <param name="entity">The object value</param>
        void Map<TEntity>(DataRow row, object entity) where TEntity: new();
    }
}
