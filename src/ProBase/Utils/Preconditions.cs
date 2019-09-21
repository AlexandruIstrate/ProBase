using System;

namespace ProBase.Utils
{
    /// <summary>
    /// Provides helper methods for argument/state validation.
    /// </summary>
    internal static class Preconditions
    {
        /// <summary>
        /// Returns the given argument after checking whether it's null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        internal static T CheckNotNull<T>(T argument, string parameterName) where T : class
        {
            if (argument is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return argument;
        }
    }
}
