using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    public class FilterData
    {
        private int m_filterParam;

        private Queue<double> m_queueData = new Queue<double>();
        private double m_mean = 0;


        public FilterData(int param)
        {
            // Check arguments
            if (param <= 0)
            {
                throw (new ArgumentException("param must be strictly positive"));
            }

            m_filterParam = param;
        }

        public void Clear()
        {
            m_queueData.Clear();
            m_mean = 0;
        }

        public double Add(double value)
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
