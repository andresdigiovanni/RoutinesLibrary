using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Core.Data
{
    public class MaxHeap : AbstractHeap
    {
        public MaxHeap() : base()
        {
        }

        internal override void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int largerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index) > LeftChild(index))
                {
                    largerChildIndex = GetRightChildIndex(index);
                }

                if (Nodes[largerChildIndex] > Nodes[index])
                    Swap(index, largerChildIndex);
                else
                    break;
                index = largerChildIndex;
            }
        }

        internal override void HeapifyUp()
        {
            int index = Size - 1;

            while (HasParent(index) && Parent(index) < Nodes[index])
            {
                Swap(index, GetParentIndex(index));
                index = GetParentIndex(index);
            }
        }
    }
}
