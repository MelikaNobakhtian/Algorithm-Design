using System;
using System.Collections.Generic;

namespace Q3
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static int[] search(string text,
                         string pattern)
        {
            List<int> result = new List<int>();
            int[] zarray = Zfunction(pattern + '$' + text);
            for(int i = 0; i < zarray.Length; i++)
            {
                if (zarray[i] == pattern.Length)
                    result.Add(i -  pattern.Length - 1);
            }
            return result.ToArray();
        }

        public static int[] Zfunction(string text)
        {
            int[] result = new int[text.Length];
            int left = 0, right = 0;
            for(int i = 1; i < text.Length; i++)
            {
                if (i > right)
                {
                    left = i;
                    right = i;
                    while (text[right - left] == text[right] && right < text.Length)
                        right++;
                    result[i] = right - left;
                    right--;
                }
                else
                {
                    int idx = i - left;
                    if (result[idx] < right - i + 1)
                        result[i] = result[idx];
                    else
                    {
                        left = i;
                        while (text[right] == text[right - left] && right < text.Length)
                            right++;
                        result[i] = right - left;
                        right--;
                    }
                }
            }
            return result;
        }
    }
}
