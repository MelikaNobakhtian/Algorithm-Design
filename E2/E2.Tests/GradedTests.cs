using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon;

namespace E2.Tests
{
    [DeploymentItem("TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(5000)]
        public void SolveTest_Q1MaxflowVertexCapacity()
        {
            //Assert.Inconclusive("E2.Q1 Not Solved");            
            RunTest(new Q1MaxflowVertexCapacity("TD1"));
        }

        [TestMethod(), Timeout(10000)]
        public void SolveTest_Q2BoardGame()
        {
            //یک راهنمایی
            //Assert.Inconclusive("E2.Q2 Not Solved");
            RunTest(new Q2BoardGame("TD2"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("E2", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}