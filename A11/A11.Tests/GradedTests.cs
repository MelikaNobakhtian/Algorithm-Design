using Microsoft.VisualStudio.TestTools.UnitTesting;
using A11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11.Tests
{
    [DeploymentItem("TestData", "A11_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q1CircuitDesign()
        {
            Assert.Inconclusive();
            RunTest(new Q1CircuitDesign("TD1"));
        }

        [TestMethod(), Timeout(4000)]
        public void SolveTest_Q2FunParty()
        {
            RunTest(new Q2FunParty("TD2"));
        }

        [TestMethod(), Timeout(14000)]
        public void SolveTest_Q3SchoolBus()
        {
            RunTest(new Q3SchoolBus("TD3"));
        }

        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q4RescheduleExam()
        {
            Assert.Inconclusive();
            RunTest(new Q4RescheduleExam("TD4"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A11", p.Process, p.TestDataName,
             p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}