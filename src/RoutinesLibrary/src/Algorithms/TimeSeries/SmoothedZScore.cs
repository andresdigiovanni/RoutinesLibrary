using RoutinesLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Algorithms.TimeSeries
{
    class SmoothedZScore
    {
        private List<double> m_filteredY = new List<double>();
        private List<double> m_avgFilter = new List<double>();
        private List<double> m_stdFilter = new List<double>();

        private float m_threshold = 0F;
        private float m_influence = 0F;
        private int m_lag = 10;


        public void SetThreshold(float threshold)
        {
            m_threshold = threshold;
        }

        public void SetInfluence(float influence)
        {
            m_influence = influence;
        }

        public void SetLag(int lag)
        {
            m_lag = lag;
        }

        public int Add(float value)
        {
            int signal = 0;
            int i = m_filteredY.Count();


            if (i > m_lag)
            {
                if (Math.Abs(value - m_avgFilter[i - 1]) > m_threshold * m_stdFilter[i - 1])
                {
                    signal = (value > m_avgFilter[i - 1]) ? 1 : -1;

                    //Make influence lower
                    m_filteredY.Add(m_influence * value + (1 - m_influence) * m_filteredY[i - 1]);
                }
                else
                {
                    m_filteredY.Add(value);
                }

                //Adjust the filters
                m_avgFilter.Add(ListHelper.Mean(m_filteredY, i - m_lag, i));
                m_stdFilter.Add(ListHelper.StandardDeviation(m_filteredY, i - m_lag, i));
            }
            else
            {
                m_filteredY.Add(value);

                //Adjust the filters
                m_avgFilter.Add(ListHelper.Mean(m_filteredY, 0, i));
                m_stdFilter.Add(ListHelper.StandardDeviation(m_filteredY, 0, i));
            }

            return signal;
        }
    }
}
