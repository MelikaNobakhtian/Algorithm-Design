using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace A3.Tests
{

    [DeploymentItem("TestData", "A3_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q1MinCost()
        {
            RunTest(new Q1MinCost("TD1"));
        }

        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q2DetectingAnomalies()
        {
            RunTest(new Q2DetectingAnomalies("TD2"));
        }


        [TestMethod(), Timeout(4000)]
        public void SolveTest_Q3ExchangingMoney()
        {
            RunTest(new Q3ExchangingMoney("TD3"));
        }


        [TestMethod(), Timeout(30000)]
        public void SolveTest_Q4FriendSuggestion()
        {
            RunTest(new Q4FriendSuggestion("TD4"));
        }


        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A3", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}
