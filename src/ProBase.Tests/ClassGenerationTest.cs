using NUnit.Framework;
using ProBase.Attributes;

namespace ProBase.Tests
{
    [TestFixture]
    public class ClassGenerationTest
    {
        //[Test]
        //public void CanGenerateInterfaceImplementation()
        //{
        //    Assert.DoesNotThrow(() =>
        //    {
        //        DatabaseContext databaseContext = new DatabaseContext();
        //        IDatabaseTestOperations testOperations = databaseContext.GenerateClass<IDatabaseTestOperations>();

        //        Assert.IsNotNull(testOperations, "The DatabaseContext should return an implementation of the given interface");

        //        Assert.DoesNotThrow(() =>
        //        {
        //            testOperations.Create("Hello, World!");
        //        }, "The database operations must be successful");
        //    }, "The creation calls must be successful");
        //}

        [DatabaseInterface]
        public interface IDatabaseTestOperations
        {
            [Procedure("dbo.CreateItem")]
            void Create(string item);
        }
    }
}
