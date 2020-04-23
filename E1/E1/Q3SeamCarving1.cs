using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TestCommon;
using System.Drawing;

namespace Exam1
{
    public class Q3SeamCarving1 : Processor // Calculate Energy
    {
        public Q3SeamCarving1(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            // Parse input file
            Color[,] data = new Color[0, 0];
            var solved = Solve(data);
            // convert solved into output string
            return string.Empty;
        }
            

        public double[,] Solve(Color[,] data)
        {
            throw new NotImplementedException();
        }
    }

    public class Q3SeamCarving2 : Processor // Find Seam
    {
        public Q3SeamCarving2(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            // Parse input file
            double[,] data = new double[0, 0];
            var solved = Solve(data);
            // convert solved into output string
            return string.Empty;
        }


        public int[] Solve(double[,] data)
        {
            throw new NotImplementedException();
        }
    }

    public class Q3SeamCarving3 : Processor // Remove Seam
    {
        public Q3SeamCarving3(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            // Parse input file
            double[,] data = new double[0, 0];
            var solved = Solve(data);
            // convert solved into output string
            return string.Empty;
        }


        public double[,] Solve(double[,] data)
        {
            throw new NotImplementedException();
        }
    }
}
