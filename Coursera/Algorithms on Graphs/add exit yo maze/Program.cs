using System;
using System.Collections.Generic;

namespace add_exit_yo_maze
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
                graph[(int)edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            return newDfs(graph, nodeCount);
        }





        public static long newDfs(List<List<long>> graph, long n)
        {
            bool[] visit = new bool[n];
            long connectedcomp = 0;
            Stack<long> nodes = new Stack<long>();
            for (int i = 0; i < n; i++)
            {
                if (!visit[i])
                {
                    nodes.Push(i);
                    connectedcomp++;
                    while (nodes.Count != 0)
                    {
                        var node = nodes.Pop();
                        if (!visit[node])
                            visit[node] = true;
                        foreach (var v in graph[(int)node])
                        {
                            if (!visit[v])
                                nodes.Push(v);
                        }
                    }
                }
            }

            return connectedcomp;

        }
    }
}
