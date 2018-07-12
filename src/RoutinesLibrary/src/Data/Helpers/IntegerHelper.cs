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

        public static string UIntToString(uint input)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            output.Append((char)((input & 0xFF)));
            output.Append((char)((input >> 8) & 0xFF));
            output.Append((char)((input >> 16) & 0xFF));
            output.Append((char)((input >> 24) & 0xFF));

            return output.ToString();
        }

        public static uint StringToUInt(string input)
        {
            uint output;
            output = ((uint)input[0]);
            output += ((uint)input[1] << 8);
            output += ((uint)input[2] << 16);
            output += ((uint)input[3] << 24);

            return output;
        }
    }
}