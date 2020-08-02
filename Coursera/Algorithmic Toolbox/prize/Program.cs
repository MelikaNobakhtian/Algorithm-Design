using System;
using System.Collections.Generic;

namespace prize
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            var result = Solve(n);
            Console.WriteLine(result.Length);
            foreach (var r in result)
                Console.Write(r + " ");
        }

        public static long[] Solve(long n)
        {
            var result = new List<long>();
            int i = 1;
            while (n - i >= 0)
            {
                result.Add(i);
                n = n - i;
                i++;
            }
            if (n - i < 0)
            {
                result[result.Count - 1] += n;
            }

            return result.ToArray();
        }
    }
}
