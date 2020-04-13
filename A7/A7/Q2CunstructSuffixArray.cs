using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        protected virtual long[] Solve(string text)
        {
            // write your code here        
            throw new NotImplementedException();
        }
    }
}
