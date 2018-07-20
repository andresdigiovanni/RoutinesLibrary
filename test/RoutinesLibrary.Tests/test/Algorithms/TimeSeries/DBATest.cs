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
        public void Average(List<double[]> value, double[] expected)
        {
            var result = DBA.Average(value);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Average_Exception_value_Empty()
        {
            List<double[]> value = new List<double[]>();

            Assert.Throws<ArgumentNullException>(() => DBA.Average(value));
        }

        [Fact]
        public void Average_Exception_value_Null()
        {
            List<double[]> value = null;

            Assert.Throws<ArgumentNullException>(() => DBA.Average(value));
        }

        public class AverageData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<double[]>() {
                                                new double[] { 0, 0, 0, 0, 0 }},
                                            new double[] { 0, 0, 0, 0, 0 }};
                yield return new object[] { new List<double[]>() {
                                                new double[] { 0, 0, 0, 0, 0 },
                                                new double[] { 2, 2, 2, 2, 2 }},
                                            new double[] { 1, 1, 1, 1, 1 }};
                yield return new object[] { new List<double[]>() {
                                                new double[] { 0, 0, 0, 0, 0 },
                                                new double[] { 2, 2, 2, 2, 2 },
                                                new double[] { 4, 4, 4, 4, 4 }},
                                            new double[] { 2, 2, 2, 2, 2 }};
                yield return new object[] { new List<double[]>() {
                                                new double[] { 0, 1, 2, 3, 4 },
                                                new double[] { 4, 3, 2, 1, 0 }},
                                            new double[] { 2, 2, 2, 2, 2 }};
                yield return new object[] { new List<double[]>() {
                                                new double[] { 0.0, 0.1, 0.2, 0.3, 0.4 },
                                                new double[] { 0.4, 0.3, 0.2, 0.1, 0.0 }},
                                            new double[] { 0.2, 0.2, 0.2, 0.2, 0.2 }};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
