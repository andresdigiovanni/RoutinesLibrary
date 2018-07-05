using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace RoutinesLibrary.Core.Data
{
    [TestClass]
    public class MaxHeapTests
    {
        [TestMethod]
        public void AddEmptyRemove()
        {
            var heap = new MaxHeap();

            heap.Add(10);
            Assert.AreEqual(10, heap.Peek());

            int result = heap.Pop();
            Assert.AreEqual(10, result);
            heap.Add(20);
            Assert.AreEqual(20, heap.Peek());
        }

        [TestMethod]
        public void AddMultipleCheckPeek()
        {
            foreach (int[] a in GetAllPermutations(new[] { 10, 20, 2, 45, 7, 5, 12 }))
            {
                MaxHeap heap = new MaxHeap();
                foreach (int i in a)
                {
                    heap.Add(i);
                }
                Assert.AreEqual(heap.Peek(), a.Max());
            }
        }

        [TestMethod]
        public void AddMultipleCheckPopThenPeek()
        {
            foreach (int[] a in GetAllPermutations(new[] { 10, 20, 2, 45, 7, 5, 12 }))
            {
                MaxHeap heap = new MaxHeap();
                foreach (int i in a)
                {
                    heap.Add(i);
                }
                foreach (int i in a.OrderByDescending(x => x))
                {
                    Assert.AreEqual(heap.Peek(), i);
                    Assert.AreEqual(heap.Pop(), i);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekPoopEmpty()
        {
            var heap = new MaxHeap();
            heap.Peek();
            heap.Pop();
        }

        private static IEnumerable<int[]> GetAllPermutations(int[] a)
        {
            Array.Sort(a);
            yield return a;
            while (true)
            {
                int i = a.Length - 2;
                while (a[i] > a[i + 1] && i != 0)
                {
                    i--;
                }
                if (i == 0)
                {
                    yield break;
                }
                int k = a.Length - 1;
                while (a[k] < a[i])
                {
                    k--;
                }
                Swap(a, i, k);
                for (int j = i + 1; j <= (a.Length + i) / 2; j++)
                {
                    Swap(a, j, a.Length + i - j);
                }
                yield return a;
            }
        }

        private static void Swap(int[] a, int index1, int index2)
        {
            int temp = a[index1];
            a[index1] = a[index2];
            a[index2] = temp;
        }
    }
}
