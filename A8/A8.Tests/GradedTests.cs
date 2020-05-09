using Microsoft.VisualStudio.TestTools.UnitTesting;
using A8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8.Tests
{
    [DeploymentItem("TestData", "A8_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q1Evaquating()
        {
            RunTest(new Q1Evaquating("TD1"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q2Airlines()
        {
            RunTest(new Q2Airlines("TD2"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q3Stocks()
        {
            RunTest(new Q3Stocks("TD3"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A8", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}