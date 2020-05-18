using System;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public string Solve(int c, int v, double[,] matrix1)
        {
            double[,] simplextableu = SimplexTableu(matrix1, v, c);
            int rows = c + 1;
            int cols = v + c + 1;
            int enteringcol, departingrow;
            while ((enteringcol=NegativeEntry(simplextableu, rows, cols)) != -1)
            {
                departingrow = FindPviot(simplextableu, enteringcol, rows, cols);
                if (departingrow == -1)
                    return "No solution";
            }
            throw new NotImplementedException();
        }

        public void Simplify(double[,] simplextableu,int pviotr,int pviotc,int rows,int cols)
        {
            double pviot = simplextableu[pviotr, pviotc];
            for(int i = 0; i < rows; i++)
            {
                if (i == pviotr) continue;
                double coefficient = pviot / simplextableu[i, pviotc];
                for (int j = 0; j < cols; j++)
                    simplextableu[i, j] -= coefficient * simplextableu[pviotr, j];
            }
            for (int j = 0; j < cols; j++)
                simplextableu[pviotr, j] /= pviot;
        }

        public int FindPviot(double[,] simplextableu,int enteringcol,int rows,int cols)
        {
            double min = double.MaxValue;
            int departingrow = -1;
            for(int i = 0; i < rows - 1; i++)
            {
                double ratio = simplextableu[i, cols - 1] / simplextableu[i, enteringcol];
                if (ratio > 0 && ratio<min && simplextableu[i,enteringcol]>0)
                {
                    min = ratio;
                    departingrow = i;
                }
            }

            return departingrow;
        }

        public int NegativeEntry(double[,] simplextableu,int rows,int cols)
        {
            double min = 0;
            int idx = -1;
            for (int j = 0; j < cols - 1; j++) 
                if (simplextableu[rows - 1, j] < min)
                {
                    min = simplextableu[rows - 1, j];
                    idx = j;
                }
            return idx;
        }

        public double[,] SimplexTableu(double[,] inequalities,int variables,int n)
        {
            int rows = n + 1;
            int cols = variables + n + 1;
            double[,] simplextableu = new double[rows, cols];
            for(int i = 0; i < n; i++)
            {
                for (int j = 0; j < variables; j++)
                    simplextableu[i, j] = inequalities[i, j];
                simplextableu[i, variables + i] = 1;
                simplextableu[i, cols - 1] = inequalities[i, variables];
            }
            for (int j = 0; j < variables; j++)
                simplextableu[rows - 1, j] -= inequalities[rows - 1, j];
            return simplextableu;
        }
    }
}
