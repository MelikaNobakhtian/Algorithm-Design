using System;
using System.Collections.Generic;

namespace kmp
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();
            var result = Solve(text, pattern);
            foreach (var r in result)
                Console.Write(r + " ");
        }

        public static long[] Solve(string text, string pattern)
        {
            string concat = pattern + '$' + text;
            long[] result = PrefixFunction(concat, pattern.Length);
            return result;
        }

        public static long[] PrefixFunction(string text, int patternLength)
        {
            int len = text.Length;
            int[] borders = new int[len];
            List<long> results = new List<long>();
            int border = 0;
            for (int i = 1; i < len; i++)
            {
                while (border > 0 && text[i] != text[border])
                    border = borders[border - 1];
                if (text[i] == text[border])
                    border++;
                else
                    border = 0;
                borders[i] = border;
                if (border == patternLength)
                    results.Add(i - 2 * patternLength);
            }
            return results.ToArray();

        }
    }
}
