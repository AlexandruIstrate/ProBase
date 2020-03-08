using NUnit.Framework;
using ProBase.Tests.Substitutes;
using System.Data;

namespace ProBase.Tests.Api
{
    [Ignore("Database infrastructure not set up")]
    [TestFixture]
    public class SimpleProcedureTest : ProcedureTestBase
    {
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
    }
}
