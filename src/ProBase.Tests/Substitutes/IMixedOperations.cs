using ProBase.Attributes;
using System;

namespace ProBase.Tests.Substitutes
{
    [DbInterface]
    public interface IMixedOperations
    {
        [Procedure("TestProcedure")]
        void TestMethod(string param1, int param2);

        [Procedure("DateTest")]
        void DateTest([Parameter("Date")] DateTime date);

        [Procedure("EnumTest")]
        void EnumTest([Parameter("comparison")] StringComparison comparison);
    }
}
