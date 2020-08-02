using System;

namespace advertisement_revenue
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] adrevenue = new long[n];
            long[] averageDailyclick = new long[n];
            var arr = Console.ReadLine().Split(' ');
            var arr2 = Console.ReadLine().Split(' ');
            for(int i = 0; i < n; i++)
            {
                adrevenue[i] = long.Parse(arr[i]);
                averageDailyclick[i] = long.Parse(arr2[i]);
            }
            Console.WriteLine(Solve(n, adrevenue, averageDailyclick));
        }

        public static long Solve(long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
            long allvalue = 0;
            while (slotCount > 0)
            {
                var first = FindMaxIdx(adRevenue);
                var second = FindMaxIdx(averageDailyClick);
                allvalue += adRevenue[first] * averageDailyClick[second];
                adRevenue[first] = long.MinValue;
                averageDailyClick[second] = long.MinValue;
                slotCount--;
            }

            return allvalue;

        }

        public static int FindMaxIdx(long[] a)
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
