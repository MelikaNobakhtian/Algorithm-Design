using System;
using System.Collections.Generic;

namespace partial_sum
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long a = long.Parse(arr[0]);
            long b = long.Parse(arr[1]);
            if (a == 0 || b == 0)
            {
                long c = a != 0 ? a : b;
                Console.WriteLine(LastDigit(c));

            }
            else
                Console.WriteLine(Solve(a, b));
        }

        public static long Solve(long a, long b)
        {
            if (a > b)
            {
                long c = a;
                a = b;
                b = c;
            }
            var last = LastDigit(b);
            var first = LastDigit(a - 1);
            var mod = last - first;
            if (mod >= 0)
                return mod;
            else
                return mod + 10;

        }

        public static long LastDigit(long n)
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
