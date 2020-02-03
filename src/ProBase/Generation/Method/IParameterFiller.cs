using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    internal interface IParameterFiller
    {
        void Fill(ParameterInfo parameter, int dbParameter, ILGenerator generator);
    }
}
