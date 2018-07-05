using System;

namespace RoutinesLibrary.Core.Data
{
    public abstract class AbstractHeap
    {
        internal int Size { get; set; }
        internal int[] Nodes { get; set; }


        internal abstract void HeapifyUp();
        internal abstract void HeapifyDown();


        public AbstractHeap()
        {
            Size = 0;
            Nodes = new int[100];
        }

        public void EnlargeIfNeeded()
        {
            if (Size == Nodes.Length)
            {
                Array.Copy(Nodes, Nodes, Nodes.Length * 2);
            }
        }

        public int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        public bool HasLeftChild(int parentIndex)
        {
            return GetLeftChildIndex(parentIndex) < Size;
        }

        public int LeftChild(int index)
        {
            return Nodes[GetLeftChildIndex(index)];
        }

        public int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        public bool HasRightChild(int parentIndex)
        {
            return GetRightChildIndex(parentIndex) < Size;
        }

        public int RightChild(int index)
        {
            return Nodes[GetRightChildIndex(index)];
        }

        public int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        public bool HasParent(int childIndex)
        {
            return GetParentIndex(childIndex) >= 0;
        }

        public int Parent(int index)
        {
            return Nodes[GetParentIndex(index)];
        }

        public void Swap(int index1, int index2)
        {
            int temp = Nodes[index1];
            Nodes[index1] = Nodes[index2];
            Nodes[index2] = temp;
        }


        /// <summary>
        /// Gets the minimum element at the root of the tree
        /// </summary>
        /// <returns>Int value of minimum element</returns>
        /// <exception cref="">InvalidOperationException when heap is empty</exception>
        public int Peek()
        {
            if (Size == 0)
                throw new InvalidOperationException("Heap is empty");

            return Nodes[0];
        }

        /// <summary>
        /// Removes the minimum element at the root of the tree
        /// </summary>
        /// <returns>Int value of minimum element</returns>
        /// <exception cref="">InvalidOperationException when heap is empty</exception>
        public int Pop()
        {
            if (Size == 0)
                throw new InvalidOperationException("Heap is empty");

            int item = Nodes[0];
            Nodes[0] = Nodes[Size - 1];
            Size--;
            HeapifyDown();
            return item;
        }

        /// <summary>
        /// Add a new item to heap, enlarging the array if needed
        /// </summary>
        /// <returns>void</returns>
        public void Add(int item)
        {
            EnlargeIfNeeded();
            Nodes[Size] = item;
            Size++;
            HeapifyUp();
        }
    }
}