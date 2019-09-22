using ProBase.Attributes;

namespace ProBase.Tests.Generation
{
    [DatabaseInterface]
    public interface IGenerationTestInterface
    {
        [Procedure("TestProcedure")]
        void TestMethod(string param1, int param2);
    }
}
