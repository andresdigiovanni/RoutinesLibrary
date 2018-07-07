using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RoutinesLibrary.Core.Algorithms.TimeSeries
{
    /// <summary>
    /// Dynamic Time Wrapping
    /// </summary>
    /// <remarks>https://gist.github.com/socrateslee/1966342</remarks>
    public class DTW
    {
        double[] x;
        double[] y;
        double[,] distance;
        double[,] f;
        double sum;

        public DTW(double[] _x, double[] _y, int sakoeChibaBand = -1)
        {
            x = _x;
            y = _y;
            distance = new double[x.Length, y.Length];
            f = new double[x.Length + 1, y.Length + 1];

            for (int i = 0; i < x.Length; ++i)
            {
                for (int j = 0; j < y.Length; ++j)
                {
                    distance[i, j] = Math.Abs(x[i] - y[j]);
                }
            }

            for (int i = 0; i <= x.Length; ++i)
            {
                for (int j = 0; j <= y.Length; ++j)
                {
                    f[i, j] = -1;
                }
            }

            for (int i = 1; i <= x.Length; ++i)
            {
                f[i, 0] = double.MaxValue;
            }
            for (int j = 1; j <= y.Length; ++j)
            {
                f[0, j] = double.MaxValue;
            }

            // Sakoe-Chiba Band
            if (sakoeChibaBand > 0)
            {
                double step = y.Length / x.Length;
                for (int i = 1; i <= x.Length; ++i)
                {
                    for (int j = 1; j <= y.Length; ++j)
                    {
                        if (i * step > j + sakoeChibaBand ||
                            i * step < j - sakoeChibaBand)
                        {
                            f[i, j] = double.MaxValue;
                        }
                    }
                }
            }

            f[0, 0] = 0;
            sum = ComputeFBackward(x.Length, y.Length);
        }

        public double GetSum()
        {
            return sum;
        }

        public Tuple<double, double>[] GetPath()
        {
            List<Tuple<double, double>> tupleBackward = ComputePathBackward(x.Length, y.Length);
            tupleBackward.RemoveAt(0);
            return tupleBackward.ToArray();
        }

        private double ComputeFBackward(int i, int j)
        {
            if (!(f[i, j] < 0))
            {
                return f[i, j];
            }
            else
            {
                if (ComputeFBackward(i - 1, j) <= ComputeFBackward(i, j - 1) &&
                    ComputeFBackward(i - 1, j) <= ComputeFBackward(i - 1, j - 1) &&
                    ComputeFBackward(i - 1, j) < double.MaxValue)
                {
                    f[i, j] = distance[i - 1, j - 1] + ComputeFBackward(i - 1, j);
                }
                else if (ComputeFBackward(i, j - 1) <= ComputeFBackward(i - 1, j) &&
                         ComputeFBackward(i, j - 1) <= ComputeFBackward(i - 1, j - 1) &&
                         ComputeFBackward(i, j - 1) < double.MaxValue)
                {
                    f[i, j] = distance[i - 1, j - 1] + ComputeFBackward(i, j - 1);
                }
                else if (ComputeFBackward(i - 1, j - 1) <= ComputeFBackward(i - 1, j) &&
                         ComputeFBackward(i - 1, j - 1) <= ComputeFBackward(i, j - 1) &&
                         ComputeFBackward(i - 1, j - 1) < double.MaxValue)
                {
                    f[i, j] = distance[i - 1, j - 1] + ComputeFBackward(i - 1, j - 1);
                }
            }
            return f[i, j];
        }

        private List<Tuple<double, double>> ComputePathBackward(int i, int j)
        {
            List<Tuple<double, double>> tupleBackward = new List<Tuple<double, double>>();

            if (i != 0 && j != 0)
            {
                if (f[i - 1, j] <= f[i - 1, j - 1] &&
                    f[i - 1, j] <= f[i, j - 1])
                {
                    tupleBackward = ComputePathBackward(i - 1, j);
                    tupleBackward.Add(new Tuple<double, double>(i - 2, j - 1));
                }
                else if (f[i, j - 1] <= f[i - 1, j] &&
                         f[i, j - 1] <= f[i - 1, j - 1])
                {
                    tupleBackward = ComputePathBackward(i, j - 1);
                    tupleBackward.Add(new Tuple<double, double>(i - 1, j - 2));
                }
                else if (f[i - 1, j - 1] <= f[i - 1, j] &&
                         f[i - 1, j - 1] <= f[i, j - 1])
                {
                    tupleBackward = ComputePathBackward(i - 1, j - 1);
                    tupleBackward.Add(new Tuple<double, double>(i - 2, j - 2));
                }
            }

            return tupleBackward;
        }
    }
}
