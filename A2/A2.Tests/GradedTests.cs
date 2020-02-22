using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace A2.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q1ShortestPath()
        {
            RunTest(new Q1ShortestPath("TD1"));
        }

        [TestMethod(), Timeout(1000)]
        public void SolveTest_Q2BipartiteGraph()
        {
            RunTest(new Q2BipartiteGraph("TD2"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A2", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}
