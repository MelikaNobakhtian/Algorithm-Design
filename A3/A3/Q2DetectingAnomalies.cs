using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies : Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long[] dist;

        public long Solve(long nodeCount, long[][] edges)
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

        public bool NegativeCycle(long nodeCount, long[][] edges, long start)
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
                return true;
            }
            return false;
        }
    }
}
