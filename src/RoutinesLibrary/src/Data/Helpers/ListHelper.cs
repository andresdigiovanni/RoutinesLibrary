using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    public static class ListHelper
    {
        public static int IndexOfMax<T>(this IList<T> values) where T : IComparable
        {
            // Check arguments
            if (ReferenceEquals(values, null) || values.Count <= 0)
            {
                throw (new ArgumentNullException("values"));
            }

            int maxIndex = -1;
            T maxValue = values[0];

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].CompareTo(maxValue) >= 0)
                {
                    maxIndex = i;
                    maxValue = values[i];
                }
            }

            return maxIndex;
        }

        public static int IndexOfMin<T>(this IList<T> values) where T : IComparable
        {
            // Check arguments
            if (ReferenceEquals(values, null) || values.Count <= 0)
            {
                throw (new ArgumentNullException("values"));
            }

            int minIndex = -1;
            T minValue = values[0];

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].CompareTo(minValue) <= 0)
                {
                    minIndex = i;
                    minValue = values[i];
                }
            }

            return minIndex;
        }

        public static void Shuffle<T>(this IList<T> values)
        {
            // Check arguments
            if (ReferenceEquals(values, null) || values.Count <= 0)
            {
                throw (new ArgumentNullException("values"));
            }

            Random rng = new Random();
            int n = values.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = values[k];
                values[k] = values[n];
                values[n] = value;
            }
        }

        public static int Median(this IEnumerable<int> values)
        {
            // Check arguments
            if (ReferenceEquals(values, null) || values.Count() <= 0)
            {
                throw (new ArgumentNullException("values"));
            }

            int midIndex = values.Count() / 2;
            var sorted = values.OrderBy(x => x).ToList();

            int median =
                values.Count() % 2 == 0
                    ? (sorted[midIndex] + sorted[midIndex - 1]) / 2
                    : sorted[midIndex];

            return median;
        }

        public static double Mean(this List<double> values)
        {
            // Check arguments
            if (ReferenceEquals(values, null) || values.Count <= 0)
            {
                throw (new ArgumentNullException("values"));
            }

            return values.Count == 0 ? 0 : values.Mean(0, values.Count - 1);
        }

        public static double Mean(this List<double> values, int start, int end)
        {
            // Check arguments
            if (ReferenceEquals(values, null) || values.Count <= 0)
            {
                throw (new ArgumentNullException("values"));
            }

            if (start < 0 || start >= values.Count)
            {
                throw (new ArgumentOutOfRangeException("start"));
            }

            if (end < 0 || end >= values.Count)
            {
                throw (new ArgumentOutOfRangeException("end"));
            }

            if (start > end)
            {
                throw (new ArgumentOutOfRangeException("end"));
            }

            double s = 0;

            for (int i = start; i <= end; i++)
            {
                s += values[i];
            }

            return s / (end - start + 1);
        }

        public static double Variance(this List<double> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean)
        {
            return values.Variance(mean, 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean, int start, int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += Math.Pow((values[i] - mean), 2);
            }

            int n = end - start;
            if (start > 0) n -= 1;

            return variance / (n);
        }

        public static double StandardDeviation(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
        }

        public static double StandardDeviation(this List<double> values, int start, int end)
        {
            double mean = values.Mean(start, end);
            double variance = values.Variance(mean, start, end);

            return Math.Sqrt(variance);
        }
    }
}
