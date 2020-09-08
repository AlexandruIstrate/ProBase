using System;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Provides a way for creating instances of <see cref="ProBase.Generation.Converters.IDataMapper"/>.
    /// </summary>
    internal static class DataMapperFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ProBase.Generation.Converters.IDataMapper"/>.
        /// </summary>
        /// <param name="type">The type of <see cref="ProBase.Generation.Converters.IDataMapper"/> to create</param>
        /// <returns>An instance of <see cref="ProBase.Generation.Converters.IDataMapper"/></returns>
        public static IDataMapper Create(DataMapperType type)
        {
            switch (type)
            {
                case DataMapperType.DataSet:
                    return new DataSetMapper(new PropertyMapper());
                default:
                    throw new NotSupportedException("The factory does not support the given type");
            }
        }
    }

    /// <summary>
    /// Specifies a type of <see cref="ProBase.Generation.Converters.IDataMapper"/>.
    /// </summary>
    internal enum DataMapperType
    {
        /// <summary>
        /// Represents a mapper used for <see cref="System.Data.DataSet"/> objects.
        /// </summary>
        DataSet
    }
}
