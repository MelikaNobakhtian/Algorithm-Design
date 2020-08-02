using System;

namespace inversion
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] a = new long[n];
            var arr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
            {
                a[i] = long.Parse(arr[i]);
            }
            Console.WriteLine(Solve(n, a));
        }

        public static long Solve(long n, long[] a)
        {
            var result = NumberofInversions(a, 0, (int)n - 1);
            return result.Item2;
        }

        public static Tuple<long[], long> NumberofInversions(long[] a, int low, int high)
        {

            var mid = (high + low) / 2;
            if (high == low)
                return new Tuple<long[], long>(new long[] { a[low] }, 0);
            var first = NumberofInversions(a, low, mid);
            var second = NumberofInversions(a, mid + 1, high);
            var result = MergeNumber(first.Item1, second.Item1);
            return new Tuple<long[], long>(result.Item1, result.Item2 + first.Item2 + second.Item2);


        }

        public static Tuple<long[], long> MergeNumber(long[] a, long[] b)
        {
            long result = 0;
            long[] d = new long[b.Length + a.Length];
            int i = 0, j = 0, k = 0;
            while (j < b.Length && k < a.Length)
            {
                var b1 = b[j];
                var a1 = a[k];
                if (b1 < a1)
                {

                    d[i] = b1;
                    j++;
                    i++;
                    result += a.Length - k;
                }

                else
                {
                    d[i] = a1;
                    k++;
                    i++;
                }
            }


            if (j < b.Length)
            {
                for (int h = j; h < b.Length; h++, i++)
                    d[i] = b[h];
            }
            else if (k < a.Length)
            {
                for (int h = k; h < a.Length; h++, i++)
                {
                    d[i] = a[h];

                }

            }


            return new Tuple<long[], long>(d, result);

        }
    }
}
