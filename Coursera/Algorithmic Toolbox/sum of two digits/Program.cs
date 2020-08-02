using System;

namespace sum_of_two_digits
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long a = long.Parse(arr[0]);
            long b = long.Parse(arr[1]);
            Console.WriteLine(Sum(a, b));
        }

        public static long Sum(long a, long b)
        {
            return a + b;
        }
    }
}
