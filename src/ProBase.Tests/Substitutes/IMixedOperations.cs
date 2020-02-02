using ProBase.Attributes;

namespace ProBase.Tests.Substitutes
{
    [DbInterface]
    public interface IMixedOperations
    {
        [Procedure("TestProcedure")]
        void TestMethod(string param1, int param2);
    }
}
