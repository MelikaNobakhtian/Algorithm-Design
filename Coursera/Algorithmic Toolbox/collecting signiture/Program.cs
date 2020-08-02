using System;
using System.Collections.Generic;

namespace collecting_signiture
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] starttimes = new long[n];
            long[] endtimes = new long[n];
            for(int i = 0; i < n; i++)
            {
                var arr = Console.ReadLine().Split(' ');
                starttimes[i] = long.Parse(arr[0]);
                endtimes[i] = long.Parse(arr[1]);
            }
            var result = Solve(n, starttimes, endtimes);
            Console.WriteLine(result.Count);
            foreach(var r in result)
            {
                Console.WriteLine(r + " ");
            }
        }

        public static List<long> Solve(long tenantCount, long[] startTimes, long[] endTimes)
        {
            List<long> result = new List<long>();
            while (tenantCount > 0)
            {
                var idx = FindMin(endTimes);
                for (int i = 0; i < endTimes.Length; i++)
                {
                    if (endTimes[idx] >= startTimes[i] && endTimes[idx] <= endTimes[i] && idx != i)
                    {
                        tenantCount--;
                        startTimes[i] = long.MaxValue;
                        endTimes[i] = long.MaxValue;
                    }
                    if (idx == i)
                        tenantCount--;
                }
                result.Add(endTimes[idx]);
                startTimes[idx] = long.MaxValue;
                endTimes[idx] = long.MaxValue;

            }

            return result;


        }



        public static long FindMin(long[] a)
        {
            var min = a[0];
            var idx = 0;
            for (int i = 1; i < a.Length; i++)
                if (a[i] < min && a[i] != 0)
                {
                    min = a[i];
                    idx = i;
                }
            return idx;

        }

    }
}
