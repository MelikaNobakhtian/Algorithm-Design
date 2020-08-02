using System;
using System.Collections.Generic;

namespace strongly_connected_component
{
    class Program
    {
        public static Stack<long> ReversePostorder;
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
            ReversePostorder = new Stack<long>();
            List<List<long>> Rgraph = new List<List<long>>();
            List<List<long>> graph = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
            {
                Rgraph.Add(new List<long>());
                graph.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0] - 1].Add(edges[i][1] - 1);
                Rgraph[(int)edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            return StronglyConnectedComponents(graph, Rgraph, nodeCount);
        }

        public static long StronglyConnectedComponents(List<List<long>> graph, List<List<long>> Rgraph, long n)
        {
            DFs(graph, n);
            long result = 0;
            bool[] visit = new bool[n];
            var vertices = ReversePostorder.ToArray();
            foreach (var v in vertices)
            {
                if (!visit[v])
                {
                    Explore(Rgraph, visit, v);
                    result++;
                }
            }

            return result;
        }

        private static void Explore(List<List<long>> graph, bool[] visit, long v)
        {
            visit[v] = true;
            foreach (var ver in graph[(int)v])
            {
                if (!visit[ver])
                    Explore(graph, visit, ver);
            }
        }

        public static void DFs(List<List<long>> graph, long n)
        {
            bool[] Marked = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (!Marked[i])
                    TopologicalSort(graph, i, Marked);
            }
        }

        public static void TopologicalSort(List<List<long>> graph, long v, bool[] Marked)
        {
            Marked[v] = true;
            foreach (var ver in graph[(int)v])
                if (!Marked[ver])
                    TopologicalSort(graph, ver, Marked);
            ReversePostorder.Push(v);


        }
    }
}
