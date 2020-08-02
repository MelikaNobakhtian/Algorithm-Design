using System;
using System.Collections.Generic;

namespace fibo_last_digit
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            Console.WriteLine(Solve(n));
        }

        public static long Solve(long n)
        {
            var Fib = new List<long>() { 0, 1 };
            int i = 1;
            while (true)
            {
                i++;
                Fib.Add(((Fib[i - 2]) + (Fib[i - 1])) % 10);
                if (Fib[i - 1] == 0 && Fib[i] == 1)
                    break;
            }
            var idx = n % (Fib.Count - 2);
            return Fib[(int)idx];
        }
    }
}
