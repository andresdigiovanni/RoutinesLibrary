using System;
using System.Collections.Generic;

namespace RoutinesLibrary.Data
{
    public class ByteHelper
    {
        public static bool ArraysEqual(byte[] first, byte[] second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string BytesToHexa(byte[] bytes, string separator = "")
        {
            string sHexa = "";

            for (var i = 0; i < bytes.Length; i++)
            {
                if (!string.IsNullOrEmpty(sHexa))
                {
                    sHexa += separator;
                }
                sHexa += bytes[i].ToString("X").PadLeft(2, '0');
            }

            return sHexa;
        }

        public static List<byte> HexaToBytes(string sHexa, string separator = "")
        {
            List<byte> bytes = new List<byte>();
            if (separator != "")
            {
                sHexa = sHexa.Replace(separator, "");
            }

            for (int i = 0; i < sHexa.Length; i += 2)
            {
                bytes.Add(Convert.ToByte(sHexa.Substring(i, 2), 16));
            }

            return bytes;
        }

        public static string BytesToString(byte[] bytes)
        {
            string sHexa = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                sHexa += string.Format("{0:X2}", bytes[i]);
            }

            return sHexa;
        }

        public static byte[] StringToBytes(string sHexa)
        {
            return System.Text.Encoding.ASCII.GetBytes(sHexa);
        }
    }
}