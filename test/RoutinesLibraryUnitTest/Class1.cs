using RoutinesLibrary.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibraryUnitTest
{
    public class Class1
    {
        #region Data

        #region Security

        #region CRC16

        [Theory]
        [InlineData("0123456789", 0x443D)]
        [InlineData("ABCDEF", 0xED91)]
        [InlineData("abcdef", 0x5805)]
        [InlineData("this_is_a_test", 0x1678)]
        [InlineData(" !$%'()*-./", 0xC85A)]
        [InlineData(":;<=>?@", 0xEDB2)]
        [InlineData("[\\]^_`{|}~", 0x2C54)]
        public void CRC16_Valid(string value, ushort expected)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var result = CRC16.ComputeChecksum(bytes);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CRC16_Exception_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16.ComputeChecksum(new byte[] { }));
        }

        [Fact]
        public void CRC16_Exception_Null()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16.ComputeChecksum(null));
        }

        #endregion

        #region CRC16_CCITT

        [Theory]
        [InlineData("0123456789", 0x9C58)]
        [InlineData("ABCDEF", 0x944D)]
        [InlineData("abcdef", 0x3AFD)]
        [InlineData("this_is_a_test", 0xFA23)]
        [InlineData(" !$%'()*-./", 0x70D1)]
        [InlineData(":;<=>?@", 0x578C)]
        [InlineData("[\\]^_`{|}~", 0x5D34)]
        public void CRC16_CCITT_Valid(string value, ushort expected)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var result = CRC16_CCITT.ComputeChecksum(bytes);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CRC16_CCITT_Exception_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16_CCITT.ComputeChecksum(new byte[] { }));
        }

        [Fact]
        public void CRC16_CCITT_Exception_Null()
        {
            Assert.Throws<ArgumentNullException>(() => CRC16_CCITT.ComputeChecksum(null));
        }

        #endregion

        #endregion

        #endregion
    }
}
