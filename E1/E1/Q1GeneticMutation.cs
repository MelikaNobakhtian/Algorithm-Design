using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q1GeneticMutation : Processor
    {
        public Q1GeneticMutation(string testDataName) : base(testDataName) { }
        public override string Process(string inStr)
            => TestTools.Process(inStr, (Func<string, string, string>)Solve);


        static int no_of_chars = 256;

        public string Solve(string firstDNA, string secondDNA)
        {
            int len = firstDNA.Length;            
            int charidx = len - 2;
            string result = null;
            while (charidx != -1)
            {
                result = firstDNA.Substring(charidx + 1) + firstDNA.Substring(0, charidx + 1);
                if (result == secondDNA)
                    return "1";
                charidx--;
            }
            if (firstDNA == secondDNA)
                return "1";
            return "-1";
        }
    }
}
