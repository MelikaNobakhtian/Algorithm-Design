using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.Tests
{
    [TestClass()]
    public class ProgramTests
    { 

        [TestMethod()]
        public void booyemooreTest()
        {
            int[] result = new int[] { 14, 33 };
            CollectionAssert.AreEqual(result, Program.booyemoore("hi my name is alis and i live is alisis", "alis"));

            int[] result2 = new int[] { 0, 1, 2, 3 };
            CollectionAssert.AreEqual(result2, Program.booyemoore("aaaaaa", "aaa"));

            int[] result3 = new int[] { -1 };
            CollectionAssert.AreEqual(result3, Program.booyemoore("abbdbababa", "df"));
        }
    }
}