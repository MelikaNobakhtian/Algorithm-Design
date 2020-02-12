using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            List<List<long>> graph = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0] - 1].Add(edges[i][1] - 1);
                graph[(int)edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            return DFS(StartNode - 1, EndNode - 1, graph, nodeCount);
        }

        long DFS(long start, long end, List<List<long>> graph, long n)
        {
            bool[] visit = new bool[n];
            return Explore(start, end, graph, visit);
        }

        private long Explore(long start, long end, List<List<long>> graph, bool[] visit)
        {
            if (start == end)
                return 1;
            visit[start] = true;
            foreach (var v in graph[(int)start])
            {
                long result = 0;
                if (!visit[v])
                    result = Explore(v, end, graph, visit);
                if (result == 1) return 1;
            }
            return 0;
        }
    }
}
