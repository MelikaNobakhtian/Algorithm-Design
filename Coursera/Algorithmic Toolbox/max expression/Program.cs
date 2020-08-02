using System;
using System.Collections.Generic;

namespace max_expression
{
    class Program
    {
        public static Dictionary<char, Func<long, long, long>> Operators =
          new Dictionary<char, Func<long, long, long>>()
          {
              ['+'] = (x, y) => x + y,
              ['-'] = (x, y) => x - y,
              ['/'] = (x, y) => x / y,
              ['*'] = (x, y) => x * y,
          };
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(Console.ReadLine()));
        }

        public static long Solve(string expression)
        {

            int n = expression.Length / 2 + 1;
            long[,] maxofexpression = new long[expression.Length / 2 + 1, expression.Length / 2 + 1];
            long[,] minofexpression = new long[expression.Length / 2 + 1, expression.Length / 2 + 1];
            for (int i = 0, j = 0; i < n; i++, j += 2)
            {
                maxofexpression[i, i] = long.Parse(expression[j].ToString());
                minofexpression[i, i] = long.Parse(expression[j].ToString());
            }
            for (int i = 1; i <= n - 1; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    int k = i + j;
                    FindMinAndMax(expression, minofexpression, maxofexpression, j, k);
                }
            }

            return maxofexpression[0, n - 1];
        }

        public static void FindMinAndMax(string exp, long[,] min, long[,] max, int i, int j)
        {

            long maxexp = long.MinValue;
            long minexp = long.MaxValue;
            for (int h = i; h < j; h++)
            {
                char op = exp[2 * h + 1];
                var first = Operators[op](max[i, h], max[h + 1, j]);
                var second = Operators[op](max[i, h], min[h + 1, j]);
                var third = Operators[op](min[i, h], max[h + 1, j]);
                var forth = Operators[op](min[i, h], min[h + 1, j]);
                var minall = Math.Min(Math.Min(third, Math.Min(first, second)), forth);
                var maxall = Math.Max(Math.Max(third, Math.Max(first, second)), forth);
                minexp = Math.Min(minexp, minall);
                maxexp = Math.Max(maxall, maxexp);

            }
            max[i, j] = maxexp;
            min[i, j] = minexp;
        }
    }
}
