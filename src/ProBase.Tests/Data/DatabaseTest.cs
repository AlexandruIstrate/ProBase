using NUnit.Framework;
using ProBase.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ProBase.Tests.Data
{
    [Ignore("We don't yet have a database to test this with")]
    [TestFixture]
    public class DatabaseTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            database = new Database(CreateConnection());
        }

        [Test]
        public void CanExecuteNonQueryProcedure()
        {
            Assert.DoesNotThrow(() =>
            {
                database.ExecuteNonQueryProcedure("", new DbParameter[] { });
            },
            "The procedure call must be successful");
        }

        [Test]
        public void CanExecuteNonQueryProcedureAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await database.ExecuteNonQueryProcedureAsync("", new DbParameter[] { });
            },
            "The procedure call must be successful");
        }

        [Test]
        public void CanExecuteScalarProcedure()
        {
            DataSet dataSet = null;

            Assert.DoesNotThrow(() =>
            {
                dataSet = database.ExecuteScalarProcedure("", new DbParameter[] { });
            },
            "The procedure call must be successful");

            Assert.IsNotNull(dataSet, "The procedure call must result in a non-null DataSet");
        }

        [Test]
        public void CanExecuteScalarProcedureAsync()
        {
            DataSet dataSet = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                dataSet = await database.ExecuteScalarProcedureAsync("", new DbParameter[] { });
            },
            "The procedure call must be successful");

            Assert.IsNotNull(dataSet, "The procedure call must result in a non-null DataSet");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            database.Dispose();
        }

        private SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
            return new SqlConnection(connectionBuilder.ToString());
        }

        private Database database;
    }
}
