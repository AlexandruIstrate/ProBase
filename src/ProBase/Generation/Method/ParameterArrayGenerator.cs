using ProBase.Utils;
using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    /// <summary>
    /// Generates an array of parameters used for calling database procedures.
    /// </summary>
    internal class ParameterArrayGenerator : ICollectionGenerator
    {
        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="defaultGenerator">The generator used for default parameter generation</param>
        /// <param name="compoundTypeGenerator">The generator used for generating compound types</param>
        public ParameterArrayGenerator(IParameterGenerator defaultGenerator, IParameterGenerator compoundTypeGenerator)
        {
            this.defaultGenerator = defaultGenerator;
            this.compoundTypeGenerator = compoundTypeGenerator;
        }

        public virtual ParameterCollection Generate(ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator)
        {
            ParameterCollection collection = new ParameterCollection();

            // The number of effective parameters after unmapping
            int parameterCount = 0;

            // Get the provider factory field
            FieldInfo providerFactory = ClassUtils.GetField<DbProviderFactory>(fields, GenerationConstants.ProviderFactoryFieldName);

            foreach (ParameterInfo parameter in parameters)
            {
                LocalBuilder[] localParams = GetGenerator(parameter.ParameterType).Generate(parameter, providerFactory, generator);
                parameterCount += localParams.Length;

                if (localParams.Length > 0)
                {
                    // Set the local for the given parameter
                    collection[parameter] = localParams.First();
                }
            }

            // Set the local to the array
            collection.CollectionLocal = CreateArray(typeof(DbParameter[]), parameterCount, generator);

            return collection;
        }

        protected virtual LocalBuilder CreateArray(Type type, int length, ILGenerator generator)
        {
            LocalBuilder localBuilder = generator.DeclareLocal(type);

            // Load the array size onto the evaluation stack
            generator.Emit(OpCodes.Ldc_I4, length);

            // Create the array
            generator.Emit(OpCodes.Newobj, GetArrayConstructor(type));

            // Store the newly created value into the local
            generator.Emit(OpCodes.Stloc, localBuilder);

            for (int i = 0; i < length; i++)
            {
                // Load the local array
                generator.Emit(OpCodes.Ldloc, localBuilder);

                // Load the array index onto the stack
                generator.Emit(OpCodes.Ldc_I4, i);

                // Loads the local parameter at index
                generator.Emit(OpCodes.Ldloc, i);

                // Set the array element at index to the current local value
                generator.Emit(OpCodes.Stelem_Ref);
            }

            return localBuilder;
        }

        // TODO: Refactor this to a factory!!!
        private IParameterGenerator GetGenerator(Type type)
        {
            if (type.IsUserDefined())
            {
                return compoundTypeGenerator;
            }

            return defaultGenerator;
        }

        private ConstructorInfo GetArrayConstructor(Type arrayType)
        {
            return arrayType.GetConstructor(new[] { typeof(int) });
        }

        private readonly IParameterGenerator defaultGenerator;
        private readonly IParameterGenerator compoundTypeGenerator;
    }
}
