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
