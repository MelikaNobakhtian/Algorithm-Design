using System;
using TestCommon;
using System.Linq;

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
        public string Solve(string bwt)
        {
            char[] characters = bwt.ToCharArray();
            char[] sorted = bwt.ToCharArray();
            int len = bwt.Length;
            int[] idx = new int[len];
            for (int i = 0; i < len; i++)
            {
                idx[i] = i;
            }
            QuickSort(idx, sorted, 0, len - 1);
            int[] indexofchar = new int[len];
            for (int i = 0; i < len; i++)
            {
                indexofchar[idx[i]] = i;
            }
            string result = "$";
            int j = 0;
            while (true)
            {
                if (characters[j] != '$')
                {
                    result = characters[j] + result;
                    j = indexofchar[j];
                }
                else
                    break;

            }
            return result;
        }

        private void QuickSort(int[] idx, char[] a, int s, int e)
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
