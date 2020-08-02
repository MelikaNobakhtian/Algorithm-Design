using System;

namespace BW_transform
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(Console.ReadLine()));
        }

        public static string Solve(string text)
        {
            int len = text.Length;
            string[] texts = new string[len];
            texts[0] = text;
            int charidx = len - 2;
            long i = 1;
            while (charidx != -1)
            {
                texts[i] = text.Substring(charidx + 1) + text.Substring(0, charidx + 1);
                charidx--;
                i++;
            }
            Array.Sort(texts);
            string result = null;
            for (int j = 0; j < len; j++)
            {
                result += texts[j][len - 1];
            }
            return result;
        }
    }
}
