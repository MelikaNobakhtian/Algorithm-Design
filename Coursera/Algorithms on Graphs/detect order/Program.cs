using System;
using System.Collections.Generic;

namespace detect_order
{
    class Program
    {
         public static long[] dist;

        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            long[][] edges = new long[m][];
            for(int i = 0; i < m; i++)
            {
                var arr1 = Console.ReadLine().Split(' ');
                var u = long.Parse(arr1[0]);
                var v = long.Parse(arr1[1]);
                var w = long.Parse(arr1[2]);
                long[] edge = new long[3];
                edge[0] = u;
                edge[1] = v;
                edge[2] = w;
                edges[i] = edge;
            }
            Console.WriteLine(Solve(n, edges));
        }

        public static long Solve(long nodeCount, long[][] edges)
        {
            dist = new long[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                dist[i] = long.MaxValue;
            }
            for (int i = 0; i < nodeCount; i++)
            {
                if (dist[i] == long.MaxValue)
                {
                    var result = NegativeCycle(nodeCount, edges, i);
                    if (result)
                        return 1;
                }
            }
            return 0;
        }

        public static bool NegativeCycle(long nodeCount, long[][] edges, long start)
        {
            dist[start] = 0;
            for (int i = 1; i < nodeCount; i++)
            {
                foreach (var edge in edges)
                    Relax(edge, dist);
            }
            foreach (var edge in edges)
            {
                var result = Relax(edge, dist);
                if (result)
                    return true;
            }
            return false;
        }

        private static bool Relax(long[] edge, long[] distance)
        {
            var v = edge[1] - 1;
            var u = edge[0] - 1;
            if (distance[u] == long.MaxValue)
                return false;
            var newdist = distance[u] + edge[2];
            if (distance[v] > newdist)
            {
                distance[v] = newdist;
                return true;
            }
            return false;
        }
    }
}
