using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Algorithms.Graphs
{
    public static class Dijkstra
    {
        public static int[] Compute(double[,] graph, int sourceNode, int destinationNode)
        {
            var length = graph.GetLength(0);

            var distance = new double[length];
            for (int i = 0; i < length; i++)
            {
                distance[i] = double.MaxValue;
            }

            distance[sourceNode] = 0;

            var used = new bool[length];
            var previous = new int?[length];

            while (true)
            {
                var minDistance = double.MaxValue;
                var minNode = 0;
                for (int i = 0; i < length; i++)
                {
                    if (!used[i] && minDistance > distance[i])
                    {
                        minDistance = distance[i];
                        minNode = i;
                    }
                }

                if (minDistance == double.MaxValue)
                {
                    break;
                }

                used[minNode] = true;

                for (int i = 0; i < length; i++)
                {
                    if (graph[minNode, i] > 0)
                    {
                        var shortestToMinNode = distance[minNode];
                        var distanceToNextNode = graph[minNode, i];

                        var totalDistance = shortestToMinNode + distanceToNextNode;

                        if (totalDistance < distance[i])
                        {
                            distance[i] = totalDistance;
                            previous[i] = minNode;
                        }
                    }
                }
            }

            if (distance[destinationNode] == double.MaxValue)
            {
                return new int[0];
            }

            var path = new LinkedList<int>();
            int? currentNode = destinationNode;
            while (currentNode != null)
            {
                path.AddFirst(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            return path.ToArray();
        }
    }
}
