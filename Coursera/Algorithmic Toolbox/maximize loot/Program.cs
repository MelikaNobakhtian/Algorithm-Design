using System;

namespace maximize_loot
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            double result = 0;
            long weight = 0;
            long capacity = long.Parse(arr[1]);
            long[] weights = new long[n];
            long[] values = new long[n];
            for(int i = 0; i < n; i++)
            {
                var arr2 = Console.ReadLine().Split(' ');
                values[i] = long.Parse(arr2[0]);
                weights[i] = long.Parse(arr2[1]);
                weight += weights[i];
            }
            if(weight>=capacity)
                Console.WriteLine(Math.Round(Solve(capacity, weights, values),4));
            else
            {
                foreach (var v in values)
                    result += v;
                Console.WriteLine(Math.Round(result,4));
            }
        }

        public static double Solve(long capacity, long[] weights, long[] values)
        {
            double value = 0;
            int length = weights.Length;
            var valueperweight = new double[length];
            for (int i = 0; i < length; i++)
            {
                valueperweight[i] = (double)values[i] / (double)weights[i];
            }
            for (int i = 0; capacity > 0; i++)
            {
                var best = FindMaxIdx(valueperweight);
                if (weights[best] <= capacity)
                {
                    capacity -= weights[best];
                    value += values[best];
                    valueperweight[best] = 0;
                }
                else
                {
                    var cap = capacity * valueperweight[best];
                    value += cap;
                    capacity = 0;
                }

            }

            return value;
        }

        public static int FindMaxIdx(double[] a)
        {
            var max = a[0];
            int idx = 0;
            for (int i = 1; i < a.Length; i++)
                if (a[i] > max)
                {
                    max = a[i];
                    idx = i;
                }
            return idx;

        }
    }
}
