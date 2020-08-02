using System;

namespace max_pairwise
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ');
            long[] numbers = new long[n];
            for(int i = 0; i < n; i++)
            {
                numbers[i] = long.Parse(arr[i]);
            }
            Console.WriteLine(MaxPairwise(numbers));
        }

        public static long MaxPairwise(long[] numbers)
        {
            long firstmax = 0;
            for (int i = 1; i < numbers.Length; i++)
                if (numbers[i] > numbers[firstmax])
                    firstmax = i;
            long secondmax = 0;
            if (firstmax == 0)
                secondmax++;
            for (int i = 1; i < numbers.Length; i++)
                if ((firstmax != i) && (numbers[secondmax] <= numbers[i]))
                    secondmax = i;

            return numbers[firstmax] * numbers[secondmax];
        }
    }
}
