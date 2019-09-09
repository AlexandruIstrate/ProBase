using System;
using System.Data;

namespace ProBase.Generation.Converters
{
    internal class DataSetMapper : IDataMapper
    {
        public DataSet MappedDataSet { get; set; }

        public DataSetMapper(DataSet mappedDataSet)
        {
            MappedDataSet = mappedDataSet ?? throw new ArgumentNullException(nameof(mappedDataSet));
        }

        public T MapToObject<T>()
        {
            throw new NotImplementedException();
        }

        public bool CanBeRepresentedAs<T>()
        {
            throw new NotImplementedException();
        }
    }
}
