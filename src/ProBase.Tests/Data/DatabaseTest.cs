using NUnit.Framework;
using ProBase.Data;
using System.Data.SqlClient;

namespace ProBase.Tests.Data
{
    [TestFixture]
    public class DatabaseTest
    {
        [OneTimeSetUp]
        public void GeneralSetUp()
        {
            database = new Database(CreateConnection());
        }

        //[Test]
        //public void CanExecuteNonQueryProcedure()
        //{
        //    Assert.DoesNotThrow(() =>
        //    {
        //        database.ExecuteNonQueryProcedure("", new DbParameter[] { });
        //    }, "The procedure call must be successful");
        //}

        [OneTimeTearDown]
        public void GeneralTearDown()
        {
            database.Dispose();
        }

        private SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
            return new SqlConnection(connectionBuilder.ToString());
        }

        private IDatabase database;
    }
}
