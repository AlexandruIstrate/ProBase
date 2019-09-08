using NUnit.Framework;

namespace ProBase.Tests
{
    [TestFixture]
    public class ApiTest
    {
        [Test]
        public void Test()
        {
            DatabaseContext context = new DatabaseContext(connection);
            IDatabaseProcedures procedures = context.GenerateCalls();
        }
    }
}
