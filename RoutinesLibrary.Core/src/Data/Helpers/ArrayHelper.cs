using System;

namespace RoutinesLibrary.Core.Data
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
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array that will be sliced from.</param>
        /// <param name="index">The start index.</param>
        /// <param name="end">The end index.  If end is negative, it is treated like length.</param>
        /// <returns>The resulting array.</returns>
        public static T[] Slice<T>(this T[] array, int index, int end)
        {
            // Handles negative ends
            int len;
            if (end == 0)
            {
                throw new ArgumentOutOfRangeException("end", end, "must be a positive index or a length (indicated by negative), not 0");
            }
            else if (end > 0)
            {
                len = end - index;
            }
            else
            {
                len = -end;
                end = index + len;
            }

            // Return new array
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = array[i + index];
            }

            return res;
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
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
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
