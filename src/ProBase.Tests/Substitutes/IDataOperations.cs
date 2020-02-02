using ProBase.Attributes;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ProBase.Tests.Substitutes
{
    [DbInterface]
    public interface IDataOperations
    {
        // Default Procedures

        [Procedure("dbo.EleviCreate")]
        void Create([Parameter("Nume")] string lastName,
                    [Parameter("Prenume")] string firstName,
                    [Parameter("Sex")] char gender,
                    [Parameter("Varsta")] int age,
                    [Parameter("Clasa")] int grade);

        [Procedure("dbo.EleviRead")]
        DataSet Read();

        [Procedure("dbo.EleviUpdate")]
        void Update([Parameter("IdElev")] int id,
                    [Parameter("Nume")] string lastName,
                    [Parameter("Prenume")] string firstName,
                    [Parameter("Sex")] char gender,
                    [Parameter("Varsta")] int age,
                    [Parameter("Clasa")] int grade);

        [Procedure("dbo.EleviDelete")]
        void Delete([Parameter("IdElev")] int id);

        // Mapped Procedures

        [Procedure("dbo.EleviRead")]
        Student ReadMapped([Parameter("IdElev")] int id);

        [Procedure("dbo.EleviRead")]
        IEnumerable<Student> ReadAllMapped();

        // Async Procedures

        [Procedure("dbo.EleviRead")]
        Task<Student> ReadMappedAsync([Parameter("IdElev")] int id);

        [Procedure("dbo.EleviRead")]
        Task<IEnumerable<Student>> ReadAllMappedAsync();
    }
}
