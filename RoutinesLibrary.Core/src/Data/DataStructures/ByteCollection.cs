using System;
using System.Collections;

namespace RoutinesLibrary.Core.Data
{
    public class ByteCollection : CollectionBase
    {
        static readonly object _locker = new object();


        public byte this[int index]
        {
            get
            {
                lock (_locker)
                {
                    return (byte) InnerList[index];
                }
            }
            set
            {
                lock (_locker)
                {
                    InnerList[index] = value;
                }
            }
        }

        public int Add(byte value)
        {
            lock (_locker)
            {
                return InnerList.Add(value);
            }
        }

        public byte[] GetArray
        {
            get
            {
                lock (_locker)
                {
                    byte[] Data = new byte[InnerList.Count];

                    for (int i = 0; i < InnerList.Count; i++)
                    {
                        Data[i] = this[i];
                    }

                    return (Data);
                }
            }
        }
    }
}