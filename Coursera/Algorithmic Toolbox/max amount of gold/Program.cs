using System;

namespace max_amount_of_gold
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long w = long.Parse(arr[0]);
            long n = long.Parse(arr[1]);
            long[] coins = new long[n];
            var arr1 = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
            {
                coins[i] = long.Parse(arr1[i]);
            }
            Console.WriteLine(Solve(w, coins));
        }

        public static long Solve(long W, long[] goldBars)
        {
            long[,] weights = new long[W + 1, goldBars.Length + 1];
            for (int i = 1; i <= goldBars.Length; i++)
            {
                for (int j = 1; j <= W; j++)
                {
                    var value = long.MinValue;
                    weights[j, i] = weights[j, i - 1];
                    if (goldBars[i - 1] <= j)
                        value = weights[j - goldBars[i - 1], i - 1] + goldBars[i - 1];
                    if (value > weights[j, i])
                        weights[j, i] = value;
                }
            }
            return weights[W, goldBars.Length];
        }
    }
}
