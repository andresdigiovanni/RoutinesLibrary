using System;

namespace RoutinesLibrary.Data
{
    /// <summary>
    /// A static class of extension methods for <see cref="Array"/>.
    /// </summary>
    /// <remarks>Based on: https://gist.github.com/ngbrown/443818/a947757556c1b716d84f634f2146474766cba8c9 </remarks>
    public static class ArrayHelper
    {
        /// <summary>
        /// Sets all values.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array that will be modified.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array</param>
        /// <param name="value">The value.</param>
        /// <returns>A reference to the changed array.</returns>
        public static T[] SetAllValues<T>(this T[] array, T value)
        {
            // Check arguments
            if (ReferenceEquals(array, null) || array.Length <= 0)
            {
                throw (new ArgumentNullException("array"));
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }

        /// <summary>
        /// Checks if the Arrays are equal.
        /// </summary>
        /// <typeparam name="T">Array type.</typeparam>
        /// <param name="first">The <see cref="Array"/> that contains data to compare with.</param>
        /// <param name="second">The <see cref="Array"/> that contains data to compare to.</param>
        /// <returns>
        /// Returns <c>true</c> if all element match and <c>false</c> otherwise.
        /// </returns>
        public static bool ArrayEqual<T>(this T[] first, T[] second) where T : IEquatable<T>
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            for (int i = 0; i < first.Length; i++)
            {
                if (!first[i].Equals(second[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Subs the array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array that will be modified.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="length">The length.</param>
        /// <returns>T[].</returns>
        public static T[] SubArray<T>(this T[] array, int index, int length)
        {
            // Check arguments
            if (ReferenceEquals(array, null) || array.Length <= 0)
            {
                throw (new ArgumentNullException("array"));
            }

            if (index < 0 || index > array.Length)
            {
                throw (new ArgumentOutOfRangeException("index"));
            }

            if (length < 1)
            {
                throw (new ArgumentOutOfRangeException("length"));
            }

            if (index + length > array.Length)
            {
                throw (new ArgumentOutOfRangeException("length"));
            }

            T[] result = new T[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }

        public static T[] Combine<T>(T[] first, T[] second)
        {
            T[] result = new T[first.Length + second.Length];
            Array.Copy(first, 0, result, 0, first.Length);
            Array.Copy(second, 0, result, first.Length, second.Length);
            return result;
        }
    }
}
