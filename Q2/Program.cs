using System;
using System.Collections.Generic;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = booyemoore("WELCOMETOTEAMMAST", "TEAMMAST");
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

        public static List<int> booyemoore(string text,string pattern)
        {
            var alphabet = BadChar(pattern);
            int skip = 0;
            List<int> result = new List<int>();
            for (int i = 0; i <= text.Length - pattern.Length; i += skip)
            {
                skip = 0;
                for(int j = pattern.Length - 1; j >= 0; j--)
                {
                    if (pattern[j] != text[i + j])
                    {
                        if (alphabet.ContainsKey(text[i + j]))
                        {
                            skip = alphabet[text[i + j]];
                            break;
                        }
                        else
                        {
                            skip = alphabet['*'];
                            break;
                        }
                    }

                }
                if (skip == 0)
                    result.Add(i);
                if (i == text.Length - pattern.Length)
                    return result;
            }
            return result;
        }
    }
}
