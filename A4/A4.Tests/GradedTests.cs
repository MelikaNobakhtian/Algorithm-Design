using Microsoft.VisualStudio.TestTools.UnitTesting;
using A4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4.Tests
{
    [DeploymentItem("TestData", "A4_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q1BuildingRoads()
        {
            RunTest(new Q1BuildingRoads("TD1"));
        }

        [TestMethod(), Timeout(4000)]
        public void SolveTest_Q2Clustering()
        {
            RunTest(new Q2Clustering("TD2"));
        }


        [TestMethod()/*, Timeout(30000)*/]
        public void SolveTest_Q3ComputeDistance()
        {
            RunTest(new Q3ComputeDistance("TD3"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A4", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}