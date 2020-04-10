using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string BWT(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public long[] Solve(string text, long n, String[] patterns)
        {
            long[] results = new long[n];
            char[] characters = text.ToCharArray();
            char[] sorted = text.ToCharArray();
            int len = text.Length;
            Array.Sort(sorted);
            Dictionary<char,List<int>> count = new Dictionary<char,List<int>>
            {
                {'A',new List<int>(){0} },
                {'T',new List<int>(){0 } },
                {'C',new List<int>(){0 } },
                {'G',new List<int>(){0 } },
                {'$',new List<int>(){0 } }
            };
            Dictionary<char, int> firsttime = new Dictionary<char, int>
            {
                {'A',int.MaxValue },
                {'T',int.MaxValue },
                {'C',int.MaxValue },
                {'G',int.MaxValue  },
                {'$',int.MaxValue }
            };
            for (int i=0;i<len;i++)
            {
                if (firsttime[sorted[i]] > i)
                    firsttime[sorted[i]] = i;
                foreach(var letter in count)
                {
                    char key = letter.Key;
                    int val = letter.Value[i];
                    if (characters[i] == key)
                    {
                        val++;
                    }
                    
                    letter.Value.Add(val);

                }
            }
            for(int i = 0; i < n; i++)
            {
                results[i] = BWmatching( firsttime, len, patterns[i], count);
            }

            return results;


        }

        public static long BWmatching(Dictionary<char,int> FirstOccurrence, int Length,string Pattern,Dictionary<char,List<int>> Count)
        {
            int top = 0;
            int bottom = Length - 1;
            int patternlen = Pattern.Length;
            while (top <= bottom)
            {
                if (patternlen != 0)
                {
                    char last = Pattern[patternlen - 1];
                    Pattern = Pattern.Remove(patternlen - 1);
                    patternlen--;
                    top = FirstOccurrence[last] + Count[last][top];
                    bottom = FirstOccurrence[last] + Count[last][bottom + 1] - 1;
                }
                else
                    return bottom - top + 1;
            }
            return 0;
        }


    }
}
