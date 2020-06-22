using ProBase.Data;
using ProBase.Generation.Class;
using ProBase.Generation.Converters;
using ProBase.Utils;
using System;
using System.Data.Common;

namespace ProBase
{
    /// <summary>
    /// Represents and object that manages the creation of objects that run operations on databases.
    /// </summary>
    public class GenerationContext : IGenerationContext
    {
        /// <summary>
        /// Gets or sets the connection object for this database.
        /// </summary>
        public DbConnection Connection { get; set; }

        public GenerationContext(DbConnection connection)
        {
            Connection = Preconditions.CheckNotNull(connection, nameof(connection));
            classGenerator = ClassGeneratorFactory.Create();
        }

        /// <summary>
        /// Generates an instance of a class with automatic procedure calls based on method attributes.
        /// </summary>
        /// <typeparam name="T">The polymorphic type to generate</typeparam>
        /// <returns>An instance implementing the passed in interface type</returns>
        public T GenerateOperations<T>()
        {
            try
            {
                Type generatedType = classGenerator.GenerateClassImplementingInterface(typeof(T));
                return (T)Activator.CreateInstance(generatedType, GetProcedureMapper(), GetProviderFactory());
            }
            catch (Exception e)
            {
                throw new OperationMappingException("The database operations class could not be created", e);
            }
        }


        private IProcedureMapper GetProcedureMapper() => new ProcedureMapper(Connection, new DataSetMapper(new PropertyMapper()));
        private DbProviderFactory GetProviderFactory() => Connection.GetProviderFactory();

        private readonly IConcreteClassGenerator classGenerator;
    }
}
