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
    }
}
