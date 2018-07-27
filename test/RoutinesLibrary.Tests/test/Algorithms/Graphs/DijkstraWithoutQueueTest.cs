using RoutinesLibrary.Algorithms.Graphs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutinesLibrary.Tests.Algorithms.Graphs
{
    public class DijkstraWithoutQueueTest
    {
        [Theory]
        [ClassData(typeof(DijkstraAlgorithmData))]
        public void DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode, int[] expected)
        {
            var output = DijkstraWithoutQueue.DijkstraAlgorithm(graph, sourceNode, destinationNode);
            int[] result = null;

            if (!ReferenceEquals(output, null))
            {
                result = output.ToArray();
            }

            Assert.Equal(expected, result);
        }

        public class DijkstraAlgorithmData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var graph = new[,]
                {
                    // 0   1   2   3   4   5   6   7   8   9  10  11
                    { 0,  0,  0,  0,  0,  0, 10,  0, 12,  0,  0,  0 }, // 0
                    { 0,  0,  0,  0, 20,  0,  0, 26,  0,  5,  0,  6 }, // 1
                    { 0,  0,  0,  0,  0,  0,  0, 15, 14,  0,  0,  9 }, // 2
                    { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  7,  0 }, // 3
                    { 0, 20,  0,  0,  0,  5, 17,  0,  0,  0,  0, 11 }, // 4
                    { 0,  0,  0,  0,  5,  0,  6,  0,  3,  0,  0, 33 }, // 5
                    {10,  0,  0,  0, 17,  6,  0,  0,  0,  0,  0,  0 }, // 6
                    { 0, 26, 15,  0,  0,  0,  0,  0,  0,  3,  0, 20 }, // 7
                    {12,  0, 14,  0,  0,  3,  0,  0,  0,  0,  0,  0 }, // 8
                    { 0,  5,  0,  0,  0,  0,  0,  3,  0,  0,  0,  0 }, // 9
                    { 0,  0,  0,  7,  0,  0,  0,  0,  0,  0,  0,  0 }, // 10
                    { 0,  6,  9,  0, 11, 33,  0, 20,  0,  0,  0,  0 }, // 11
                };

                yield return new object[] { graph, 0, 9, new int[] { 0, 8, 5, 4, 11, 1, 9 } };
                yield return new object[] { graph, 0, 2, new int[] { 0, 8, 2 } };
                yield return new object[] { graph, 0, 10, null };
                yield return new object[] { graph, 0, 11, new int[] { 0, 8, 5, 4, 11 } };
                yield return new object[] { graph, 0, 1, new int[] { 0, 8, 5, 4, 11, 1 } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
