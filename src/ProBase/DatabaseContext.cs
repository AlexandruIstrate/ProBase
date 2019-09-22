using ProBase.Data;
using ProBase.Generation;
using ProBase.Utils;
using System;
using System.Data.Common;

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
        public DbConnection Connection { get; set; }

        public DatabaseContext(DbConnection connection)
        {
            Connection = Preconditions.CheckNotNull(connection, nameof(connection));
            classGenerator = ClassGeneratorFactory.Create();
        }

        /// <summary>
        /// Generates an instance of a class with automatic procedure calls based on method attributes.
        /// </summary>
        /// <typeparam name="T">The polymorphic type to generate</typeparam>
        /// <returns>An instance implementing the passed in interface type</returns>
        public T GenerateObject<T>()
        {
            try
            {
                Type generatedType = classGenerator.GenerateClassImplementingInterface(typeof(T));
                return (T)Activator.CreateInstance(generatedType, ProcedureMapperFactory.Create(Connection));
            }
            catch (Exception e)
            {
                throw new OperationMappingException("The database operations class could not be created", e);
            }
        }

        private readonly IConcreteClassGenerator classGenerator;
    }
}
