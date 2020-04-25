using Microsoft.VisualStudio.TestTools.UnitTesting;
using A7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7.Tests
{
    [DeploymentItem("TestData", "A7_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q1()
        {
            //Assert.Inconclusive("Not Solved");
            RunTest(new Q1FindAllOccur("TD1"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q2()
        {
            //Assert.Inconclusive("Not Solved");
            RunTest(new Q2CunstructSuffixArray("TD2"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q3()
        {
            //Assert.Inconclusive("Not Solved");
            RunTest(new Q3PatternMatchingSuffixArray("TD3"));
        }

        public static void RunTest(Processor p)
        { 
            TestTools.RunLocalTest("A7", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}