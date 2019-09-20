﻿using System;
using System.Data;

namespace ProBase.Generation.Converters
{
    /// <summary>
    /// Maps a <see cref="System.Data.DataSet"/> to an object that has the same properties as the columns in the DataSet.
    /// </summary>
    internal class DataSetMapper : IDataMapper
    {
        public DataSet MappedDataSet
        {
            get => mappedDataSet;
            set => mappedDataSet = value ?? throw new ArgumentNullException(nameof(MappedDataSet));
        }

        public DataSetMapper(DataSet mappedDataSet)
        {
            MappedDataSet = mappedDataSet ?? throw new ArgumentNullException(nameof(mappedDataSet));
        }

        public T MapToObject<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public bool CanBeRepresentedAs<T>()
        {
            throw new NotImplementedException();
        }

        private DataSet mappedDataSet;
    }
}
