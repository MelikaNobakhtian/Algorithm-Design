using Microsoft.VisualStudio.TestTools.UnitTesting;
using A6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6.Tests
{
    [TestClass()]
    [DeploymentItem("TestData", "A6_TestData")]
    public class GradedTests
    {
        [TestMethod(), Timeout(500)]
        public void SolveTest_Q1ConstructBWT()
        {
            RunTest(new Q1ConstructBWT("TD1"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q2ReconstructStringFromBWT()
        {
            RunTest(new Q2ReconstructStringFromBWT("TD2"));
        }

        [TestMethod(), Timeout(500)]
        public void SolveTest_Q3MatchingAgainCompressedString()
        {
            RunTest(new Q3MatchingAgainCompressedString("TD3"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q4ConstructSuffixArray()
        {
            RunTest(new Q4ConstructSuffixArray("TD4"));
        }
        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A6", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}