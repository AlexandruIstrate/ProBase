using ProBase.Utils;
using System;
using System.Data;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Maps a <see cref="System.Data.DataSet"/> to an object that has the same properties as the columns in the DataSet.
    /// </summary>
    internal class DataSetMapper : IDataMapper
    {
        /// <summary>
        /// Tests if the given <see cref="System.Data.DataSet"/> object can be mapped to the given type.
        /// </summary>
        /// <typeparam name="T">The type to check the mapping for</typeparam>
        /// <param name="obj">The object to check</param>
        /// <returns>True if the mapping succeeds, false otherwise</returns>
        public bool CanBeRepresentedAs<T>(object obj)
        {
            DataSet dataSet = Preconditions.CheckIsType<DataSet>(obj, nameof(obj));
            throw new NotImplementedException();
        }

        /// <summary>
        /// Maps a given <see cref="System.Data.DataSet"/> to a type.
        /// </summary>
        /// <typeparam name="T">The type to map to</typeparam>
        /// <param name="obj">The object to map</param>
        /// <returns>A mapped object</returns>
        public T MapToObject<T>(object obj) where T : new()
        {
            // TODO: Support compound types
            DataSet dataSet = Preconditions.CheckIsType<DataSet>(obj, nameof(obj));
            throw new NotImplementedException();
        }
    }
}
