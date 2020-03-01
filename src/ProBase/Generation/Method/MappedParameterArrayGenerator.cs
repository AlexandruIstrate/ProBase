using ProBase.Generation.Converters;
using ProBase.Utils;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Generation.Method
{
    internal class MappedParameterArrayGenerator : ParameterArrayGenerator
    {
        public MappedParameterArrayGenerator(IParameterConverter<DbParameter[]> parameterConverter) : base(null)
        {
            this.parameterConverter = parameterConverter;
        }

        public override LocalBuilder Generate(ParameterInfo[] parameters, FieldInfo[] fields, ILGenerator generator)
        {
            // Get the provider factory field
            FieldInfo providerFactory = ClassUtils.GetField<DbProviderFactory>(fields, GenerationConstants.ProviderFactoryFieldName);

            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter[] mappedParameters = parameterConverter.ConvertParameter(parameters[i], value: null);

                foreach (DbParameter dbParameter in mappedParameters)
                {
                    // Create the parameter
                    LocalBuilder parameterBuilder = CreateParameter(providerFactory, generator);

                    // Set the name for this parameter
                    SetParameterName(parameterBuilder, dbParameter.ParameterName, generator);

                    // Set the direction for this parameter
                    SetParameterDirection(parameterBuilder, dbParameter.Direction, generator);

                    // Set the parameter value
                    // TODO: Fix
                    SetParameterValue(parameterBuilder, valueIndex: i + 1, null, generator);

                    // Set the parameter size
                    SetParameterSize(parameterBuilder, parameters[i], generator); 
                }
            }

            // Create the array
            return CreateArray(typeof(DbParameter[]), parameters.Length, generator);
        }

        private readonly IParameterConverter<DbParameter[]> parameterConverter;
    }
}
