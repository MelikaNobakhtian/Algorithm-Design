using System;
using System.Collections.Generic;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            long a = long.Parse(Console.ReadLine());
            var result = Solve(a);
            Console.WriteLine(result.Length-1);
            foreach (var r in result)
                Console.Write(r + " ");

        }

        public static long[] Solve(long n)
        {
            long[] operationcount = new long[n + 1];
            operationcount[0] = 0;
            operationcount[1] = 1;
            long minoperations = long.MaxValue;
            for (int i = 2; i <= n; i++)
            {
                minoperations = long.MaxValue;
                var count3 = long.MaxValue;
                var count2 = long.MaxValue;
                var count1 = long.MaxValue;
                if (i % 3 == 0)
                    count3 = operationcount[i / 3] + 1;
                if (i % 2 == 0)
                    count2 = operationcount[i / 2] + 1;
                minoperations = (count2 < count3) ? count2 : count3;
                count1 = operationcount[i - 1] + 1;
                minoperations = (count1 < minoperations) ? count1 : minoperations;
                operationcount[i] = minoperations;

            }

            var index = n;
            Stack<long> numbers = new Stack<long>();
            numbers.Push(n);
            var goalindex = long.MaxValue;
            while (index != 1)
            {
                if (index % 3 == 0)
                {
                    goalindex = operationcount[index / 3] + 1;
                    if (goalindex == operationcount[index])
                    {
                        numbers.Push(index / 3);
                        index /= 3;
                        continue;
                    }

                }

                if (index % 2 == 0)
                {
                    goalindex = operationcount[index / 2] + 1;
                    if (goalindex == operationcount[index])
                    {
                        numbers.Push(index / 2);
                        index /= 2;
                        continue;
                    }
                }

                goalindex = operationcount[index - 1] + 1;
                if (goalindex == operationcount[index])
                {
                    numbers.Push(index - 1);
                    index -= 1;
                    continue;
                }

            }

            return numbers.ToArray();
        }
    }
}
