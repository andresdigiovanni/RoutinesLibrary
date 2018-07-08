using System;
using System.Collections.Generic;

namespace RoutinesLibrary.Data
{
    public class IntegerHelper
    {
        public static int RoundNumber(int value, int step)
        {
            return Convert.ToInt32(Math.Round((double)value / step) * step);
        }

        public static int RoundNumber(int value, int step, int min, int max)
        {
            value = RoundNumber(value, step);

            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// Convert a byte array to integer
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <param name="bytesInBigEndian">True if the byte array is in Big Endian</param>
        /// <returns>Converted byte array</returns>
        public static int BytesToInt(byte[] bytes, bool bytesInBigEndian)
        {
            List<byte> copyBytes = new List<byte>();
            copyBytes.AddRange(bytes);

            if ((BitConverter.IsLittleEndian && bytesInBigEndian) || (!BitConverter.IsLittleEndian && !bytesInBigEndian))
            {
                copyBytes.Reverse();
            }

            return BitConverter.ToInt32(copyBytes.ToArray(), 0);
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