using System;
using System.Collections.Generic;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = BadChar("aajjnka");
        }

        public static Dictionary<char,int> BadChar(string text)
        {
            int len = text.Length;
            Dictionary<char, int> alphabet = new Dictionary<char, int>();
            for(int i = 0; i < len; i++)
            {
                int value = Math.Max(1, len - i - 1);
                if (!alphabet.ContainsKey(text[i]))
                    alphabet.Add(text[i], value);
                else
                    alphabet[text[i]] = value;
            }
            alphabet.Add('*', len);
            return alphabet;
        }
    }
}
