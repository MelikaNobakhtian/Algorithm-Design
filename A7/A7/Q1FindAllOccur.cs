using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, string pattern)
        {
            string concat = pattern + '$' + text;
            long[] result = PrefixFunction(concat, pattern.Length);
            if (result.Length == 0)
                result = new long[] { -1 };
            return result;
        }

        public static long[] PrefixFunction(string text,int patternLength)
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
