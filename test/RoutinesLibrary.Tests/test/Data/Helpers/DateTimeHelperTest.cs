using RoutinesLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Data.Helpers
{
    public class DateTimeHelperTest
    {
        [Theory]
        [InlineData("01/01/1970 00:00:00", 0)]
        [InlineData("16/02/2008 12:15:12", 1203164112)]
        public void DateTimeToUnixTimeStamp(string value, long expected)
        {
            DateTime date = DateTime.Parse(value);
            var result = DateTimeHelper.DateTimeToUnixTimeStamp(date);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, "01/01/1970 00:00:00")]
        [InlineData(1203164112, "16/02/2008 12:15:12")]
        public void UnixTimeStampToDateTime(long value, string expected)
        {
            var result = DateTimeHelper.UnixTimeStampToDateTime(value).ToString("dd/MM/yyyy HH:mm:ss");

            Assert.Equal(expected, result);
        }
    }
}
