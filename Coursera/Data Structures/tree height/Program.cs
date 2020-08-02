using System;
using System.Collections.Generic;
using System.Linq;

namespace tree_height
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ');
            long[] nodes = new long[n];
            for (int i = 0; i < n; i++)
                nodes[i] = long.Parse(arr[i]);
            Console.WriteLine(Solve(n, nodes));
        }

        public static long Solve(long nodeCount, long[] tree)
        {
            long root = 0;
            List<List<long>> nodes = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
                nodes.Add(new List<long>());
            for (int i = 0; i < nodeCount; i++)
            {
                if (tree[i] == -1)
                {
                    root = i;
                    continue;
                }

                nodes[(int)tree[i]].Add(i);
            }

            Stack<long> childs = new Stack<long>();
            childs.Push(root);
            long[] depths = new long[nodeCount];
            depths[root] = 1;
            while (childs.Count != 0)
            {
                var parentnode = childs.Pop();
                if (nodes[(int)parentnode].Count != 0)
                {
                    for (int i = 0; i < nodes[(int)parentnode].Count; i++)
                    {
                        childs.Push(nodes[(int)parentnode][i]);
                        depths[nodes[(int)parentnode][i]] = depths[(int)parentnode] + 1;

                    }
                }


            }


            return depths.Max();


        }
    }
}
