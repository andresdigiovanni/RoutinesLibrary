using RoutinesLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Data.DataStructures
{
    public class TwoKeyHashtableTest
    {
        [Theory]
        [InlineData(1000, 1000)]
        public void Add(int countKey1, int countKey2)
        {
            var twoKeyHashtable = new TwoKeyHashtable();
            var result = true;

            for (int i = 0; i < countKey1; i++)
            {
                for (int j = 0; j < countKey2; j++)
                {
                    twoKeyHashtable.Add(i, j, i + "," + j);
                    result &= twoKeyHashtable.Item(i, j) == (i + "," + j);
                }
            }

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1000, 1000)]
        public void Clear(int countKey1, int countKey2)
        {
            var twoKeyHashtable = new TwoKeyHashtable();
            for (int i = 0; i < countKey1; i++)
            {
                for (int j = 0; j < countKey2; j++)
                {
                    twoKeyHashtable.Add(i, j, 0);
                }
            }
            twoKeyHashtable.Clear();

            var result = twoKeyHashtable.CountRow();
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 1000)]
        public void Count(int key1, int countKey2)
        {
            var twoKeyHashtable = new TwoKeyHashtable();            
            for (int i = 0; i < countKey2; i++)
            {
                twoKeyHashtable.Add(key1, i, 0);
            }
            
            var result = twoKeyHashtable.Count(key1);
            Assert.Equal(countKey2, result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1000, 0)]
        public void CountRow(int countKey1, int key2)
        {
            var twoKeyHashtable = new TwoKeyHashtable();
            for (int i = 0; i < countKey1; i++)
            {
                twoKeyHashtable.Add(i, key2, 0);
            }

            var result = twoKeyHashtable.CountRow();
            Assert.Equal(countKey1, result);
        }
    }
}
