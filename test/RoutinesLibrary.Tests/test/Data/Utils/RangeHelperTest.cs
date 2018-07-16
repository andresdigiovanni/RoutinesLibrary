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
    public class RangeHelperTest
    {
        [Theory]
        [InlineData(20, 15, 10, 5, false)]
        [InlineData(20, 15, 15, 5, false)]
        [InlineData(20, 15, 19, 5, true)]
        [InlineData(20, 15, 19, 16, true)]
        [InlineData(20, 15, 30, 16, true)]
        [InlineData(20, 15, 30, 20, false)]
        [InlineData(20, 15, 30, 25, false)]
        public void Intersection_Detected(double max1, double min1, double max2, double min2, bool expected)
        {
            double maxRresult = 0;
            double minRresult = 0;
            var result = RangeHelper.Intersection(max1, min1, max2, min2, out maxRresult, out minRresult);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(20, 15, 19, 5, 19, 15)]
        [InlineData(20, 15, 19, 16, 19, 16)]
        [InlineData(20, 15, 30, 16, 20, 16)]
        public void Intersection_Values(double max1, double min1, double max2, double min2, double maxRexpected, double minRexpected)
        {
            double maxRresult = 0;
            double minRresult = 0;
            RangeHelper.Intersection(max1, min1, max2, min2, out maxRresult, out minRresult);

            Assert.True(maxRresult == maxRexpected && minRresult == minRexpected);
        }
    }
}
