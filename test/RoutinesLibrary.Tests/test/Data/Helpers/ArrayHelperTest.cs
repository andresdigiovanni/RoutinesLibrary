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
    public class ArrayHelperTest
    {
        [Theory]
        [ClassData(typeof(SetAllValuesData))]
        public void SetAllValues(double[] value, double setter, double[] expected)
        {
            var result = value.SetAllValues(setter);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SetAllValues_Exception_value_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => ArrayHelper.SetAllValues(new double[] { }, 0));
        }

        [Fact]
        public void SetAllValues_Exception_value_Null()
        {
            Assert.Throws<ArgumentNullException>(() => ArrayHelper.SetAllValues(null, 0));
        }

        public class SetAllValuesData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new double[] { 0 }, 1, new double[] { 1 } };
                yield return new object[] { new double[] { 0, 0, 0, 0, 0 }, 1, new double[] { 1, 1, 1, 1, 1 } };
                yield return new object[] { new double[] { 0, 0, 0, 0, 0 }, 1.2, new double[] { 1.2, 1.2, 1.2, 1.2, 1.2 } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
