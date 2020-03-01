using NUnit.Framework;
using ProBase.Tests.Substitutes;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
                IDataOperations testOperations = CreateOperationsInterface();
                Assert.IsNotNull(testOperations, "The DatabaseContext must return an implementation of the given interface");
            },
            "The creation calls must be successful");
        }

        [Test]
        public void CanCreate()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                testOperations.Create("LastName", "FirstName", gender: 'f', age: 34, grade: 12);
            },
            "The create operation must be successful");
        }

        [Test]
        public void CanRead()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
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
                IDataOperations testOperations = CreateOperationsInterface();
                testOperations.Update(id: 1, "LastName", "FirstName", gender: 'm', age: 19, grade: 11);
            },
            "The update operation must be successful");
        }

        [Test]
        public void CanDelete()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                testOperations.Delete(id: 1);
            },
            "The delete operation must be successful");
        }

        [Test]
        public void CanReadMapped()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                Student student = testOperations.ReadMapped(id: 69);

                Assert.IsNotNull(student, "The Student returned must not be null");
            },
            "The mapped read operation must be successful");
        }

        [Test]
        public void CanReadAllMapped()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                IEnumerable<Student> students = testOperations.ReadAllMapped();

                Assert.IsNotNull(students, "The enumeration returned must not be null");
            },
            "The mapped read all operation must be successful");
        }

        [Test]
        public void CanCreateAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task task = testOperations.CreateAsync("LastName", "FirstName", gender: 'm', age: 19, grade: 11);
                Assert.IsNotNull(task, "The Task returned must not be null");

                await task;
            },
            "The async create operation must be successful");
        }

        [Test]
        public void CanReadAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task<DataSet> task = testOperations.ReadAsync(id: 33);
                Assert.IsNotNull(task, "The Task returned must not be null");

                DataSet dataSet = await task;
                Assert.IsNotNull(dataSet, "The DataSet returned must not be null");
            },
            "The async read operation must be successful");
        }

        [Test]
        public void CanReadAllAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task<DataSet> task = testOperations.ReadAllAsync();
                Assert.IsNotNull(task, "The Task returned must not be null");

                DataSet dataSet = await task;
                Assert.IsNotNull(dataSet, "The DataSet returned must not be null");
            },
            "The async read all operation must be successful");
        }

        [Test]
        public void CanUpdateAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task task = testOperations.UpdateAsync(id: 1, "LastName", "FirstName", gender: 'm', age: 19, grade: 11);
                Assert.IsNotNull(task, "The Task returned must not be null");

                await task;
            },
            "The async update operation must be successful");
        }

        [Test]
        public void CanDeleteAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task task = testOperations.DeleteAsync(id: 35);
                Assert.IsNotNull(task, "The Task returned must not be null");

                await task;
            },
            "The async delete operation must be successful");
        }

        [Test]
        public void CanReadMappedAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task<Student> task = testOperations.ReadMappedAsync(id: 69);
                Assert.IsNotNull(task, "The Task returned must not be null");

                Student student = await task;
                Assert.IsNotNull(student, "The Student returned must not be null");
            },
            "The mapped read operation must be successful");
        }

        [Test]
        public void CanReadAllMappedAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task<IEnumerable<Student>> task = testOperations.ReadAllMappedAsync();
                Assert.IsNotNull(task, "The Task returned must not be null");

                IEnumerable<Student> students = await task;
                Assert.IsNotNull(students, "The enumeration returned must not be null");
            },
            "The mapped read all operation must be successful");
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

            return new SqlConnection(connectionStringBuilder.ToString());
        }

        private IDataOperations CreateOperationsInterface()
        {
            return generationContext.GenerateObject<IDataOperations>();
        }

        private TestConfiguration configuration;
        private GenerationContext generationContext;
    }
}
