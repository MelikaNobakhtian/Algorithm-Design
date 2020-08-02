using System;
using System.Collections.Generic;

namespace convert_to_heap
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] array = new long[n];
            var arr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
                array[i] = long.Parse(arr[i]);
            var result = Solve(array);
            Console.WriteLine(result.Length);
            foreach (var a in result)
                Console.WriteLine(a.Item1 + " " + a.Item2);
        }

        public static Tuple<long, long>[] Solve(long[] array)
        {
            List<Tuple<long, long>> swaps = new List<Tuple<long, long>>();
            int last = array.Length / 2;
            for (int i = last; i >= 0; i--)
                SiftDown(i, array, swaps);


            return swaps.ToArray();
        }

        public static void SiftDown(int i, long[] heap, List<Tuple<long, long>> swaps)
        {
            int minindex = i;
            int leftchild;
            int rightchild;
            if (2 * i + 1 < heap.Length)
            {
                leftchild = 2 * i + 1;
                if (heap[leftchild] < heap[minindex])
                    minindex = leftchild;
            }
            if (2 * i + 2 < heap.Length)
            {
                rightchild = 2 * i + 2;
                if (heap[minindex] > heap[rightchild])
                    minindex = rightchild;
            }
            if (i != minindex)
            {
                var tmp = heap[i];
                heap[i] = heap[minindex];
                heap[minindex] = tmp;
                swaps.Add(new Tuple<long, long>(i, minindex));
                SiftDown(minindex, heap, swaps);
            }

        }
    }
}
