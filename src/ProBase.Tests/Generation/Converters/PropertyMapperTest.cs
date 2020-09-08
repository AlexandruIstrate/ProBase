using NUnit.Framework;
using ProBase.Generation.Converters;
using ProBase.Tests.Substitutes;

namespace ProBase.Tests.Generation.Converters
{
    [TestFixture]
    public class PropertyMapperTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            propertyMapper = new PropertyMapper();
        }

        [Test]
        public void CanMapProperty()
        {
            Assert.DoesNotThrow(() =>
            {
                Student writer = new Student();

                propertyMapper.Map<Student>(StudentFactory.CreateDataRow(StudentFactory.CreateStudent()), writer);

                Assert.NotNull(writer.FirstName, "The FirstName property must not be null");
                Assert.NotNull(writer.LastName, "The LastName property must not be null");
            },
            "The map operation must be successful");
        }

        private PropertyMapper propertyMapper;
    }
}
