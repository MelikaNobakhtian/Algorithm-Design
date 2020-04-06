using System;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName)
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public string Solve(string text)
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
