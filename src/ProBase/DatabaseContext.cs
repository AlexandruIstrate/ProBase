using ProBase.Generation;
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

        public DatabaseContext(IDbConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Generates a class with automatic procedure calls based on method attributes.
        /// </summary>
        /// <typeparam name="T">The polymorphic type to generate</typeparam>
        /// <returns>An instance of the operations class</returns>
        public T GenerateClass<T>()
        {
            Type generatedType = classGenerator.GenerateClassImplementingInterface(typeof(T));
            return (T)Activator.CreateInstance(generatedType);
        }

        private readonly IConcreteClassGenerator classGenerator;
    }
}
