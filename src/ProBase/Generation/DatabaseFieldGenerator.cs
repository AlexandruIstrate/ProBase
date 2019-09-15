using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    internal class DatabaseFieldGenerator : IClassFieldGenerator
    {
        public FieldBuilder GenerateField(FieldInfo fieldInfo, TypeBuilder typeBuilder)
        {
            FieldBuilder fieldBuilder = typeBuilder.DefineField(fieldInfo.Name, fieldInfo.FieldType, FieldAttributes.Private);

            return fieldBuilder;
        }
    }
}
