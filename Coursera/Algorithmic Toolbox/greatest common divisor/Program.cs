using System;

namespace greatest_common_divisor
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
            if (a < b)
            {
                long c = a;
                a = b;
                b = c;
            }
            if (b == 0)
                return a;
            return Solve(b, a % b);
        }
    }
}
