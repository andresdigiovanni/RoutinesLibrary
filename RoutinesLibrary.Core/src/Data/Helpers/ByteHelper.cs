using System;
using System.Collections.Generic;

namespace RoutinesLibrary.Core.Data
{
    public class ByteHelper
    {
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
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static byte[] StringToBytes(string sHexa)
        {
            return System.Text.Encoding.ASCII.GetBytes(sHexa);
        }
    }
}