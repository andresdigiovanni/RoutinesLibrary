using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Core.Data
{
    public class FilterData
    {
        private int m_filterParam;

        private Queue<int> m_queueData = new Queue<int>();
        private int m_mean = 0;


        public FilterData(int param)
        {
            m_filterParam = param;
        }

        public void Clear()
        {
            m_queueData.Clear();
            m_mean = 0;
        }

        public int Add(int value)
        {

            m_queueData.Enqueue(value);
            m_mean += value;

            if (m_queueData.Count() > m_filterParam)
            {
                m_mean -= m_queueData.Dequeue();
            }

            value = m_mean / m_queueData.Count();

            return value;
        }
    }
}
