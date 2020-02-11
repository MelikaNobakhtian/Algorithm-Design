using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace T1.Tests
{
    [DeploymentItem("TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(100)]
        public void SolveTest_Q0Problem()
        {
            RunTest(new Q0Problem("TD1"));            
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("T1", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }        
    }
}