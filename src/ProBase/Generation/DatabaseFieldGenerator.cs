using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation
{
    internal class DatabaseFieldGenerator : IClassFieldGenerator
    {
        public FieldBuilder GenerateField(string fieldName, Type fieldType, TypeBuilder typeBuilder)
        {
            FieldBuilder fieldBuilder = typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Private);

            return fieldBuilder;
        }
    }
}
