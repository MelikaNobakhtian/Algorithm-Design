using System;
using System.Collections.Generic;

namespace bw_inverse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(Console.ReadLine()));
        }

        public static string Solve(string bwt)
        {
            int len = bwt.Length;
            char[] sorted = bwt.ToCharArray();
            Array.Sort(sorted);
            Dictionary<char, int> countbefore = new Dictionary<char, int>();
            char present = sorted[0];
            countbefore.Add(present, 0);
            int counter = 1;
            for (int i = 1; i < len; i++)
            {
                if (present == sorted[i])
                    counter++;
                else
                {
                    present = sorted[i];
                    countbefore.Add(present, counter);
                    counter++;
                }
            }

            Dictionary<char, long> index = new Dictionary<char, long>();
            index.Add('$', 0);
            index.Add('A', 0);
            index.Add('C', 0);
            index.Add('G', 0);
            index.Add('T', 0);
            long[] periority = new long[len];
            for (int i = 0; i < len; i++)
            {
                index[bwt[i]] += 1;
                periority[i] = index[bwt[i]] + countbefore[bwt[i]];
            }
            char[] result = new char[len];
            long k = len - 2;
            long idx = 0;
            char start = bwt[(int)idx];
            result[len - 1] = '$';
            while (start != '$')
            {
                result[k] = start;
                idx = periority[idx] - 1;
                start = bwt[(int)idx];
                k--;
            }


            return new string(result);
        }
    }
}
