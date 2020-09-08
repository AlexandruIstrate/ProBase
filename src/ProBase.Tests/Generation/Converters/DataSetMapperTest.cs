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
            DataRow dataRow = StudentFactory.CreateDataRow(testStudent);

            Assert.DoesNotThrow(() =>
            {
                Student student = dataSetMapper.Map<Student>(dataRow);

                Assert.NotNull(student, "The mapping must return a non-null value");

                Assert.NotNull(student.FirstName, "The FirstName must be not-null");
                Assert.NotNull(student.LastName, "The LastName must be not-null");

                Assert.AreEqual(testStudent.FirstName, student.FirstName, "The FirstName must be equal to the row's value");
                Assert.AreEqual(testStudent.LastName, student.LastName, "The LastName must be equal to the row's value");
                Assert.AreEqual(testStudent.Age, student.Age, "The Age must be equal to the row's value");
            },
            "The map operation on the DataRow must be successful");
        }

        [Test]
        public void CanMapDataTable()
        {
            DataTable dataTable = StudentFactory.CreateDataTable(StudentFactory.CreateStudentList());

            Assert.DoesNotThrow(() =>
            {
                IEnumerable<Student> writers = dataSetMapper.Map<Student>(dataTable);

                Assert.NotNull(writers, "The mapping must return a non-null value");
                Assert.Greater(arg1: writers.Count(), arg2: 0, "The mapping must return a non-empty Enumerable");

                foreach (Student writer in writers)
                {
                    Assert.NotNull(writer, "The value in the Enumerable must not be null");

                    Assert.NotNull(writer.FirstName, "The FirstName must be not-null");
                    Assert.NotNull(writer.LastName, "The LastName must be not-null");
                }
            },
            "The map operation on the DataTable must be successful");
        }

        private readonly Student testStudent = StudentFactory.CreateStudent();

        private DataSetMapper dataSetMapper;
    }
}
