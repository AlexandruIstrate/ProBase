using System;
using System.Data;

namespace ProBase
{
    /// <summary>
    /// Represents the global object that manages operations on a database.
    /// </summary>
    public class DatabaseContext
    {
        /// <summary>
        /// Gets or sets the connection object for this database.
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// Generates a class with automatic procedure calls based on method attributes.
        /// </summary>
        /// <typeparam name="T">The polymorphic type to generate</typeparam>
        /// <returns>An instance of the operations class</returns>
        public T GenerateClass<T>()
        {
            throw new NotImplementedException();
        }
    }
}
