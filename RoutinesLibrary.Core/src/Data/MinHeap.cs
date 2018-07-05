using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Core.Data
{
    public class MinHeap : AbstractHeap
    {
        public MinHeap() : base()
        {
        }

        internal override void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index) < LeftChild(index))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (Nodes[smallerChildIndex] < Nodes[index])
                    Swap(index, smallerChildIndex);
                else
                    break;
                index = smallerChildIndex;
            }
        }
        internal override void HeapifyUp()
        {
            int index = Size - 1;

            while (HasParent(index) && Parent(index) > Nodes[index])
            {
                Swap(index, GetParentIndex(index));
                index = GetParentIndex(index);
            }
        }
    }
}
