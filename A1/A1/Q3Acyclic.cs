using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

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
            }

            return HasCycle(graph, nodeCount);
        }

        public long HasCycle(List<List<long>> graph, long n)
        {
            bool[] visit = new bool[n];
            bool[] inStack = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (DFs(i, graph, visit, inStack))
                    return 1;
            }
            return 0;
        }

        private bool DFs(int i, List<List<long>> graph, bool[] visit, bool[] inStack)
        {
            if (!visit[i])
            {
                visit[i] = true;
                inStack[i] = true;
                foreach (var v in graph[i])
                {
                    if (!visit[v] && DFs((int)v, graph, visit, inStack))
                        return true;
                    else if (inStack[v])
                        return true;
                }
            }
            inStack[i] = false;
            return false;
        }
    }
}