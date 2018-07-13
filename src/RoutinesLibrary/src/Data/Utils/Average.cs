using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    public class Average
    {
        /// <summary>
        /// Calculate average
        /// </summary>
        /// <param name="previousAvg">Previous average</param>
        /// <param name="count">Number of elements with the new value</param>
        /// <param name="currentValue">New value</param>
        /// <returns>Average</returns>
        public static double RunningAverage(double previousAvg, int count, double currentValue)
        {
            if (count == 0)
            {
                throw (new DivideByZeroException("count"));
            }

            return (previousAvg * (count - 1) + currentValue) / count;
        }
    }
}
