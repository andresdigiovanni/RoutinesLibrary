using RoutinesLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Data
{
    public class AverageTest
    {
        [Theory]
        [InlineData(0, 4, 1, 0.25)]
        [InlineData(2.5, 3, 4, 3)]
        [InlineData(3, 3, 3, 3)]
        [InlineData(4, 4, 0, 3)]
        public void RunningAverage_Valid(double previousAvg, int count, double currentValue, double expected)
        {
            var result = Average.RunningAverage(previousAvg, count, currentValue);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RunningAverage_Exception_Count_Zero()
        {
            Assert.Throws<DivideByZeroException>(() => Average.RunningAverage(5, 0, 5));
        }
    }
}
