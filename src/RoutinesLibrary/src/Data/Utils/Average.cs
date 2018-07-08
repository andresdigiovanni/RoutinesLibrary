using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    public class Average
    {
        public static double RunningAverage(double previousAvg, int count, double currentValue)
        {
            return (previousAvg * (count - 1) + currentValue) / count;
        }
    }
}
