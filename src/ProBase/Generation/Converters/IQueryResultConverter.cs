using System.Data;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides an operation to convert between the representation of data in a database and a structured, object oriented representation.
    /// </summary>
    internal interface IQueryResultConverter
    {
        /// <summary>
        /// Converts a <see cref="System.Data.DataSet"/> to an object.
        /// </summary>
        /// <typeparam name="T">The type of the class, struct or enum to convert to</typeparam>
        /// <param name="dataSet">The dataset where the database data is stored</param>
        /// <returns></returns>
        T ConvertResult<T>(DataSet dataSet);
    }
}
