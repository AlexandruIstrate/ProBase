using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Operations
{
    internal class DbParameterArrayGenerator : IArrayGenerator
    {
        public void Generate(ParameterInfo[] parameters, ILGenerator generator)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                // Load the argument at the given index
                generator.Emit(OpCodes.Ldarg, i);

                // Loads the field used for creating the parameter onto the stack
                generator.Emit(OpCodes.Ldfld, GetParameterFactoryType());

                // Calls the creation method on the field
                generator.Emit(OpCodes.Callvirt, GetParameterCreationMethod());

                // Unload the argument from the stack
                generator.Emit(OpCodes.Stloc, i);

                // Load the local variable associated with this parameter
                generator.Emit(OpCodes.Ldloc, i);

                // Set the parameter direction
                //generator.Emit(OpCodes.Ldc_I4, ParameterDirection.InputOutput);
            }
        }

        private Type GetParameterFactoryType()
        {
            return typeof(DbProviderFactory);
        }

        private MethodInfo GetParameterCreationMethod()
        {
            return typeof(DbProviderFactory).GetMethod(nameof(DbProviderFactory.CreateParameter));
        }
    }
}
