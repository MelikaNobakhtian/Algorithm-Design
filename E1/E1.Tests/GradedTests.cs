using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace Exam1.Tests
{
    [DeploymentItem("TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1500)]
        public void SolveTest_Q1GeneticMutation()
        {
            RunTest(new Q1GeneticMutation("TD1"));
        }

        [TestMethod(), Timeout(12000)]
        public void SolveTest_Q2Outbreak()
        {
            RunTest(new Q2Outbreak("TD2"));
        }


        [TestMethod()]
        public void SolveTest_Q3SeamCarving()
        {
            RunTest(new Q3SeamCarving1("TD3.1"));
            RunTest(new Q3SeamCarving2("TD3.2"));
            RunTest(new Q3SeamCarving3("TD3.3"));
        }

        [TestMethod(), Timeout(30000)]
        public void SolveTest_Q4Vaccine()
        {
            RunTest(new Q4Vaccine("TD4"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("E1", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}