using RoutinesLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Algorithms.TimeSeries
{
    /// <summary>
    /// DTW Barycenter Averaging
    /// </summary>
    /// <remarks>https://github.com/qusma/qusmaml/tree/master/QUSMAML</remarks>
    public class DBA
    {
        /// <summary>
        /// Generate average of supplied series.
        /// </summary>
        public static int[] Average(List<int[]> series, int maxIterations = 100)
        {
            // Check arguments
            if (ReferenceEquals(series, null) || series.Count <= 0)
            {
                throw (new ArgumentNullException("series"));
            }

            if (series.Count == 1)
            {
                return series[0];
            }

            int length = 0;
            for (int i = 0; i < series.Count; i++)
            {
                length += series[i].Length;
            }
            length /= series.Count;

            //initialize to series closest to median min/max after detrending
            List<int[]> tempSeries = series.Select(Detrend).ToList();
            List<int> maxIndexes = tempSeries.Select(x => x.IndexOfMax()).ToList();
            List<int> minIndexes = tempSeries.Select(x => x.IndexOfMin()).ToList();
            int medianMaxIndex = maxIndexes.Median();
            int medianMinIndex = minIndexes.Median();
            List<int> distances = maxIndexes.Select((x, i) => (int)Math.Pow(x - medianMaxIndex, 2) + (int)Math.Pow(minIndexes[i] - medianMinIndex, 2)).ToList();
            int[] average = new int[length];
            for (int i = 0; i < length; i++)
            {
                average[i] = 0;
            }

            //this list will hold the values of each aligned point, 
            //later used to construct the aligned average
            List<int>[] points = new List<int>[length];
            for (int i = 0; i < length; i++)
            {
                points[i] = new List<int>();
            }

            double prevTotalDist = -1;
            double totalDist = -2;

            //sometimes the process gets "stuck" in a loop between two different states
            //so we have to set a hard limit to end the loop
            int count = 0;

            //get the path between each series and the average
            while (totalDist != prevTotalDist && count < maxIterations)
            {
                prevTotalDist = totalDist;

                //clear the points from the last calculation
                foreach (List<int> list in points)
                {
                    list.Clear();
                }

                //here we do the alignment for every series
                foreach (int[] ts in series)
                {
                    DTW dtw = new DTW(ts, average, 3);
                    Tuple<int, int>[] path = dtw.GetPath();

                    //use the path to distribute the points according to the warping
                    Array.ForEach(path, x => points[x.Item2].Add(ts[x.Item1]));
                }

                //Then simply construct the new average series by taking the mean of every List in points.
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].Count > 0)
                    {
                        average[i] = (int)points[i].Average();
                    }
                }

                //calculate Euclidean distance to stop the loop if no further improvement can be made
                int[] average1 = average;
                totalDist = 0;
                for (int i = 0; i < series.Count; i++)
                {
                    for (int j = 0; j < series[i].Length; j++)
                    {
                        if (average1.Length <= j) break;
                        totalDist += Math.Pow(series[i][j] - average1[j], 2);
                    }
                }
                count++;
            }

            return average;
        }

        private static int[] Detrend(int[] input)
        {
            int len = input.Length;
            int step = (input[len - 1] - input[0]) / len;
            int[] output = new int[len];

            for (int i = 0; i < len; i++)
            {
                output[i] = input[i] - step * i;
            }

            return output;
        }
    }
}
