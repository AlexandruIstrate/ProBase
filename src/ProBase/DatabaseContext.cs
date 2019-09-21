using ProBase.Generation;
using ProBase.Utils;
using System;
using System.Data;

namespace ProBase
{
    /// <summary>
    /// Represents and object that manages the creation of objects that run operations on databases.
    /// </summary>
    public class DatabaseContext
    {
        /// <summary>
        /// Gets or sets the connection object for this database.
        /// </summary>
        public IDbConnection Connection { get; set; }

        public DatabaseContext(IDbConnection connection)
        {
            Connection = Preconditions.CheckNotNull(connection, nameof(connection));
            classGenerator = ClassGeneratorFactory.Create();
        }

        /// <summary>
        /// Generates an instance of a class with automatic procedure calls based on method attributes.
        /// </summary>
        /// <typeparam name="T">The polymorphic type to generate</typeparam>
        /// <returns>An instance of the operation class</returns>
        public T GenerateObject<T>()
        {
            try
            {
                Type generatedType = classGenerator.GenerateClassImplementingInterface(typeof(T));
                return (T)Activator.CreateInstance(generatedType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private readonly IConcreteClassGenerator classGenerator;
    }
}
