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

        /// <summary>
        /// Checks if a given argument is in the given range.
        /// </summary>
        /// <param name="paramName">The name of the parameter</param>
        /// <param name="value">The current value of the parameter</param>
        /// <param name="minInclusive">The minimum, inclusive value the parameter must have</param>
        /// <param name="maxInclusive">The maximum, inclusive value the parameter must have</param>
        internal static void CheckArgumentRange(string paramName, int value, int minInclusive, int maxInclusive)
        {
            if (value < minInclusive || value > maxInclusive)
            {
                ThrowArgumentOutOfRangeException(paramName, value, minInclusive, maxInclusive);
            }
        }

        /// <summary>
        /// Checks if a given argument is in the given range.
        /// </summary>
        /// <param name="paramName">The name of the parameter</param>
        /// <param name="value">The current value of the parameter</param>
        /// <param name="minInclusive">The minimum, inclusive value the parameter must have</param>
        /// <param name="maxInclusive">The maximum, inclusive value the parameter must have</param>
        internal static void CheckArgumentRange(string paramName, long value, long minInclusive, long maxInclusive)
        {
            if (value < minInclusive || value > maxInclusive)
            {
                ThrowArgumentOutOfRangeException(paramName, value, minInclusive, maxInclusive);
            }
        }

        /// <summary>
        /// Checks if a given argument is in the given range.
        /// </summary>
        /// <param name="paramName">The name of the parameter</param>
        /// <param name="value">The current value of the parameter</param>
        /// <param name="minInclusive">The minimum, inclusive value the parameter must have</param>
        /// <param name="maxInclusive">The maximum, inclusive value the parameter must have</param>
        internal static void CheckArgumentRange(string paramName, double value, double minInclusive, double maxInclusive)
        {
            if (value < minInclusive || value > maxInclusive || double.IsNaN(value))
            {
                ThrowArgumentOutOfRangeException(paramName, value, minInclusive, maxInclusive);
            }
        }

        private static void ThrowArgumentOutOfRangeException<T>(string paramName, T value, T minInclusive, T maxInclusive)
        {
            throw new ArgumentOutOfRangeException(paramName, value,
                $"Value should be in range [{minInclusive}-{maxInclusive}]");
        }
    }
}
