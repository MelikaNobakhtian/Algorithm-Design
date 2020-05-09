using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);

        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            long[] matching = new long[flightCount];
            long[,] network = ConstructNetwork(info, flightCount, crewCount);
            long maxflow = Maxflow(flightCount + crewCount + 2, network);
            for (int i = 0; i < flightCount; i++)
            {
                for (int j = 0; j < crewCount; j++)
                {
                    matching[i] = -1;
                    if (info[i][j] == 1 && network[i, j + flightCount] == 0)
                    {
                        matching[i] = j + 1;
                        break;
                    }
                }
            }
            return matching;

        }

        public virtual long Maxflow(long nodeCount, long[,] residual)
        {
            long maxflow = 0;
            long[] path = new long[nodeCount];
            long min = 0;
            while (BFS_AugmentingPath(residual, path, nodeCount))
            {
                UpdateResidual(min, path, residual, nodeCount);
                maxflow += min;

            }
            return maxflow;

        }

        private void UpdateResidual(long maxcap, long[] path, long[,] residual, long nodeCount)
        {
            maxcap = long.MaxValue;
            for (long ver = nodeCount - 1; ver != nodeCount - 2; ver = path[ver])
            {
                long parent = path[ver];
                maxcap = Math.Min(residual[parent, ver], maxcap);
            }

            for (long ver = nodeCount - 1; ver != nodeCount - 2; ver = path[ver])
            {
                long parent = path[ver];
                residual[parent, ver] -= maxcap;
                residual[ver, parent] += maxcap;
            }
        }

        public bool BFS_AugmentingPath(long[,] residual, long[] path, long nodeCount)
        {
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(nodeCount - 2);
            bool[] visit = new bool[path.Length];
            visit[nodeCount - 2] = true;
            while (queue.Count != 0)
            {
                long v = queue.Dequeue();
                for (int i = 0; i < nodeCount; i++)
                {
                    long u = i;
                    if (!visit[u] && residual[v, u] != 0 && v != u)
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

        public long[,] ConstructNetwork(long[][] info, long u, long v)
        {
            long n = u + v + 2;
            long[,] network = new long[n, n];
            for (int i = 0; i < u; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    if (i == 0)
                    {
                        network[u + j, u + v + 1] = 1;
                    }
                    network[i, u + j] = info[i][j];
                }
                network[v + u, i] = 1;

            }
            return network;

        }
    }
}
