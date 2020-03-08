using NUnit.Framework;
using ProBase.Tests.Substitutes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProBase.Tests.Api
{
    [Ignore("Database infrastructure not set up")]
    [TestFixture]
    public class MappedProcedureTest : ProcedureTestBase
    {
        // Mapped Results

        [Test]
        public void CanReadMapped()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                Student student = testOperations.ReadMapped(id: 69);

                Assert.IsNotNull(student, "The Student returned must not be null");
            },
            "The mapped read operation must be successful");
        }

        [Test]
        public void CanReadAllMapped()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                IEnumerable<Student> students = testOperations.ReadAllMapped();

                Assert.IsNotNull(students, "The enumeration returned must not be null");
            },
            "The mapped read all operation must be successful");
        }

        [Test]
        public void CanReadMappedAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task<Student> task = testOperations.ReadMappedAsync(id: 69);
                Assert.IsNotNull(task, "The Task returned must not be null");

                Student student = await task;
                Assert.IsNotNull(student, "The Student returned must not be null");
            },
            "The mapped read operation must be successful");
        }

        [Test]
        public void CanReadAllMappedAsync()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                IDataOperations testOperations = CreateOperationsInterface();

                Task<IEnumerable<Student>> task = testOperations.ReadAllMappedAsync();
                Assert.IsNotNull(task, "The Task returned must not be null");

                IEnumerable<Student> students = await task;
                Assert.IsNotNull(students, "The enumeration returned must not be null");
            },
            "The mapped read all operation must be successful");
        }

        // Mapped Parameters

        [Test]
        public void CanCreateWithMappedParameters()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                testOperations.Create(new Student
                {
                    FirstName = "CompoundTypeTest",
                    LastName = "LastName",
                    Age = 28,
                    Gender = "M",
                    Grade = 12
                });
            },
            "The create operation with mapped parameters must be successful");
        }

        [Test]
        public void CanUpdateWithMappedParameters()
        {
            Assert.DoesNotThrow(() =>
            {
                IDataOperations testOperations = CreateOperationsInterface();
                testOperations.Update(id: 55, new Student
                {
                    FirstName = "CompoundTypeTest - Updated",
                    LastName = "LastName - Updated",
                    Age = 29,
                    Gender = "F",
                    Grade = 11
                });
            },
            "The update operation with mapped parameters must be successful");
        }
    }
}
