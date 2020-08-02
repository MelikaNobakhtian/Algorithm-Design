using System;

namespace binary_search
{
    class test
    {
       public string a;
        public test(string d)
        {
            a = d;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            test s = new test("vcvc");
             ref test dd =ref s;
            dd.a = "gf";
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long[] a = new long[n];
            for (int i = 1; i <= n; i++)
                a[i - 1] = long.Parse(arr[i]);
            var arr1 = Console.ReadLine().Split(' ');
            long m = long.Parse(arr1[0]);
            long[] b = new long[m];
            for (int i = 1; i <= m; i++)
                b[i - 1] = long.Parse(arr1[i]);
            var result = Solve(a, b);
            foreach (var r in result)
                Console.Write(r + " ");
        }

        public static long[] Solve(long[] a, long[] b)
        {
            var result = new long[b.Length];
            for (int i = 0; i < b.Length; i++)
                result[i] = BinarySearch(b[i], a, 0, a.Length - 1);
            return result;
        }

        public static long BinarySearch(long n, long[] a, int low, int high)
        {
            if (high < low)
                return -1;
            int mid = (high + low) / 2;
            if (a[mid] == n)
                return mid;
            return (a[mid] > n) ? BinarySearch(n, a, low, mid - 1) : BinarySearch(n, a, mid + 1, high);
        }
    }
}
