using NUnit.Framework;
using ProBase.Attributes;
using System.Data;
using System.Data.SqlClient;

namespace ProBase.Tests.Api
{
    [Ignore("Database infrastructure not set up")]
    [TestFixture]
    public class SqlServerTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            configuration = TestHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);

            generationContext = new GenerationContext(CreateConnection());
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
            Assert.DoesNotThrow(() =>
            {
                IDatabaseTestOperations testOperations = generationContext.GenerateObject<IDatabaseTestOperations>();
                testOperations.Create("LastName", "FirstName", gender: 'f', age: 34, grade: 12);
            },
            "The create operation must be successful");
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
            Assert.DoesNotThrow(() =>
            {
                IDatabaseTestOperations testOperations = generationContext.GenerateObject<IDatabaseTestOperations>();
                testOperations.Update(id: 0, "LastName", "FirstName", gender: 'm', age: 19, grade: 11);
            },
            "The update operation must be successful");
        }
        
        [Test]
        public void CanDelete()
        {
            Assert.DoesNotThrow(() =>
            {
                IDatabaseTestOperations testOperations = generationContext.GenerateObject<IDatabaseTestOperations>();
                testOperations.Delete(id: 0);
            },
            "The delete operation must be successful");
        }

        private SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                ApplicationName = configuration.ApplicationName,
                DataSource = configuration.ServerAddress,
                InitialCatalog = configuration.DatabaseName,
                UserID = configuration.Username,
                Password = configuration.Password
            };

            TestContext.WriteLine(configuration.ServerAddress);

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

        private TestConfiguration configuration;

        private GenerationContext generationContext;
    }
}
