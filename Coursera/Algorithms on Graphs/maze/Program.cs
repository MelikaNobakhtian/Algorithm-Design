using System;
using System.Collections.Generic;

namespace maze
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            long[][] edges = new long[m][];
            for(int i = 0; i < m; i++)
            {
                var a = Console.ReadLine().Split(' ');
                long[] edge = new long[2];
                edge[0] = long.Parse(a[0]);
                edge[1] = long.Parse(a[1]);
                edges[i] = edge;
            }
            var arr1 = Console.ReadLine().Split(' ');
            long start = long.Parse(arr1[0]);
            long end = long.Parse(arr1[1]);
            Console.WriteLine(Solve(n, edges, start, end));
        }

        public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            List<List<long>> graph = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0] - 1].Add(edges[i][1] - 1);
                graph[(int)edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            return DFS(StartNode - 1, EndNode - 1, graph, nodeCount);
        }

        static long DFS(long start, long end, List<List<long>> graph, long n)
        {
            bool[] visit = new bool[n];
            return Explore(start, end, graph, visit);
        }

        private static long Explore(long start, long end, List<List<long>> graph, bool[] visit)
        {
            if (start == end)
                return 1;
            visit[start] = true;
            foreach (var v in graph[(int)start])
            {
                long result = 0;
                if (!visit[v])
                    result = Explore(v, end, graph, visit);
                if (result == 1) return 1;
            }
            return 0;
        }
    }
}
