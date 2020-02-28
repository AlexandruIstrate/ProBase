using System.Threading.Tasks;

namespace ProBase.Async
{
    /// <summary>
    /// Represents a parameter that can be used as an async out value.
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public class AsyncOut<TParameter>
    {
        /// <summary>
        /// A <see cref="System.Threading.Tasks.Task{TResult}"/> representing the parameter result.
        /// </summary>
        public TParameter ParameterValue { get; set; }
    }
}
