using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long NodeCount, long[][] edges)
        {
            long[] colors = new long[NodeCount];
            bool[] visit = new bool[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                colors[i] = long.MaxValue;
            colors[0] = 0;
            visit[0] = true;
            Queue<long> nodes = new Queue<long>();
            nodes.Enqueue(0);
            while (nodes.Count != 0)
            {
                var v = nodes.Dequeue();
                for(int i = 0; i < edges.Length; i++)
                {
                    if (edges[i][0] == v + 1)
                    {
                        if (colors[edges[i][1] - 1] == long.MaxValue)
                        {
                            FindColor(colors, v, edges[i][1] - 1);
                            nodes.Enqueue(edges[i][1] - 1);
                        }
                        else if (colors[v] == colors[edges[i][1] - 1])
                            return 0;
                            
                    }

                    if (edges[i][1] == v + 1)
                    {
                        if (colors[edges[i][0] - 1] == long.MaxValue)
                        {
                            FindColor(colors, v, edges[i][0] - 1);
                            nodes.Enqueue(edges[i][0] - 1);
                        }
                        else if (colors[v] == colors[edges[i][0] - 1])
                            return 0;

                    }
                }
            }

            return 1;
           
        }

        private void FindColor(long[] colors, long v1, long v2)
        {
            if (colors[v1] == 0)
                colors[v2] = 1;
            else
                colors[v2] = 0;
        }
    }
}
