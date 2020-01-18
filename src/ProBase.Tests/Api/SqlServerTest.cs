using NUnit.Framework;
using ProBase.Attributes;
using ProBase.Tests.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ProBase.Tests.Api
{
    [Ignore("Database infrastructure not set up")]
    [TestFixture]
    public class SqlServerTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Assert.DoesNotThrow(() =>
            {
                generationContext = new GenerationContext(CreateConnection());
            },
            "The creation of the context must be successful");
        }

        [Test]
        public void CanGenerateInterfaceImplementation()
        {
            Assert.DoesNotThrow(() =>
            {
                IDatabaseTestOperations testOperations = generationContext.GenerateObject<IDatabaseTestOperations>();
                Assert.IsNotNull(testOperations, "The DatabaseContext should return an implementation of the given interface");
            },
            "The creation calls must be successful");
        }

        [Test]
        public void CanCreate()
        {

        }

        [Test]
        public void CanRead()
        {
            Assert.DoesNotThrow(() =>
            {
                IDatabaseTestOperations testOperations = generationContext.GenerateObject<IDatabaseTestOperations>();
                DataSet dataSet = testOperations.Read();

                Assert.IsNotNull(dataSet, "The call must return a non-null DataSet");
            },
            "The read operation must be successful");
        }

        [Test]
        public void CanUpdate()
        {

        }
        
        [Test]
        public void CanDelete()
        {

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
            void Create([Parameter("Nume")] string lastName,
                        [Parameter("Prenume")] string firstName,
                        [Parameter("Sex")] char gender,
                        [Parameter("Varsta")] int age,
                        [Parameter("Clasa")] int grade);

            [Procedure("dbo.EleviRead")]
            DataSet Read();

            [Procedure("dbo.EleviUpdate")]
            void Update([Parameter("IdElev")] int id,
                        [Parameter("Nume")] string lastName,
                        [Parameter("Prenume")] string firstName,
                        [Parameter("Sex")] char gender,
                        [Parameter("Varsta")] int age,
                        [Parameter("Clasa")] int grade);

            [Procedure("dbo.EleviDelete")]
            void Delete([Parameter("IdElev")] int id);
        }

        private GenerationContext generationContext;
    }
}
