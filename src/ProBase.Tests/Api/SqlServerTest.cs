using NUnit.Framework;
using ProBase.Attributes;
using System.Data.SqlClient;

namespace ProBase.Tests.Api
{
    [TestFixture]
    public class SqlServerTest
    {
        [Test]
        public void CanGenerateInterfaceImplementation()
        {
            Assert.DoesNotThrow(() =>
            {
                DatabaseContext databaseContext = new DatabaseContext(CreateConnection());
                IDatabaseTestOperations testOperations = databaseContext.GenerateObject<IDatabaseTestOperations>();

                Assert.IsNotNull(testOperations, "The DatabaseContext should return an implementation of the given interface");
            },
            "The creation calls must be successful");
        }

        private SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "Address here"
            };

            return new SqlConnection(connectionStringBuilder.ToString());
        }

        [DatabaseInterface]
        public interface IDatabaseTestOperations
        {
            [Procedure("dbo.CreateItem")]
            void Create(object item);

            [Procedure("ReadItem", DatabaseSchema = "dbo")]
            object Read(int id);
        }
    }
}
