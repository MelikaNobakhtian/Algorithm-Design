using System;
using System.Collections.Generic;

namespace square_sum
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            if (n != 0)
                Console.WriteLine(Solve(n));
            else
                Console.WriteLine(0);
        }

        public static long Solve(long n)
        {
            var last = LastDigitFib(n);
            var result = (last * ((LastDigitFib(n - 1) + last) % 10)) % 10;
            return result;
        }

        public static long LastDigitFib(long n)
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
