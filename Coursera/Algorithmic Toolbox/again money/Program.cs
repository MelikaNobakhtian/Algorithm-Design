using System;

namespace again_money
{
    class Program
    {
        static void Main(string[] args)
        {
            long money = long.Parse(Console.ReadLine());
            Console.WriteLine(Solve(money));
        }

        public static long Solve(long n)
        {
            long[] coins = new long[] { 1, 3, 4 };
            long[] MinNumCoins = new long[n + 1];
            MinNumCoins[0] = 0;
            long NumCoins;
            for (int i = 1; i <= n; i++)
            {
                MinNumCoins[i] = long.MaxValue;
                for (int j = 0; j < coins.Length; j++)
                {
                    if (i >= coins[j])
                    {
                        NumCoins = MinNumCoins[i - coins[j]] + 1;
                        if (NumCoins < MinNumCoins[i])
                        {
                            MinNumCoins[i] = NumCoins;
                        }
                    }

                }
            }

            return MinNumCoins[n];

        }
    }
}
