using System.Data;

namespace ProBase
{
    public class DatabaseContext
    {
        public IDbConnection Connection { get; set; }

        public T GenerateClass<T>()
        {
            return default(T);
        }
    }
}
