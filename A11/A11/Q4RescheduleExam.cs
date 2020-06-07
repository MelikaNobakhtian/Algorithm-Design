using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Solvers;
using TestCommon;

namespace A11
{
    public class Q4RescheduleExam : Processor
    {
        public Q4RescheduleExam(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, char[], long[][], char[]>) Solve);

        public override Action<string, string> Verifier =>
            TestTools.GraphColorVerifier;


        public virtual char[] Solve(long nodeCount, char[] colors, long[][] edges)
        {
            // write your code here
            throw new NotImplementedException();
        }
    }
}
