using System;

namespace LCM
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long a = long.Parse(arr[0]);
            long b = long.Parse(arr[1]);
            Console.WriteLine(Solve(a, b));
        }

        public static long Solve(long a, long b)
        {
            var gcd = GCD(a, b);
            return (a / gcd) * b;
        }

        public static long GCD(long a, long b)
        {
            if (a < b)
            {
                long c = a;
                a = b;
                b = c;
            }
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
    }
}
