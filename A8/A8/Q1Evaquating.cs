using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
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

        private void UpdateResidual(ref long maxcap, long[] path, long[,] residual, long nodeCount)
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

        public bool BFS_AugmentingPath(long[,] residual, long[] path, long nodeCount)
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
