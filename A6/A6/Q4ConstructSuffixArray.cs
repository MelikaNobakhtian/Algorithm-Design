using System;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            int len = text.Length;
            long[] idx = new long[len];
            string[] suffix = new string[len];
            string word = null;
            for(int i = len - 1; i >= 0; i--)
            {
                word = text[i] + word;
                suffix[i] = word;
                idx[i] = i;
            }
            Array.Sort(suffix, idx);
            return idx;
        }
    }
}
