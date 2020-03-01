using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using Priority_Queue;
namespace A3
{
    public class Q4FriendSuggestion : Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long, long[][], long[]>)Solve);


        public long[] Solve(long NodeCount, long EdgeCount,
                                long[][] edges, long QueriesCount,
                                long[][] Queries)
        {
            List<long>[] neighbours = new List<long>[NodeCount];
            List<long>[] w = new List<long>[NodeCount];
            List<long>[] neighboursR = new List<long>[NodeCount];
            List<long>[] wR = new List<long>[NodeCount];
            for (int i = 0; i < NodeCount; i++)
            {
                neighbours[i] = new List<long>();
                neighboursR[i] = new List<long>();
                w[i] = new List<long>();
                wR[i] = new List<long>();
            }
            var count = edges.Length;
            for(int i = 0; i < count; i++)
            {
                neighbours[edges[i][0] - 1].Add(edges[i][1] - 1);
                neighboursR[edges[i][1] - 1].Add(edges[i][0] - 1);
                w[edges[i][0] - 1].Add(edges[i][2]);
                wR[edges[i][1] - 1].Add(edges[i][2]);

            }
            long[] results = new long[QueriesCount];
            for (int i = 0; i < QueriesCount; i++)
            {
                if (Queries[i][0] == Queries[i][1])
                    results[i] = 0;
                var result = BidirectionalDijkstra(NodeCount, Queries[i][0] - 1, Queries[i][1] - 1, neighbours, neighboursR,w,wR);
                results[i] = result != long.MaxValue ? result : -1;
            }

            return results;
        }

        private void Relax(long u, long v, long w, long[] distance)
        {
            if (distance[u] == long.MaxValue)
                return;
            var newdist = distance[u] + w;
            if (distance[v] > newdist)
            {
                distance[v] = newdist;
            }
        }


        public long FindMin(long[] dist, bool[] process, long nodes)
        {
            long minval = long.MaxValue;
            long minidx = 0;
            for (int i = 0; i < nodes; i++)
            {
                if (dist[i] < minval && !process[i])
                {
                    minval = dist[i];
                    minidx = i;
                }
            }

            return minidx;
        }

        public long BidirectionalDijkstra(long nodeCount, long start, long end, List<long>[]  neighbours, List<long>[] neighboursR, List<long>[] w, List<long>[] wR)
        {
            bool[] process = new bool[nodeCount];
            bool[] processR = new bool[nodeCount];
            long[] dist = new long[nodeCount];
            long[] distR = new long[nodeCount];
            HashSet<long> allproc = new HashSet<long>();
            for (int i = 0; i < nodeCount; i++)
            {
                dist[i] = long.MaxValue;
                distR[i] = long.MaxValue;
            }
            dist[start] = 0;
            distR[end] = 0;
            long nodes = nodeCount;
            while (nodes != 0)
            {
                nodes--;
                long v = FindMin(dist, process, nodeCount);
                process[v] = true;
                allproc.Add(v);
                long len = neighbours[v].Count;
                for(int i = 0; i < len; i++)
                {
                    Relax(v, neighbours[v][i], w[v][i], dist);
                }
                if (processR[v] == true)
                    return ShortestPath(nodeCount, dist, distR, allproc);
                long vR = FindMin(distR, processR, nodeCount);
                processR[vR] = true;
                allproc.Add(vR);
                len = neighboursR[vR].Count;
                for (int i = 0; i < len; i++)
                {
                    Relax(vR, neighboursR[vR][i], wR[vR][i], distR);
                }
                if (process[vR] == true)
                    return ShortestPath(nodeCount, dist, distR, allproc);
            }

            return long.MaxValue;
        }


        private long ShortestPath(long nodes, long[] dist, long[] distR, HashSet<long> process)
        {
            long distance = long.MaxValue;
            foreach (var v in process)
                if (dist[v] != long.MaxValue && distR[v] != long.MaxValue && dist[v] + distR[v] < distance)
                    distance = dist[v] + distR[v];

            return distance;
        }
    }
}