using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q1MaxflowVertexCapacity : Processor
    {
        public Q1MaxflowVertexCapacity(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][],long[] , long, long, long>)Solve);

        public virtual long Solve(long nodeCount, 
            long edgeCount, long[][] edges, long[] nodeWeight, 
            long startNode , long endNode)
        {
            long maxflow = 0;
            long[,] residual = new long[nodeCount*2, nodeCount*2];
            long u, v, w;
            for (int i = 0; i < edgeCount; i++)
            {
                u = OutNode(edges[i][0]);
                v = InNode(edges[i][1]);
                w = edges[i][2];
                residual[u, v] += w;
            }
            for(int i = 0; i < nodeCount; i++)
            {
                u = InNode(i + 1);
                v = OutNode(i + 1);
                w = nodeWeight[i];
                residual[u, v] += w;
            }

            long[] path = new long[nodeCount*2];
            long min = 0;
            long start = InNode(1);
            long end = OutNode(nodeCount);
            while (BFS_AugmentingPath(residual, path, nodeCount*2,start,end))
            {
                UpdateResidual(ref min, path, residual,start,end);
                maxflow += min;

            }
            return maxflow;
        }

        private long InNode(long v) => (v - 1) * 2;   
        private long OutNode(long v) => (v - 1) * 2 + 1;

        private void UpdateResidual(ref long maxcap, long[] path, long[,] residual, long start,long end)
        {
            maxcap = long.MaxValue;
            for (long ver = end; ver != start; ver = path[ver])
            {
                long parent = path[ver];
                maxcap = Math.Min(residual[parent, ver], maxcap);
            }

            for (long ver = end; ver != start; ver = path[ver])
            {
                long parent = path[ver];
                residual[parent, ver] -= maxcap;
                residual[ver, parent] += maxcap;
            }
        }

        public bool BFS_AugmentingPath(long[,] residual, long[] path, long nodeCount,long start,long end)
        {
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(start);
            bool[] visit = new bool[nodeCount];
            visit[start] = true;
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
                        if (u == end)
                            return true;
                    }
                }
            }

            return visit[end];
        }

    }
}
