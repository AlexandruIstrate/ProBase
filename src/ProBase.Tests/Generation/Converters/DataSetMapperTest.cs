using NUnit.Framework;
using ProBase.Generation.Converters;
using ProBase.Tests.Substitutes;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProBase.Tests.Generation.Converters
{
    [TestFixture]
    public class DataSetMapperTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            dataSetMapper = new DataSetMapper(new PropertyMapper());
        }

        [Test]
        public void CanMapDataRow()
        {
            DataRow dataRow = SubstituteFactory.CreateDataRow(testWriter);

            Assert.DoesNotThrow(() =>
            {
                FamousWriter writer = dataSetMapper.Map<FamousWriter>(dataRow);

                Assert.NotNull(writer, "The mapping must return a non-null value");

                Assert.NotNull(writer.FirstName, "The FirstName must be not-null");
                Assert.NotNull(writer.LastName, "The LastName must be not-null");

                Assert.AreEqual(testWriter.FirstName, writer.FirstName, "The FirstName must be equal to the row's value");
                Assert.AreEqual(testWriter.LastName, writer.LastName, "The LastName must be equal to the row's value");
                Assert.AreEqual(testWriter.Age, writer.Age, "The Age must be equal to the row's value");
            },
            "The map operation on the DataRow must be successful");
        }

        [Test]
        public void CanMapDataTable()
        {
            DataTable dataTable = SubstituteFactory.CreateDataTable(SubstituteFactory.CreateWriterList());

            Assert.DoesNotThrow(() =>
            {
                IEnumerable<FamousWriter> writers = dataSetMapper.Map<FamousWriter>(dataTable);

                Assert.NotNull(writers, "The mapping must return a non-null value");
                Assert.Greater(arg1: writers.Count(), arg2: 0, "The mapping must return a non-empty Enumerable");

                foreach (FamousWriter writer in writers)
                {
                    Assert.NotNull(writer, "The value in the Enumerable must not be null");

                    Assert.NotNull(writer.FirstName, "The FirstName must be not-null");
                    Assert.NotNull(writer.LastName, "The LastName must be not-null");
                }
            },
            "The map operation on the DataTable must be successful");
        }

        private readonly FamousWriter testWriter = SubstituteFactory.CreateWriter();

        private DataSetMapper dataSetMapper;
    }
}
