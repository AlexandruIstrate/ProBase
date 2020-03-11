using NUnit.Framework;
using ProBase.Tests.Substitutes;
using System.Data.SqlClient;

namespace ProBase.Tests.Api
{
    public class ProcedureTestBase
    {
        public TestConfiguration Configuration { get; private set; }

        public GenerationContext Context { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Configuration = TestHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
            Context = new GenerationContext(CreateConnection());
        }

        protected SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                ApplicationName = Configuration.ApplicationName,
                DataSource = Configuration.ServerAddress,
                InitialCatalog = Configuration.DatabaseName,
                UserID = Configuration.Username,
                Password = Configuration.Password
            };

            return new SqlConnection(connectionStringBuilder.ToString());
        }

        protected IDataOperations CreateOperationsInterface()
        {
            return Context.GenerateObject<IDataOperations>();
        }
    }
}
