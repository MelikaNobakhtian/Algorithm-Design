using Microsoft.VisualStudio.TestTools.UnitTesting;
using A9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A9.Tests
{
    [DeploymentItem("TestData", "A9_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(200)]
        public void SolveTest_Q1InferEnergyValues()
        {
            //Assert.Inconclusive("A9.Q1 Not Solved");
            RunTest(new Q1InferEnergyValues("TD1"));
        }

        [TestMethod(), Timeout(200)]
        public void SolveTest_Q2OptimalDiet()
        {
            //Assert.Inconclusive("A9.Q2 Not Solved");
            RunTest(new Q2OptimalDiet("TD2"));
        }

        [TestMethod(), Timeout(200)]
        public void SolveTest_Q3OnlineAdAllocation()
        {
            Assert.Inconclusive("A9.Q3 Not Solved");
            RunTest(new Q3OnlineAdAllocation("TD3"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A9", p.Process, p.TestDataName, p.Verifier, 
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}