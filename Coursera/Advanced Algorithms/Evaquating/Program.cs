using System;
using System.Collections.Generic;

namespace Evaquating
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long nodeCount = long.Parse(arr[0]);
            long edgeCount = long.Parse(arr[1]);
            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
            {
                var arr1 = Console.ReadLine().Split(' ');
                edges[i] = new long[3];
                edges[i][0] = long.Parse(arr1[0]);
                edges[i][1] = long.Parse(arr1[1]);
                edges[i][2] = long.Parse(arr1[2]);
            }
            Console.WriteLine(Solve(nodeCount, edgeCount, edges));
        }

             public static long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            long maxflow = 0;
            long[,] residual = new long[nodeCount, nodeCount];
            long u, v, w;
            for (int i = 0; i < edgeCount; i++)
            {
                u = edges[i][0] - 1;
                v = edges[i][1] - 1;
                w = edges[i][2];
                residual[u, v] += w;
            }
            long[] path = new long[nodeCount];
            long min = 0;
            while (BFS_AugmentingPath(residual, path, nodeCount))
            {
                UpdateResidual(ref min, path, residual, nodeCount);
                maxflow += min;

            }
            return maxflow;

        }

        private static void UpdateResidual(ref long maxcap, long[] path, long[,] residual, long nodeCount)
        {
            maxcap = long.MaxValue;
            for (long ver = nodeCount - 1; ver != 0; ver = path[ver])
            {
                long parent = path[ver];
                maxcap = Math.Min(residual[parent, ver], maxcap);
            }

            for (long ver = nodeCount - 1; ver != 0; ver = path[ver])
            {
                long parent = path[ver];
                residual[parent, ver] -= maxcap;
                residual[ver, parent] += maxcap;
            }
        }

        public static bool BFS_AugmentingPath(long[,] residual, long[] path, long nodeCount)
        {
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(0);
            bool[] visit = new bool[nodeCount];
            visit[0] = true;
            while (queue.Count != 0)
            {
                long v = queue.Dequeue();
                for (int u = 0; u < nodeCount; u++)
                {
                    if (!visit[u] && residual[v, u] > 0 && v != u)
                    {
                        visit[u] = true;
                        path[u] = v;
                        queue.Enqueue(u);
                        if (u == nodeCount - 1)
                            return true;
                    }
                }
            }

            return visit[nodeCount - 1];
        }
    }
    }

