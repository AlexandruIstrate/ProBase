using System.Threading.Tasks;

namespace ProBase.Async
{
    /// <summary>
    /// Represents a parameter that can be used as an async out value.
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public class AsyncOut<TParameter> : Task<TParameter>
    {
        public Task<TParameter> ResultTask { get; set; }

        public AsyncOut() : base(null)
        {
        }
    }
}
