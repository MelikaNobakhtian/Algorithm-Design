using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
    public class Q2FunParty : Processor
    {
        public Q2FunParty(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long[], long[][], long>)Solve);
        public long[] maxfun;

        public virtual long Solve(long n, long[] funFactors, long[][] hierarchy)
        {
            maxfun = new long[n];
            Dictionary<long, List<long>> graph = new Dictionary<long, List<long>>();
            for (int i = 0; i < n; i++)
            {
                maxfun[i] = long.MaxValue;
                graph.Add(i, new List<long>());
            }
            for (int i = 0; i < hierarchy.Length; i++)
            {
                long u = hierarchy[i][0] - 1;
                long v = hierarchy[i][1] - 1;
                graph[u].Add(v);
                graph[v].Add(u);
            }
            return FunParty(0, graph, funFactors, long.MinValue);

        }

        public long FunParty(long v, Dictionary<long, List<long>> graph, long[] funfactors, long parent)
        {
            if (maxfun[v] == long.MaxValue)
            {
                if (graph[v].Count == 1 && graph[v][0] == parent)
                    maxfun[v] = funfactors[v];
                else
                {
                    long max1 = funfactors[v];
                    foreach (var child in graph[v])
                        if (child != parent)
                            foreach (var gchild in graph[child])
                                if (gchild != v)
                                    max1 += FunParty(gchild, graph, funfactors, child);
                    long max0 = 0;
                    foreach (var child in graph[v])
                        if (child != parent)
                            max0 += FunParty(child, graph, funfactors, v);
                    maxfun[v] = Math.Max(max1, max0);
                }
            }
            return maxfun[v];
        }
    }
}
