using System;
using System.Text;

namespace RoutinesLibrary.Data
{
    public class StringHelper
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string StringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public static string HexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }

        public static string Left(string str, int length)
        {
            return (str.Length <= length) ? str : str.Substring(0, length);
        }

        public static string Mid(string str, int startIndex, int length)
        {
            if (str.Length < startIndex) return "";

            return (str.Length < startIndex + length) ? str.Substring(startIndex, str.Length - startIndex)
                                                      : str.Substring(startIndex, length);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}