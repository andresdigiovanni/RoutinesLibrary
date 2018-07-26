using System;
using System.Collections.Generic;

namespace RoutinesLibrary.Data
{
    public class ByteHelper
    {
        public static string BytesToHexa(byte[] bytes, string separator = "")
        {
            // Check arguments
            if (ReferenceEquals(bytes, null))
            {
                throw (new ArgumentNullException("bytes"));
            }

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

        public static byte[] HexaToBytes(string value, string separator = "")
        {
            List<byte> bytes = new List<byte>();
            if (separator != "")
            {
                value = value.Replace(separator, "");
            }

            for (int i = 0; i < value.Length; i += 2)
            {
                bytes.Add(Convert.ToByte(value.Substring(i, 2), 16));
            }

            return bytes.ToArray();
        }

        public static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static byte[] StringToBytes(string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }
    }
}
