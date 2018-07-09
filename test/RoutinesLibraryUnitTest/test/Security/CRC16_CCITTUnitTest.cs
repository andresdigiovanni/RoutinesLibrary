using RoutinesLibrary.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibraryUnitTest.Security
{
    public class CRC16_CCITTUnitTest
    {
        [Theory]
        [InlineData("0123456789", 0x9C58)]
        [InlineData("ABCDEF", 0x944D)]
        [InlineData("abcdef", 0x3AFD)]
        [InlineData("this_is_a_test", 0xFA23)]
        [InlineData(" !$%'()*-./", 0x70D1)]
        [InlineData(":;<=>?@", 0x578C)]
        [InlineData("[\\]^_`{|}~", 0x5D34)]
        public void ComputeChecksum_Valid(string value, ushort expected)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var result = CRC16_CCITT.ComputeChecksum(bytes);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ComputeChecksum_Exception_Value_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16_CCITT.ComputeChecksum(new byte[] { }));
        }

        [Fact]
        public void ComputeChecksum_Exception_Value_Null()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16_CCITT.ComputeChecksum(null));
        }
    }
}
