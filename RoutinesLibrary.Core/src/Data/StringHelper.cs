using System;

namespace RoutinesLibrary.Core.Data
{
    public class StringHelper
    {
        public static string Base64Encode(string data)
        {
            byte[] encData = new byte[data.Length];
            encData = System.Text.Encoding.UTF8.GetBytes(data);

            return Convert.ToBase64String(encData);
        }

        public static string Base64Decode(string data)
        {
            System.Text.Decoder utf8Decoder = new System.Text.UTF8Encoding().GetDecoder();

            byte[] bytes = Convert.FromBase64String(data);
            char[] decoded = new char[utf8Decoder.GetCharCount(bytes, 0, bytes.Length)];
            utf8Decoder.GetChars(bytes, 0, bytes.Length, decoded, 0);

            return new String(decoded);
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

        public static string AddEndSlash(string sText)
        {
            if (sText.Substring(sText.Length - 1, 1) != "\\")
            {
                sText += "\\";
            }
            return sText;
        }

        public static string Left(string value, int length)
        {
            return (value.Length <= length) ? value : value.Substring(0, length);
        }

        public static string Mid(string value, int startIndex, int length)
        {
            if (value.Length < startIndex) return "";

            return (value.Length < startIndex + length) ? value.Substring(startIndex, value.Length - startIndex)
                                                        : value.Substring(startIndex, length);
        }
    }
}