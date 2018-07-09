using RoutinesLibrary.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibraryUnitTest.Security
{
    public class CRC16UnitTest
    {
        [Theory]
        [InlineData("0123456789", 0x443D)]
        [InlineData("ABCDEF", 0xED91)]
        [InlineData("abcdef", 0x5805)]
        [InlineData("this_is_a_test", 0x1678)]
        [InlineData(" !$%'()*-./", 0xC85A)]
        [InlineData(":;<=>?@", 0xEDB2)]
        [InlineData("[\\]^_`{|}~", 0x2C54)]
        public void ComputeChecksum_Valid(string value, ushort expected)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var result = CRC16.ComputeChecksum(bytes);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ComputeChecksum_Exception_Value_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16.ComputeChecksum(new byte[] { }));
        }

        [Fact]
        public void ComputeChecksum_Exception_Value_Null()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16.ComputeChecksum(null));
        }
    }
}
