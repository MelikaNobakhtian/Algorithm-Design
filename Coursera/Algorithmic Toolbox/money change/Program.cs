using System;

namespace money_change
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            Console.WriteLine(Solve(n));
        }

        public static long Solve(long money)
        {

            var coin = new[] { 10, 5, 1 };
            long coincount = 0;
            int i = 0;
            while (money != 0)
            {
                coincount += money / coin[i];
                money = money % coin[i];
                i++;
            }
            return coincount;

        }
    }
}
