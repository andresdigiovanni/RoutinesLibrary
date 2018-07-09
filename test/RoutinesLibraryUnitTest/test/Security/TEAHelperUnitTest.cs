using RoutinesLibrary.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibraryUnitTest.Security
{
    public class TEAHelperUnitTest
    {
        [Theory]
        [ClassData(typeof(TEATestData))]
        public void Encrypt_Valid(byte[] value, uint[] key, byte[] expected)
        {
            var result = TEAHelper.Encrypt(value, key);

            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public void Encrypt_Exception_Value_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Encrypt(new byte[] { } , new uint[] { 0 }));
        }

        [Fact]
        public void Encrypt_Exception_Value_Null()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Encrypt(null, new uint[] { 0 }));
        }

        [Fact]
        public void Encrypt_Exception_Key_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Encrypt(new byte[] { 0 }, new uint[] { }));
        }

        [Fact]
        public void Encrypt_Exception_Key_Length()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Encrypt(new byte[] { 0 }, new uint[] { 0, 1, 2 }));
        }

        [Fact]
        public void Encrypt_Exception_Key_Null()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Encrypt(new byte[] { 0 }, null));
        }

        [Theory]
        [ClassData(typeof(TEATestData))]
        public void Decrypt_Valid(byte[] value, uint[] key, byte[] encrypted)
        {
            var result = TEAHelper.Decrypt(encrypted, key);

            Assert.True(result.SequenceEqual(value));
        }

        [Fact]
        public void Decrypt_Exception_Value_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Decrypt(new byte[] { }, new uint[] { 0 }));
        }

        [Fact]
        public void Decrypt_Exception_Value_Null()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Decrypt(null, new uint[] { 0 }));
        }

        [Fact]
        public void Decrypt_Exception_Key_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Decrypt(new byte[] { 0 }, new uint[] { }));
        }

        [Fact]
        public void Decrypt_Exception_Key_Length()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Decrypt(new byte[] { 0 }, new uint[] { 0, 1, 2 }));
        }

        [Fact]
        public void Decrypt_Exception_Key_Null()
        {
            Assert.Throws<ArgumentNullException>(() => TEAHelper.Decrypt(new byte[] { 0 }, null));
        }

        public class TEATestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new byte[] { 0x73, 0x65, 0x63, 0x72, 0x65, 0x74, 0x20, 0x74, 0x65, 0x78, 0x74 },
                                            new uint[] { 0, 1, 2, 3 },
                                            new byte[] { 0xF1, 0xE5, 0xCA, 0x7C, 0x71, 0x04, 0x46, 0x26, 0xFD, 0x01, 0xDA, 0x1B, 0x01, 0x04, 0xC6, 0xC5 } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
