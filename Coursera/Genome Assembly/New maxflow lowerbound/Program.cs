using System;
using System.Collections.Generic;

namespace New_maxflow_lowerbound
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            long nodeCount = n + 2;
            long edgeCount = m * 2 + n * 4;
            long lowerbound = 0;
            long[] In = new long[n];
            long[] Out= new long[n];
            List<long>[] graph = new List<long>[nodeCount];
            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < nodeCount; i++)
                graph[i] = new List<long>();
            for(int i = 0; i < m; i++)
            {
                var edge = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
                AddEdge(graph, edge, edges, i);
                In[edge[1] - 1] += edge[2];
                Out[edge[0] - 1] += edge[2];
                lowerbound += edge[2];
            }
            long backandfor = m * 2;
            for(int i = 1; i < nodeCount - 1; i++)
            {
                graph[0].Add(backandfor);
                graph[i].Add(backandfor + 1);
                edges[backandfor] = new long[] { 0, i, 0, In[i - 1], 0 };
                edges[backandfor + 1] = new long[] { i, 0, 0, 0, 0 };
                graph[i].Add(backandfor + 2);
                graph[nodeCount - 1].Add(backandfor + 3);
                edges[backandfor + 2] = new long[] { i, nodeCount - 1, 0, Out[i - 1], 0 };
                edges[backandfor + 3] = new long[] { nodeCount - 1, i, 0, 0, 0 };
                backandfor += 4;
            }
            long flow = Maxflow(nodeCount, graph, edges);
            if (flow != lowerbound)
                Console.WriteLine("NO");
            else
            {
                Console.WriteLine("YES");
                for (int i = 0; i < m * 2; i += 2)
                    Console.WriteLine((edges[i][4] + edges[i][2]).ToString());
            }
        }

        private static void AddEdge(List<long>[] graph, long[] edge,long[][] edges, int i)
        {
            //idx=0 from
            //idx=1 to
            //idx=2 lowerbound
            //idx=3 capacity
            //idx=4 flow
            long forId = i * 2;
            long backId = forId + 1;
            long[] foredge = new long[5];
            long[] backedge = new long[5];
            backedge[0] = edge[1];backedge[1] = edge[0];
            for (int j = 0; j < 3; j++)
                foredge[j] = edge[j];
            foredge[3] = edge[3] - edge[2];
            edges[forId] = new long[5];edges[forId] = foredge;
            edges[backId] = new long[5];edges[backId] = backedge;
            graph[foredge[0]].Add(forId);
            graph[backedge[0]].Add(backId);
        }

        public static long Maxflow(long nodeCount, List<long>[]graph,long[][] edges)
        {
            long maxflow = 0;
            long[] path = new long[nodeCount];
            long min = 0;
            while (BFS_AugmentingPath(graph,edges,path,nodeCount))
            {
                UpdateResidual(ref min, path, edges, nodeCount);
                maxflow += min;

            }
            return maxflow;

        }

        private static void UpdateResidual(ref long maxcap, long[] path,long[][] edges, long nodeCount)
        {
            maxcap = long.MaxValue;
            for (long edgeNo = path[nodeCount-1]; edgeNo != -1; edgeNo = path[edges[edgeNo][0]])
            {
                maxcap = Math.Min(edges[edgeNo][3]-edges[edgeNo][4], maxcap);
            }

            for (long edgeNo = path[nodeCount - 1]; edgeNo != -1; edgeNo = path[edges[edgeNo][0]])
            {
                edges[edgeNo][4] += maxcap;
                edges[edgeNo ^ 1][4] -= maxcap;
            }
        }

        public static bool BFS_AugmentingPath(List<long>[] graph,long[][] edges, long[] path, long nodeCount)
        {
            for (int i = 0; i < nodeCount; i++)
                path[i] = -1;
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(0);
            bool[] visit = new bool[path.Length];
            visit[0] = true;
            while (queue.Count != 0)
            {
                long v = queue.Dequeue();
                foreach(long edgeNo in graph[v])
                {
                    long[] edge = edges[edgeNo];
                    long u = edges[edgeNo][1];
                    if(!visit[u] && edge[3]>edge[4] && v!=u && u != 0)
                    {
                        visit[u] = true;
                        path[u] = edgeNo;
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
