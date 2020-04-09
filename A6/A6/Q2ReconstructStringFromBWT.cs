using System;
using TestCommon;
using System.Linq;
using System.Collections.Generic;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName)
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public class charAndidx
        {
            public char Ch;
            public int Idx;
            public charAndidx(char ch, int idx)
            {
                Ch = ch;
                Idx = idx;
            }
        }
        public string Solve(string bwt)
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
            //string ff=str.ToCharArray()
            //har[] sorted = Array.Sort(bwt.ToCharArray());
            //long len = bwt.Length;
            //Dictionary<char, long> chars = new Dictionary<char, long>();
            //chars.Add('$', 0);
            //chars.Add('A', 1);
            //chars.Add('C', 2);
            //chars.Add('G', 3);
            //chars.Add('T', 4);
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
            //return null;
            char[] result = new char[len];
            //string result = null;
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

            //string r = result.ToString();

            return new string(result);
        }

        public static void QuickSort(int[] idx, char[] a, int s, int e)
        {
            if (e > s)
            {
                char pivot = a[s];
                int j = s;
                for (int i = s + 1; i <= e; i++)
                {
                    if (a[i] < pivot)
                    {
                        j += 1;
                        (a[i], a[j]) = (a[j], a[i]);
                        (idx[i], idx[j]) = (idx[j], idx[i]);
                    }
                }
                (a[s], a[j]) = (a[j], a[s]);
                (idx[s], idx[j]) = (idx[j], idx[s]);

                QuickSort(idx, a, s, j - 1);
                QuickSort(idx, a, j + 1, e);
            }
        }
    }
}
