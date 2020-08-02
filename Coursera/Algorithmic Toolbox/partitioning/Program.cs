using System;
using System.Collections.Generic;

namespace partitioning
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] souvenirs = new long[n];
            var arr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
                souvenirs[i] = long.Parse(arr[i]);
            Console.WriteLine(Solve(n, souvenirs));
        }

        public static long Solve(long souvenirsCount, long[] souvenirs)
        {
            long sum = 0;
            for (int i = 0; i < souvenirs.Length; i++)
            {
                sum += souvenirs[i];
            }
            if (sum % 3 != 0 || souvenirsCount < 3)
                return 0;
            else
                sum /= 3;

            Dictionary<string, long> allsubsets = new Dictionary<string, long>();
            return IsthereSubset(souvenirs, souvenirsCount - 1, sum, sum, sum, allsubsets);
          
        }


        private static long IsthereSubset(long[] souvenirs, long lastidx, long sum1, long sum2, long sum3, Dictionary<string, long> allsubsets)
        {
            if (sum1 == 0 && sum2 == 0 && sum3 == 0)
                return 1;
            if (lastidx < 0)
                return 0;

            string subset = sum1.ToString() + "," + sum2.ToString() + "," + sum3.ToString() + "," + lastidx.ToString();
            if (!allsubsets.ContainsKey(subset))
            {
                long firstsubset = 0;
                if (sum1 - souvenirs[lastidx] >= 0)
                    firstsubset = IsthereSubset(souvenirs, lastidx - 1, sum1 - souvenirs[lastidx], sum2, sum3, allsubsets);

                long secondsubset = 0;
                if (firstsubset == 0 && sum2 - souvenirs[lastidx] >= 0)
                    secondsubset = IsthereSubset(souvenirs, lastidx - 1, sum1, sum2 - souvenirs[lastidx], sum3, allsubsets);

                long thirdsubset = 0;
                if (firstsubset == 0 && secondsubset == 0 && sum3 - souvenirs[lastidx] >= 0)
                    thirdsubset = IsthereSubset(souvenirs, lastidx - 1, sum1, sum2, sum3 - souvenirs[lastidx], allsubsets);

                allsubsets.Add(subset, (firstsubset == 1 || secondsubset == 1 || thirdsubset == 1) ? 1 : 0);
            }

            return allsubsets[subset];
        }
    }
}
