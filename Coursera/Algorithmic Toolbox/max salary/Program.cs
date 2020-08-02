using System;
using System.Collections.Generic;
using System.Linq;

namespace max_salary
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] numbers = new long[n];
            var arr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
                numbers[i] = long.Parse(arr[i]);
            Console.WriteLine(Solve(n, numbers));
        }

        public static string Solve(long n, long[] numbers)
        {

            string result = null;
            var listofnumbers = numbers.ToList();
            while (listofnumbers.Count > 0)
            {
                var max = FindBestChoice(listofnumbers);
                result += max.ToString();
                listofnumbers.Remove(max);


            }

            return result;
        }

        private static long FindBestChoice(List<long> listofnumbers)
        {
            var max = listofnumbers[0];
            for (int i = 1; i < listofnumbers.Count; i++)
                max = Maximize(listofnumbers[i], max);
            return max;
        }

        private static long Maximize(long v, long max)
        {
            var first = v.ToString() + max.ToString();
            var second = max.ToString() + v.ToString();
            if (long.Parse(first) > long.Parse(second))
                return v;
            return max;
        }

    }
}
