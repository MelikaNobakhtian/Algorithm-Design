using System;
using System.Collections.Generic;

namespace last_digit_of_fibo_sum
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
            var sumdigit = new List<long>() { 0, 1 };
            long sum = 1;
            int i = 1;
            while (true)
            {
                i++;
                Fib.Add(((Fib[i - 2]) + (Fib[i - 1])) % 10);
                sum = (sum + Fib[i]) % 10;
                sumdigit.Add(sum);
                if (sumdigit[i - 1] == 0 && sumdigit[i] == 1)
                    break;
            }
            var idx = n % (sumdigit.Count - 2);
            return sumdigit[(int)idx];
        }
    }
}
