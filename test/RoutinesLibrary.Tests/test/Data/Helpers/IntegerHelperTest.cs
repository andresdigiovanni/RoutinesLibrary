using RoutinesLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Data
{
    public class IntegerHelperTest
    {
        [Theory]
        [InlineData(10, 2, 10)]
        [InlineData(11, 2, 12)]
        [InlineData(12, 2, 12)]
        [InlineData(10, 5, 10)]
        [InlineData(11, 5, 10)]
        [InlineData(12, 5, 10)]
        [InlineData(13, 5, 15)]
        [InlineData(14, 5, 15)]
        [InlineData(15, 5, 15)]
        [InlineData(-10, 5, -10)]
        [InlineData(-11, 5, -10)]
        [InlineData(-12, 5, -10)]
        [InlineData(-13, 5, -15)]
        [InlineData(-14, 5, -15)]
        [InlineData(-15, 5, -15)]
        [InlineData(0, 5, 0)]
        public void RoundNumber_Valid(double value, int step, int expected)
        {
            var result = IntegerHelper.RoundNumber(value, step);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RoundNumber_Exception_Step_Zero()
        {
            Assert.Throws<DivideByZeroException>(() => IntegerHelper.RoundNumber(10, 0));
        }

        [Theory]
        [InlineData(10, 2, 0, 8, 8)]
        [InlineData(10, 2, 0, 12, 10)]
        [InlineData(11, 2, 0, 8, 8)]
        [InlineData(11, 2, 0, 12, 12)]
        [InlineData(12, 2, 0, 8, 8)]
        [InlineData(12, 2, 0, 12, 12)]
        [InlineData(10, 2, 8, 20, 10)]
        [InlineData(10, 2, 12, 20, 12)]
        [InlineData(11, 2, 8, 20, 12)]
        [InlineData(11, 2, 12, 20, 12)]
        [InlineData(12, 2, 8, 20, 12)]
        [InlineData(12, 2, 12, 20, 12)]
        public void RoundNumber_WithStep_Valid(double value, int step, int min, int max, int expected)
        {
            var result = IntegerHelper.RoundNumber(value, step, min, max);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RoundNumber_WithStep_Exception_Step_Zero()
        {
            Assert.Throws<DivideByZeroException>(() => IntegerHelper.RoundNumber(10, 0, 11, 13));
        }

        [Theory]
        [InlineData(1684234849, "abcd")]
        [InlineData(25185, "ab")]
        [InlineData(0, "")]
        public void UIntToString_Valid(uint value, string expected)
        {
            var result = IntegerHelper.UIntToString(value);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abcd", 1684234849)]
        [InlineData("ab", 25185)]
        [InlineData("", 0)]
        public void StringToUInt_Valid(string value, uint expected)
        {
            var result = IntegerHelper.StringToUInt(value);

            Assert.Equal(expected, result);
        }
    }
}
