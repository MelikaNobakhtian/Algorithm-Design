using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);

        long[] dist;
        string[] pre;
        bool[] infinitDis;
        string[] path;

        public string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            dist = new long[nodeCount];
            pre = new string[nodeCount];
            infinitDis = new bool[nodeCount];
            path = new string[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                dist[i] = long.MaxValue;
            }
            BellmanFord(nodeCount, edges, startNode - 1);
            for (int i = 0; i < nodeCount; i++)
            {
                if (infinitDis[i] == true)
                    path[i] = "-";
                else if (pre[i] == null && dist[i] == long.MaxValue)
                    path[i] = "*";
                else
                    path[i] = dist[i].ToString();
            }
            return path;
        }

        public void BellmanFord(long nodeCount, long[][] edges, long start)
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
                if (result && !infinitDis[edge[1] - 1])
                {
                    InfiniteArbitrage(edges, nodeCount, edge[1] - 1);
                }
            }
        }

        private void InfiniteArbitrage(long[][] edges, long NodeCount, long StartNode)
        {
            bool[] process = new bool[NodeCount];
            process[StartNode] = true;
            infinitDis[StartNode] = true;
            Queue<long> vertices = new Queue<long>();
            vertices.Enqueue(StartNode);
            while (vertices.Count != 0)
            {
                var u = vertices.Dequeue();
                for (int i = 0; i < edges.Length; i++)
                    if (edges[i][0] - 1 == u)
                    {
                        var v = edges[i][1] - 1;
                        if (!infinitDis[v])
                            infinitDis[v] = true;
                        if (process[v] == false)
                        {
                            vertices.Enqueue(v);
                            process[v] = true;
                        }
                    }
            }
        }

        private bool Relax(long[] edge, long[] distance)
        {
            var v = edge[1] - 1;
            var u = edge[0] - 1;
            if (distance[u] == long.MaxValue)
                return false;
            var newdist = distance[u] + edge[2];
            if (distance[v] > newdist)
            {
                distance[v] = newdist;
                pre[v] = u.ToString();
                return true;
            }
            return false;
        }
    }
}
