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

        [Fact]
        public void DTW_Exception_x_Empty()
        {
            int[] x = new int[] { };
            int[] y = new int[] { 0, 1, 2, 3, 4, 5 };
            Assert.Throws<ArgumentNullException>(() => new DTW(x, y, -1));
        }

        [Fact]
        public void DTW_Exception_x_Null()
        {
            int[] x = null;
            int[] y = new int[] { 0, 1, 2, 3, 4, 5 };
            Assert.Throws<ArgumentNullException>(() => new DTW(x, y, -1));
        }

        [Fact]
        public void DTW_Exception_y_Empty()
        {
            int[] x = new int[] { 0, 1, 2, 3, 4, 5 };
            int[] y = new int[] { };            
            Assert.Throws<ArgumentNullException>(() => new DTW(x, y, -1));
        }

        [Fact]
        public void DTW_Exception_y_Null()
        {
            int[] x = new int[] { 0, 1, 2, 3, 4, 5 };
            int[] y = null;
            Assert.Throws<ArgumentNullException>(() => new DTW(x, y, -1));
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
                yield return new object[] { new int[] { 0, 1, 2, 3, 4 },
                                            new int[] { 0, 1, 2, 3, 4 },
                                            -1,
                                            new Tuple<int, int>[] {
                                                new Tuple<int,int>(0, 0),
                                                new Tuple<int,int>(1, 1),
                                                new Tuple<int,int>(2, 2),
                                                new Tuple<int,int>(3, 3),
                                                new Tuple<int,int>(4, 4)} };
                yield return new object[] { new int[] { 4, 3, 2, 1, 0 },
                                            new int[] { 4, 3, 2, 1, 0 },
                                            -1,
                                            new Tuple<int, int>[] {
                                                new Tuple<int,int>(0, 0),
                                                new Tuple<int,int>(1, 1),
                                                new Tuple<int,int>(2, 2),
                                                new Tuple<int,int>(3, 3),
                                                new Tuple<int,int>(4, 4)} };
                yield return new object[] { new int[] { 0, 0, 0, 0, 0 },
                                            new int[] { 0, 0, 0, 0, 0 },
                                            -1,
                                            new Tuple<int, int>[] {
                                                new Tuple<int,int>(0, 0),
                                                new Tuple<int,int>(1, 1),
                                                new Tuple<int,int>(2, 2),
                                                new Tuple<int,int>(3, 3),
                                                new Tuple<int,int>(4, 4)} };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
