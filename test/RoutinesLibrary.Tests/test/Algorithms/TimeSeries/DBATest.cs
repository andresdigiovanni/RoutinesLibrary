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
    public class DBATest
    {
        [Theory]
        [ClassData(typeof(AverageData))]
        public void Average(List<int[]> value, int[] expected)
        {
            var result = DBA.Average(value);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Average_Exception_value_Empty()
        {
            List<int[]> value = new List<int[]>();

            Assert.Throws<ArgumentNullException>(() => DBA.Average(value));
        }

        [Fact]
        public void Average_Exception_value_Null()
        {
            List<int[]> value = null;

            Assert.Throws<ArgumentNullException>(() => DBA.Average(value));
        }

        public class AverageData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<int[]>() {
                                                new int[] { 0, 0, 0, 0, 0 }},
                                            new int[] { 0, 0, 0, 0, 0 }};
                yield return new object[] { new List<int[]>() {
                                                new int[] { 0, 0, 0, 0, 0 },
                                                new int[] { 2, 2, 2, 2, 2 }},
                                            new int[] { 1, 1, 1, 1, 1 }};
                yield return new object[] { new List<int[]>() {
                                                new int[] { 0, 0, 0, 0 },
                                                new int[] { 2, 2, 2, 2, 2, 2 }},
                                            new int[] { 1, 1, 1, 1, 1 }};
                yield return new object[] { new List<int[]>() {
                                                new int[] { 0, 0, 0, 0 },
                                                new int[] { 2, 2, 2, 2, 2 },
                                                new int[] { 4, 4, 4, 4, 4, 4 }},
                                            new int[] { 2, 2, 2, 2, 2 }};
                yield return new object[] { new List<int[]>() {
                                                new int[] { 0, 1, 2, 3, 4 },
                                                new int[] { 4, 3, 2, 1, 0 }},
                                            new int[] { 2, 2, 2, 2, 2 }};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
