using System;
using System.Text;

namespace RoutinesLibrary.Data
{
    public class StringHelper
    {
        public static string ToHex(string asciiString)
        {
            string hex = "";
            foreach (char eachChar in asciiString)
            {
                hex += string.Format("{0:x2}", Convert.ToInt32(eachChar));
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