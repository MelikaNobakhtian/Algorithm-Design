using System;
using System.Collections.Generic;

namespace acycle
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            long[][] edges = new long[m][];
            for (int i = 0; i < m; i++)
            {
                var a = Console.ReadLine().Split(' ');
                long[] edge = new long[2];
                edge[0] = long.Parse(a[0]);
                edge[1] = long.Parse(a[1]);
                edges[i] = edge;
            }
            Console.WriteLine(Solve(n, edges));
        }

        public static long Solve(long nodeCount, long[][] edges)
        {
            List<List<long>> graph = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0] - 1].Add(edges[i][1] - 1);
            }

            return HasCycle(graph, nodeCount);
        }

        public static long HasCycle(List<List<long>> graph, long n)
        {
            bool[] visit = new bool[n];
            bool[] inStack = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (DFs(i, graph, visit, inStack))
                    return 1;
            }
            return 0;
        }

        private static bool DFs(int i, List<List<long>> graph, bool[] visit, bool[] inStack)
        {
            if (!visit[i])
            {
                visit[i] = true;
                inStack[i] = true;
                foreach (var v in graph[i])
                {
                    if (!visit[v] && DFs((int)v, graph, visit, inStack))
                        return true;
                    else if (inStack[v])
                        return true;
                }
            }
            inStack[i] = false;
            return false;
        }
    }
}
