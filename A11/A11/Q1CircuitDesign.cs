using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using Microsoft.SolverFoundation.Solvers;

namespace A11
{
    public class Q1CircuitDesign : Processor
    {
        public Q1CircuitDesign(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, 
                (Func<long, long, long[][], Tuple<bool, long[]>>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatAssignmentVerifier;

        public virtual Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
        {
            // write your code here
            throw new NotImplementedException();
        }
    }
}
