using System;
using System.Text;

namespace NPinyin
{
    public static class Pinyin
    {
        public static string GetInitials(string text)
        {
            text = text.Trim();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                string pinyin = Pinyin.GetPinyin(text[i]);
                if (pinyin != "")
                {
                    stringBuilder.Append(pinyin[0]);
                }
            }
            return stringBuilder.ToString().ToUpper();
        }

        public static string GetInitials(string text, Encoding encoding)
        {
            string text2 = Pinyin.ConvertEncoding(text, encoding, Encoding.UTF8);
            return Pinyin.ConvertEncoding(Pinyin.GetInitials(text2), Encoding.UTF8, encoding);
        }

        public static string GetPinyin(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                string pinyin = Pinyin.GetPinyin(text[i]);
                if (pinyin != "")
                {
                    stringBuilder.Append(pinyin);
                }
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString().Trim();
        }

        public static string GetPinyin(string text, Encoding encoding)
        {
            string text2 = Pinyin.ConvertEncoding(text.Trim(), encoding, Encoding.UTF8);
            return Pinyin.ConvertEncoding(Pinyin.GetPinyin(text2), Encoding.UTF8, encoding);
        }

        public static string GetChineseText(string pinyin)
        {
            string str = pinyin.Trim().ToLower();
            string[] codes = PyCode.codes;
            for (int i = 0; i < codes.Length; i++)
            {
                string text = codes[i];
                if (text.StartsWith(str + " ") || text.StartsWith(str + ":"))
                {
                    return text.Substring(7);
                }
            }
            return "";
        }

        public static string GetChineseText(string pinyin, Encoding encoding)
        {
            string pinyin2 = Pinyin.ConvertEncoding(pinyin, encoding, Encoding.UTF8);
            return Pinyin.ConvertEncoding(Pinyin.GetChineseText(pinyin2), Encoding.UTF8, encoding);
        }

        public static string GetPinyin(char ch)
        {
            short hashIndex = Pinyin.GetHashIndex(ch);
            for (int i = 0; i < PyHash.hashes[(int)hashIndex].Length; i++)
            {
                short num = PyHash.hashes[(int)hashIndex][i];
                int num2 = PyCode.codes[(int)num].IndexOf(ch, 7);
                if (num2 != -1)
                {
                    return PyCode.codes[(int)num].Substring(0, 6).Trim();
                }
            }
            return ch.ToString();
        }

        public static string GetPinyin(char ch, Encoding encoding)
        {
            ch = Pinyin.ConvertEncoding(ch.ToString(), encoding, Encoding.UTF8)[0];
            return Pinyin.ConvertEncoding(Pinyin.GetPinyin(ch), Encoding.UTF8, encoding);
        }

        public static string ConvertEncoding(string text, Encoding srcEncoding, Encoding dstEncoding)
        {
            byte[] bytes = srcEncoding.GetBytes(text);
            byte[] bytes2 = Encoding.Convert(srcEncoding, dstEncoding, bytes);
            return dstEncoding.GetString(bytes2);
        }

        private static short GetHashIndex(char ch)
        {
            return (short)((ulong)ch % (ulong)((long)PyCode.codes.Length));
        }
    }
}
