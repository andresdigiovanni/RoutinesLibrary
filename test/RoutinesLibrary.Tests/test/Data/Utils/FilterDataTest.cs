using RoutinesLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Data
{
    public class FilterDataTest
    {
        [Theory]
        [ClassData(typeof(FilterDataTestData))]
        public void Add(double[] value, double expected)
        {
            double result = 0;
            var filter = new FilterData(value.Count());

            for (int i = 0; i < value.Count(); i++)
            {
                result = filter.Add(value[i]);
            }

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(FilterDataTestData))]
        public void Clear(double[] value, double expected)
        {
            double result = 0;
            var filter = new FilterData(value.Count());

            for (int i = 0; i < value.Count(); i++)
            {
                filter.Add(value[i]);
            }

            filter.Clear();

            for (int i = 0; i < value.Count(); i++)
            {
                result = filter.Add(value[i]);
            }

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FilterData_Exception_Length_Zero()
        {
            Assert.Throws<ArgumentException>(() => new FilterData(0));
        }

        public class FilterDataTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5 };
                yield return new object[] { new double[] { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5 }, 0 };
                yield return new object[] { new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 }, 0.5 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
