using System;
using TestCommon;

namespace T1
{
    public class Q0Problem: Processor
    {
        public Q0Problem(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr)
        {
            return Solve(int.Parse(inStr)).ToString();
        }

        public int Solve(int n) => n+1;        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

    }
}
