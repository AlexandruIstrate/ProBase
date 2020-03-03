using ProBase.Attributes;

namespace ProBase.Tests.Substitutes
{
    public class Student
    {
        [Column("IdElev", Serialization = SerializationBehavior.None)]
        public int Id { get; set; }

        [Column("Prenume")]
        public string FirstName { get; set; }

        [Column("Nume")]
        public string LastName { get; set; }

        [Column("Sex")]
        public string Gender { get; set; }

        [Column("Varsta")]
        public int Age { get; set; }

        [Column("Clasa")]
        public int Grade { get; set; }
    }
}
