using System;
using System.Collections.Generic;

namespace RoutinesLibrary.Data
{
    public class IntegerHelper
    {
        public static int RoundNumber(double value, int step)
        {
            if (step == 0)
            {
                throw (new DivideByZeroException("step"));
            }

            return Convert.ToInt32(Math.Round(value / step) * step);
        }

        public static int RoundNumber(double value, int step, int min, int max)
        {
            value = RoundNumber(value, step);

            if (value < min) return min;
            if (value > max) return max;
            return Convert.ToInt32(value);
        }

        public static string UIntToString(uint value)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            while (value > 0)
            {
                output.Append((char)((value & 0xFF)));
                value >>= 8;
            }
            return output.ToString();
        }

        public static uint StringToUInt(string value)
        {
            uint output = 0;

            for (int i = 0; i < value.Length; i++)
            {
                output += ((uint)value[i] << (i * 8));
            }
            return output;
        }
    }
}