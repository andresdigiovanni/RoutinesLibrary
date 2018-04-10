using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    public static class ListHelper
    {
        public static int IndexOfMax(this IList<int> input)
        {
            if (input == null) throw new ArgumentNullException("input");

            int maxIndex = -1;
            int maxValue = input[0];

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] >= maxValue)
                {
                    maxIndex = i;
                    maxValue = input[i];
                }
            }

            return maxIndex;
        }

        public static int IndexOfMin(this IList<int> input)
        {
            if (input == null) throw new ArgumentNullException("input");

            int minIndex = -1;
            int minValue = input[0];

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] <= minValue)
                {
                    minIndex = i;
                    minValue = input[i];
                }
            }

            return minIndex;
        }

        public static int Median(this IEnumerable<int> input)
        {
            if (input == null) throw new ArgumentNullException("input");

            int midIndex = input.Count() / 2;
            var sorted = input.OrderBy(x => x).ToList();

            int median =
                input.Count() % 2 == 0
                    ? (sorted[midIndex] + sorted[midIndex - 1]) / 2
                    : sorted[midIndex];

            return median;
        }

        public static double Mean(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static double Mean(this List<double> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
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
