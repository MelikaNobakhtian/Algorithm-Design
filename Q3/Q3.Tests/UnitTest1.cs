using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Q3;
using System.Linq;

namespace Q3.Tests
{
    [TestClass]
    public class UnitTest1
    {
       
        
         [TestMethod()]
        public void SearchTest()
        {
            string text = "aabxaabxcaabxaabxay";
            string patern = "aabx";
            int[] resualts = { 0, 4, 9, 13 };
            
            List<int> res = Program.search(text, patern).ToList();
            CollectionAssert.AreEqual(resualts, res);


        }
    
    }
}
