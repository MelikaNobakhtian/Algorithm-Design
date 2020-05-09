using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        public Q3Stocks(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            long nodes = stockCount * 2 + 2;
            long source = nodes - 2;
            long target = nodes - 1;
            long[,] bipartiteGraph = new long[nodes, nodes];
            List<long>[] edges = new List<long>[nodes];
            ConstructBipartite(bipartiteGraph, edges, stockCount, matrix, source, target, pointCount);
            long maxflow = Maxflow(nodes, bipartiteGraph, edges);
            return stockCount - maxflow;
        }

        public void ConstructBipartite(long[,] bipartiteGraph, List<long>[] edges, long stockCount, long[][] matrix, long source, long target, long pointCount)
        {
            edges[source] = new List<long>();
            edges[target] = new List<long>();
            for (int i = 0; i < stockCount; i++)
            {
                edges[i] = new List<long>();
                edges[i + stockCount] = new List<long>();
                for (int j = 0; j < stockCount; j++)
                {
                    if (i == j) continue;
                    if (IsUpper(matrix[i], matrix[j], pointCount))
                    {
                        bipartiteGraph[i, stockCount + j] = 1;
                        edges[i].Add(stockCount + j);
                    }

                }
                bipartiteGraph[source, i] = 1;
                edges[source].Add(i);
                bipartiteGraph[i + stockCount, target] = 1;
                edges[i + stockCount].Add(target);

            }
        }

        public bool IsUpper(long[] stock1, long[] stock2, long pointCount)
        {
            for (int i = 0; i < pointCount; i++)
                if (stock1[i] >= stock2[i])
                    return false;
            return true;
        }

        public virtual long Maxflow(long nodeCount, long[,] residual, List<long>[] neighbours)
        {
            long maxflow = 0;
            long[] path = new long[nodeCount];
            long min = 0;
            while (BFS_AugmentingPath(residual, path, nodeCount, ref min, neighbours))
            {
                UpdateResidual(min, path, residual, nodeCount, neighbours);
                maxflow += min;

            }
            return maxflow;

        }

        private void UpdateResidual(long maxcap, long[] path, long[,] residual, long nodeCount, List<long>[] adj)
        {
            for (long ver = nodeCount - 1; ver != nodeCount - 2; ver = path[ver])
            {
                long parent = path[ver];
                if (residual[ver, parent] == 0)
                    adj[ver].Add(parent);
                residual[parent, ver] -= maxcap;
                residual[ver, parent] += maxcap;
            }
        }

        public bool BFS_AugmentingPath(long[,] residual, long[] path, long nodeCount, ref long min, List<long>[] adj)
        {
            min = long.MaxValue;
            path[nodeCount - 2] = -1;
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(nodeCount - 2);
            bool[] visit = new bool[path.Length];
            visit[nodeCount - 2] = true;
            while (queue.Count != 0)
            {
                long v = queue.Dequeue();
                int len = adj[v].Count;
                for (int i = 0; i < len; i++)
                {
                    long u = adj[v][i];
                    if (!visit[u] && residual[v, u] != 0 && v != u)
                    {
                        visit[u] = true;
                        path[u] = v;
                        min = Math.Min(min, residual[v, u]);
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
