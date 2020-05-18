using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            Elimination(MATRIX_SIZE, matrix);
            double[] results = FindSolution(matrix, MATRIX_SIZE);
            for (int j = 0; j < MATRIX_SIZE; j++)
            {
                double percision = Math.Abs(results[j] - (long)results[j]);
                if (percision < 0.25)
                    results[j] = (long)results[j];
                else if (percision > 0.75)
                {
                    if (results[j] < 0)
                        results[j] = (long)results[j] - 1;
                    else
                        results[j] = (long)results[j] + 1;
                }
                else
                {
                    if (results[j] < 0)
                        results[j] = (long)results[j] - 0.5;
                    else
                        results[j] = (long)results[j] + 0.5;
                }

            }
            return results;
        }

        public double[] FindSolution(double[,] matrix, long size)
        {
            double[] result = new double[size];
            for (int idx = (int)size - 1; idx >= 0; idx--)
            {
                result[idx] = matrix[idx, size];
                for (int col = idx + 1; col < size; col++)
                    result[idx] -= matrix[idx, col] * result[col];
            }
            return result;
        }
        public int  FindPviot(double[,] matrix,long size,int i)
        {
            int pviot = i;
            double max = Math.Abs(matrix[pviot, i]);
            for(int j = i + 1; j < size; j++)
            {
                if (Math.Abs(matrix[j, i]) > max)
                {
                    max = Math.Abs(matrix[j, i]);
                    pviot = j;
                }
            }
            return pviot;
        }
        public void Elimination(long size, double[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                int pviot = FindPviot(matrix, size, i);
                if (pviot != i)
                    SwapRows(matrix, i, pviot, size);
                for (int j = i + 1; j < size; j++)
                {
                    double ratio = matrix[j, i] / matrix[i, i];
                    for (int k = i + 1; k <= size; k++)
                        matrix[j, k] -= matrix[i, k] * ratio;
                    matrix[j, i] = 0;
                }
                if (matrix[i, i] != 1)
                {
                    double divide = matrix[i, i];
                    for (int h = 0; h <= size; h++)
                        matrix[i, h] /= divide;
                }

            }
        }

        private void SwapRows(double[,] matrix, int i, int idx, long size)
        {
            for (int j = 0; j <= size; j++)
            {
                var tmp = matrix[i, j];
                matrix[i, j] = matrix[idx, j];
                matrix[idx, j] = tmp;
            }
        }
    }
}
