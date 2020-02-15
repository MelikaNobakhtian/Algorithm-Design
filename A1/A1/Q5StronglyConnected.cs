using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);
        Stack<long> ReversePostorder;
        public long Solve(long nodeCount, long[][] edges)
        {
            ReversePostorder = new Stack<long>();
            List<List<long>> Rgraph = new List<List<long>>();
            List<List<long>> graph = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
            {
                Rgraph.Add(new List<long>());
                graph.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0] - 1].Add(edges[i][1] - 1);
                Rgraph[(int)edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            return StronglyConnectedComponents(graph, Rgraph, nodeCount);
        }

        public long StronglyConnectedComponents(List<List<long>> graph, List<List<long>> Rgraph, long n)
        {
            DFs(graph, n);
            long result = 0;
            bool[] visit = new bool[n];
            var vertices = ReversePostorder.ToArray();
            foreach (var v in vertices)
            {
                if (!visit[v])
                {
                    Explore(Rgraph, visit, v);
                    result++;
                }
            }

            return result;
        }

        private void Explore(List<List<long>> graph, bool[] visit, long v)
        {
            visit[v] = true;
            foreach (var ver in graph[(int)v])
            {
                if (!visit[ver])
                    Explore(graph, visit, ver);
            }
        }

        public void DFs(List<List<long>> graph, long n)
        {
            bool[] Marked = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (!Marked[i])
                    TopologicalSort(graph, i, Marked);
            }
        }

        public void TopologicalSort(List<List<long>> graph, long v, bool[] Marked)
        {
            Marked[v] = true;
            foreach (var ver in graph[(int)v])
                if (!Marked[ver])
                    TopologicalSort(graph, ver, Marked);
            ReversePostorder.Push(v);


        }
    }
}
