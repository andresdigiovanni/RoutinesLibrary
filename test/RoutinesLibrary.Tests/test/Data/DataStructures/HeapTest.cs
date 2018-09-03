using RoutinesLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Data
{
    public class HeapTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void Insert(int values)
        {
            Heap<int> heap = new Heap<int>(1);
            for (int i = 0; i < values; i++)
            {
                heap.Insert(i);
            }
            var result = heap.Count;

            Assert.Equal(values, result);
        }

        [Fact]
        public void IsEmpty()
        {
            Heap<int> heap = new Heap<int>(0);

            Assert.True(heap.IsEmpty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void Peek_MinHeap(int values)
        {
            var result = true;
            Heap<int> heap = new Heap<int>(1);

            for (int i = values - 1; i > 0; i--)
            {
                heap.Insert(i);
                result &= (i == heap.Peek());
            }

            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void Peek_MaxHeap(int values)
        {
            var result = true;
            Heap<int> heap = new Heap<int>(1, true);

            for (int i = 0; i < values; i++)
            {
                heap.Insert(i);
                result &= (i == heap.Peek());
            }

            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void Remove(int values)
        {
            Heap<int> heap = new Heap<int>(1);
            for (int i = 0; i < values; i++)
            {
                heap.Insert(i);
            }
            for (int i = 0; i < values; i++)
            {
                heap.Remove();
            }
            var result = heap.Count;

            Assert.True(heap.IsEmpty);
        }
    }
}
