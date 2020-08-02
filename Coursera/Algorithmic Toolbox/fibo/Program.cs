using System;

namespace fibo
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            Console.WriteLine(fibo(n));
        }

        public static long fibo(long n)
        {
            var Fib = new long[n + 1];
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            Fib[0] = 0;
            Fib[1] = 1;
            for (int i = 2; i <= n; i++)
                Fib[i] = Fib[i - 2] + Fib[i - 1];

            return Fib[n];

        }
    }
}
