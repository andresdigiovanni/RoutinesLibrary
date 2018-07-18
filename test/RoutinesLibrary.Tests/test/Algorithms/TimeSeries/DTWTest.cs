using RoutinesLibrary.Algorithms.TimeSeries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Algorithms.TimeSeries
{
    public class DTWTest
    {
        [Theory]
        [ClassData(typeof(GetSumData))]
        public void GetSum(int[] x, int[] y, int sakoeChibaBand, int expected)
        {
            var dtw = new DTW(x, y, sakoeChibaBand);
            var result = dtw.GetSum();

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(GetPathData))]
        public void GetPath(int[] x, int[] y, int sakoeChibaBand, Tuple<int, int>[] expected)
        {
            var dtw = new DTW(x, y, sakoeChibaBand);
            var result = dtw.GetPath();

            Assert.Equal(expected, result);
        }

        public class GetSumData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new int[] { 0, 1, 2, 3, 4, 5 },
                                            new int[] { 0, 1, 2, 3, 4, 5 }, -1, 0 };
                yield return new object[] { new int[] { 0, 1, 2, 3, 4, 5 },
                                            new int[] { 5, 4, 3, 2, 1, 0 }, -1, 18 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class GetPathData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new int[] { 0, 1, 2, 3, 4, 5 },
                                            new int[] { 0, 1, 2, 3, 4, 5 },
                                            -1,
                                            new Tuple<int, int>[] {
                                                new Tuple<int,int>(0, 0),
                                                new Tuple<int,int>(1, 1),
                                                new Tuple<int,int>(2, 2),
                                                new Tuple<int,int>(3, 3),
                                                new Tuple<int,int>(4, 4),
                                                new Tuple<int,int>(5, 5)} };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
