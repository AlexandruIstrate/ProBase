using NUnit.Framework;
using ProBase.Attributes;
using ProBase.Tests.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

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
                GenerationContext databaseContext = new GenerationContext(CreateConnection());
                IDatabaseTestOperations testOperations = databaseContext.GenerateObject<IDatabaseTestOperations>();

                Assert.IsNotNull(testOperations, "The DatabaseContext should return an implementation of the given interface");
            },
            "The creation calls must be successful");
        }

        private SqlConnection CreateConnection()
        {
            Settings config = Settings.Default;

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name,
                DataSource = config.ServerAddress,
                InitialCatalog = config.DatabaseName,
                UserID = config.Username,
                Password = config.Password
            };

            return new SqlConnection(connectionStringBuilder.ToString());
        }

        [DbInterface]
        public interface IDatabaseTestOperations
        {
            [Procedure("dbo.EleviCreate")]
            void Create([Parameter("Prenume")] string firstName,
                        [Parameter("Nume")] string lastName,
                        [Parameter("Sex")] char gender,
                        [Parameter("Varsta")] int age,
                        [Parameter("Clasa")] int grade);

            [Procedure("dbo.EleviRead")]
            DataSet Read();
        }
    }
}
