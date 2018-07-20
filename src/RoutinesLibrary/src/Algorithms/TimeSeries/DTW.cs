using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RoutinesLibrary.Algorithms.TimeSeries
{
    /// <summary>
    /// Dynamic Time Wrapping
    /// </summary>
    /// <remarks>https://gist.github.com/socrateslee/1966342</remarks>
    public class DTW
    {
        private double[] _x;
        private double[] _y;
        private double[,] _distance;
        private double[,] _f;
        private double _sum;

        public DTW(double[] x, double[] y, int sakoeChibaBand = -1)
        {
            // Check arguments
            if (ReferenceEquals(x, null) || x.Length <= 0)
            {
                throw (new ArgumentNullException("x"));
            }

            if (ReferenceEquals(y, null) || y.Length <= 0)
            {
                throw (new ArgumentNullException("y"));
            }

            _x = x;
            _y = y;
            _distance = new double[x.Length, y.Length];
            _f = new double[x.Length + 1, y.Length + 1];

            for (int i = 0; i < x.Length; ++i)
            {
                for (int j = 0; j < y.Length; ++j)
                {
                    _distance[i, j] = Math.Abs(x[i] - y[j]);
                }
            }

            for (int i = 0; i <= x.Length; ++i)
            {
                for (int j = 0; j <= y.Length; ++j)
                {
                    _f[i, j] = -1;
                }
            }

            for (int i = 1; i <= x.Length; ++i)
            {
                _f[i, 0] = int.MaxValue;
            }
            for (int j = 1; j <= y.Length; ++j)
            {
                _f[0, j] = int.MaxValue;
            }

            // Sakoe-Chiba Band
            if (sakoeChibaBand > 0)
            {
                double step = (double)y.Length / (double)x.Length;
                for (int i = 1; i <= x.Length; ++i)
                {
                    for (int j = 1; j <= y.Length; ++j)
                    {
                        if (i * step > j + sakoeChibaBand ||
                            i * step < j - sakoeChibaBand)
                        {
                            _f[i, j] = int.MaxValue;
                        }
                    }
                }
            }

            _f[0, 0] = 0;
            _sum = ComputeFBackward(x.Length, y.Length);
        }

        public double GetSum()
        {
            return _sum;
        }

        public Tuple<int, int>[] GetPath()
        {
            List<Tuple<int, int>> tupleBackward = ComputePathBackward(_x.Length, _y.Length);
            tupleBackward.RemoveAt(0);
            tupleBackward.Add(new Tuple<int, int>(_x.Length - 1, _y.Length - 1));
            return tupleBackward.ToArray();
        }

        private double ComputeFBackward(int i, int j)
        {
            if (!(_f[i, j] < 0))
            {
                return _f[i, j];
            }
            else
            {
                if (ComputeFBackward(i - 1, j) <= ComputeFBackward(i, j - 1) &&
                    ComputeFBackward(i - 1, j) <= ComputeFBackward(i - 1, j - 1) &&
                    ComputeFBackward(i - 1, j) < int.MaxValue)
                {
                    _f[i, j] = _distance[i - 1, j - 1] + ComputeFBackward(i - 1, j);
                }
                else if (ComputeFBackward(i, j - 1) <= ComputeFBackward(i - 1, j) &&
                         ComputeFBackward(i, j - 1) <= ComputeFBackward(i - 1, j - 1) &&
                         ComputeFBackward(i, j - 1) < int.MaxValue)
                {
                    _f[i, j] = _distance[i - 1, j - 1] + ComputeFBackward(i, j - 1);
                }
                else if (ComputeFBackward(i - 1, j - 1) <= ComputeFBackward(i - 1, j) &&
                         ComputeFBackward(i - 1, j - 1) <= ComputeFBackward(i, j - 1) &&
                         ComputeFBackward(i - 1, j - 1) < int.MaxValue)
                {
                    _f[i, j] = _distance[i - 1, j - 1] + ComputeFBackward(i - 1, j - 1);
                }
            }
            return _f[i, j];
        }

        private List<Tuple<int, int>> ComputePathBackward(int i, int j)
        {
            List<Tuple<int, int>> tupleBackward = new List<Tuple<int, int>>();

            if (i != 0 && j != 0)
            {
                if (_f[i - 1, j - 1] <= _f[i - 1, j] &&
                         _f[i - 1, j - 1] <= _f[i, j - 1])
                {
                    tupleBackward = ComputePathBackward(i - 1, j - 1);
                    tupleBackward.Add(new Tuple<int, int>(i - 2, j - 2));
                }
                else if (_f[i - 1, j] <= _f[i - 1, j - 1] &&
                    _f[i - 1, j] <= _f[i, j - 1])
                {
                    tupleBackward = ComputePathBackward(i - 1, j);
                    tupleBackward.Add(new Tuple<int, int>(i - 2, j - 1));
                }
                else if (_f[i, j - 1] <= _f[i - 1, j] &&
                         _f[i, j - 1] <= _f[i - 1, j - 1])
                {
                    tupleBackward = ComputePathBackward(i, j - 1);
                    tupleBackward.Add(new Tuple<int, int>(i - 1, j - 2));
                }
            }

            return tupleBackward;
        }
    }
}
