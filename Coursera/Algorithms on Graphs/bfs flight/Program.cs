using System;
using System.Collections.Generic;

namespace bfs_flight
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
            var arr1 = Console.ReadLine().Split(' ');
            long u = long.Parse(arr1[0]);
            long v = long.Parse(arr1[1]);
            Console.WriteLine(Solve(n, edges, u, v));
        }

        public static long Solve(long NodeCount, long[][] edges,
                         long StartNode, long EndNode)
        {
            long[] dist = new long[NodeCount];
            List<List<long>> edge = new List<List<long>>();

            for (int i = 0; i < NodeCount; i++)
            {
                dist[i] = long.MaxValue;
                edge.Add(new List<long>());
            }
            foreach(var e in edges)
            {
                edge[(int)e[0] - 1].Add(e[1] - 1);
                edge[(int)e[1] - 1].Add(e[0] - 1);
            }
            dist[StartNode - 1] = 0;
            Queue<long> vertices = new Queue<long>();
            vertices.Enqueue(StartNode-1);
            while (vertices.Count != 0)
            {
                var u = vertices.Dequeue();
                foreach(var v in edge[(int)u])
                {
                    if (dist[v] == long.MaxValue)
                    {
                        vertices.Enqueue(v);
                        dist[v] = dist[u] + 1;
                    }
                }
                

            }

            return dist[EndNode - 1] != long.MaxValue ? dist[EndNode - 1] : -1;
        }
    }
}
