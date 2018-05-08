using System;
using System.Collections.Generic;

namespace RoutinesLibrary.Core.Data
{
    public class RangeHelper
    {
        public static bool Intersection(double max1, double min1, double max2, double min2, out double maxR, out double minR)
        {
            bool intersection;

            //min1--
            //  min2--
            if (min1 < min2)
            {
                //--max1
                //    min2--
                if (max1 < min2)
                {
                    //No intersection
                    maxR = max1;
                    minR = min1;
                    intersection = false;
                }
                //--max1
                //  --max2
                else if (max1 < max2)
                {
                    maxR = max1;
                    minR = min2;
                    intersection = true;
                }
                //  --max1
                //--max2
                else
                {
                    maxR = max2;
                    minR = min2;
                    intersection = true;
                }
            }
            //  min1--
            //min2--
            else
            {
                //    min1--
                //--max2
                if (max2 < min1)
                {
                    //No intersection
                    maxR = max1;
                    minR = min1;
                    intersection = false;
                }
                //  --max1
                //--max2
                else if (max2 < max1)
                {
                    maxR = max2;
                    minR = min1;
                    intersection = true;
                }
                //--max1
                //  --max2
                else
                {
                    maxR = max1;
                    minR = min1;
                    intersection = true;
                }
            }

            return intersection;
        }
    }
}
