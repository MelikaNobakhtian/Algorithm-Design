using System;
using System.Collections.Generic;

namespace fibo_again
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n=long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            Console.WriteLine(Solve(n, m));
        }

        public static long Solve(long a, long b)
        {
            var Fib = new List<long>() { 0, 1 };
            int i = 1;
            while (true)
            {
                i++;
                Fib.Add((Fib[i - 2] + Fib[i - 1]) % b);
                if (Fib[i - 1] == 0 && Fib[i] == 1)
                    break;
            }
            var idx = a % (Fib.Count - 2);
            return Fib[(int)idx];
        }
    }
}
