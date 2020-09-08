using NUnit.Framework;
using ProBase.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ProBase.Tests.Data
{
    [TestFixture]
    public class DbExtensionsTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            connection = new SqlConnection();
        }

        [Test]
        public void CanGetProviderFactory()
        {
            DbProviderFactory providerFactory = null;

            Assert.DoesNotThrow(() =>
            {
                providerFactory = connection.GetProviderFactory();
            },
            "The get call must be successful");

            Assert.NotNull(providerFactory, "The method should return a non-null value");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            connection.Dispose();
        }

        private SqlConnection connection;
    }
}
