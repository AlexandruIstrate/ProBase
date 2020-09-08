using NUnit.Framework;
using ProBase.Tests.Substitutes;
using System.Data;
using System.Threading.Tasks;

namespace ProBase.Tests.Api
{
    [Ignore("Database infrastructure not set up")]
    [TestFixture]
    public class AsyncProcedureTest : ProcedureTestBase
    {
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
    }
}
