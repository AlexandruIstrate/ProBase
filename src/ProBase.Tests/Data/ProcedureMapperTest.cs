using System;
using System.Data.Common;
using NUnit.Framework;
using ProBase.Data;
using ProBase.Generation.Converters;

namespace ProBase.Tests.Data
{
    [Ignore("We don't yet have a database to test this with")]
    [TestFixture]
    public class ProcedureMapperTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            procedureMapper = new ProcedureMapper(GetConnection(), new DataSetMapper(new PropertyMapper()));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            procedureMapper.Dispose();
        }

        [Test]
        public void CanExecuteMappedProcedure()
        {
            MappedType mappedType = null;

            Assert.DoesNotThrow(() =>
            {
                mappedType = procedureMapper.ExecuteMappedProcedure<MappedType>(ProcedureName, new DbParameter[] { });
            },
            "The mapping operation must be successful");

            Assert.IsNotNull(mappedType, "The mapping result must be a non-null value");
        }

        [Test]
        public void CanExecuteMappedProcedureAsync()
        {
            MappedType mappedType = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                mappedType = await procedureMapper.ExecuteMappedProcedureAsync<MappedType>(ProcedureName, new DbParameter[] { });
            },
            "The mapping operation must be successful");

            Assert.IsNotNull(mappedType, "The mapping result must be a non-null value");
        }

        private DbConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        private const string ProcedureName = "procedure";

        private ProcedureMapper procedureMapper;

        internal class MappedType
        {
            public string Property1 { get; set; }

            public int Property2 { get; set; }
        }
    }
}
