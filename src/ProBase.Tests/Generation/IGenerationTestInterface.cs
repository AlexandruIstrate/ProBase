using ProBase.Attributes;

namespace ProBase.Tests.Generation
{
    [DbInterface]
    public interface IGenerationTestInterface
    {
        [Procedure("TestProcedure")]
        void TestMethod(string param1, int param2);
    }
}
