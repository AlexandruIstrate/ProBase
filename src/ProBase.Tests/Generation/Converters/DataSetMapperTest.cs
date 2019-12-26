using NUnit.Framework;
using ProBase.Generation.Converters;
using System;
using System.Data;
using System.Text;

namespace ProBase.Tests.Generation.Converters
{
    [Ignore("Not finished")]
    [TestFixture]
    public class DataSetMapperTest
    {
        [Test]
        public void CanMapDataRow()
        {
            DataRow dataRow = CreateDataRow();

            Assert.DoesNotThrow(() =>
            {
                dataSetMapper.Map<StringBuilder>(dataRow);
            },
            "The map operation must be successful");
        }

        [Test]
        public void CanMapDataTable()
        {
            DataTable dataTable = CreateDataTable();

            Assert.DoesNotThrow(() =>
            {
                dataSetMapper.Map<StringBuilder>(dataTable);
            },
            "The map operation must be successful");
        }

        private DataRow CreateDataRow()
        {
            throw new NotImplementedException();
        }

        private DataTable CreateDataTable()
        {
            throw new NotImplementedException();
        }

        private DataSetMapper dataSetMapper;
    }
}
