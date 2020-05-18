using System;
using TestCommon;

namespace A9
{
    public class Q2OptimalDiet : Processor
    {
        public Q2OptimalDiet(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);
        public static long[][] Subsets;
        public static long Idx;
        public string Solve(int N, int M, double[,] matrix1)
        {
            string str = "Bounded Solution";
            Idx = 0;
            long numberofsubsets = Factoriel(M + N) / (Factoriel(M) * Factoriel(N));
            Subsets = new long[numberofsubsets][];
            long status = -1;
            double maximum = double.MinValue;
            long[] set = new long[M + N];
            long[] subset = new long[M];
            double[] answers = new double[M];
            double[,] selectedequ = new double[M, M + 1];
            double[,] equations = AddEquationsAndMakeSet(matrix1, set, M, N);
            SubsetofSizeM(set, M, M + N, subset, 0, 0);
            for (int i = 0; i < numberofsubsets; i++)
            {
                bool[] used = new bool[M + N];
                PutIntoArray(Subsets[i], equations, selectedequ);
                var result = Guass(M, selectedequ);
                bool condition = SatisfyConditions(result, equations, used, M + N);
                if (!condition)
                    continue;
                var max = CheckForMax(matrix1, result, N, status);
                status = max.Item2;
                if (status == 0 && max.Item1 >= maximum)
                {
                    answers = result;
                    maximum = max.Item1;
                }
            }
            if (status == 0)
            {
                RoundSolution(answers, M);
                str += '\n';
                for (int i = 0; i < M; i++)
                    str += $"{answers[i]} ";
                return str;
            }
            if (status == -1)
                return "No Solution";
            else
                return "Infinity";
        }
        public void RoundSolution(double[] results, long m)
        {
            for (int j = 0; j < m; j++)
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
        }

        private (double, long) CheckForMax(double[,] matrix1, double[] result, long n, long status)
        {
            long m = result.Length;
            double max = 0;
            for (int i = 0; i < m; i++)
            {
                if (double.IsNegativeInfinity(result[i]))
                    return (0, status);
            }
            for (int i = 0; i < m; i++)
            {
                if (double.IsPositiveInfinity(result[i]))
                    return (1, status);
                max += result[i] * matrix1[n, i];
            }
            return (Math.Round(max, 3), 0);
        }

        private bool SatisfyConditions(double[] result, double[,] equations, bool[] used, long size)
        {
            long len = result.Length;
            for (int i = 0; i < size; i++)
            {
                double amount = 0;
                for (int j = 0; j < len; j++)
                {
                    if (double.IsNaN(result[j]) || double.IsInfinity(result[j]))
                        return false;
                    amount += result[j] * equations[i, j];
                }
                if (amount > equations[i, len])
                    return false;
            }
            return true;
        }

        private void PutIntoArray(long[] v, double[,] equations, double[,] selectedequ)
        {
            long len = v.Length;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j <= len; j++)
                    selectedequ[i, j] = equations[v[i], j];
            }
        }

        public long Factoriel(long n)
        {
            long result = 1;
            while (n != 1)
            {
                result *= n;
                n--;
            }
            return result;
        }

        public double[,] AddEquationsAndMakeSet(double[,] matrix, long[] set, int m, int n)
        {
            double[,] inequalities = new double[m + n, m + 1];
            for (int i = 0; i < n; i++)
            {
                set[i] = i;
                for (int j = 0; j <= m; j++)
                    inequalities[i, j] = matrix[i, j];
            }
            for (int i = n; i < m + n; i++)
            {
                inequalities[i, i - n] = -1;
                set[i] = i;
            }
            return inequalities;
        }

        public void SubsetofSizeM(long[] set, long m, long size, long[] subset, long indexofset, long indexofsubset)
        {
            if (indexofsubset == m)
            {
                Subsets[Idx] = new long[m];
                for (int i = 0; i < m; i++)
                    Subsets[Idx][i] = subset[i];
                Idx++;
                return;
            }
            if (indexofset >= size)
                return;
            subset[indexofsubset] = set[indexofset];
            SubsetofSizeM(set, m, size, subset, indexofset + 1, indexofsubset + 1);
            SubsetofSizeM(set, m, size, subset, indexofset + 1, indexofsubset);
        }

        public double[] Guass(long MATRIX_SIZE, double[,] matrix)
        {
            Elimination(MATRIX_SIZE, matrix);
            double[] results = FindSolution(matrix, MATRIX_SIZE);
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

        public void Elimination(long size, double[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                int idx = i;
                double value = Math.Abs(matrix[idx, i]);

                for (int j = i + 1; j < size; j++)
                {
                    if (Math.Abs(matrix[j, i]) > value)
                    {
                        value = Math.Abs(matrix[j, i]);
                        idx = j;
                    }

                }
                if (idx != i)
                    SwapRows(matrix, i, idx, size);
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
