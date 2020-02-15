using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
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

            return newDfs(graph, nodeCount);
        }

        public long newDfs(List<List<long>> graph, long n)
        {
            bool[] visit = new bool[n];
            long connectedcomp = 0;
            Stack<long> nodes = new Stack<long>();
            for (int i = 0; i < n; i++)
            {
                if (!visit[i])
                {
                    nodes.Push(i);
                    connectedcomp++;
                    while (nodes.Count != 0)
                    {
                        var node = nodes.Pop();
                        if (!visit[node])
                            visit[node] = true;
                        foreach (var v in graph[(int)node])
                        {
                            if (!visit[v])
                                nodes.Push(v);
                        }
                    }
                }
            }

            return connectedcomp;

        }
    }
}
