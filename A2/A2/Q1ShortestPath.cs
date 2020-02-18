using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        
        public long Solve(long NodeCount, long[][] edges, 
                          long StartNode,  long EndNode)
        {
            long[] dist = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                dist[i] = long.MaxValue;
            dist[StartNode-1] = 0;
            Queue<long> vertices = new Queue<long>();
            vertices.Enqueue(StartNode);
            while (vertices.Count != 0)
            {
                var u = vertices.Dequeue();
                for(int i = 0; i < edges.Length; i++)
                {
                    if(edges[i][0]==u)
                    {
                        var v = edges[i][1] - 1;
                        if (dist[v] == long.MaxValue)
                        {
                            vertices.Enqueue(v+1);
                            dist[v] = dist[u-1] + 1;
                        }
                    }

                    if (edges[i][1] == u)
                    {
                        var v = edges[i][0] - 1;
                        if (dist[v] == long.MaxValue)
                        {
                            vertices.Enqueue(v + 1);
                            dist[v] = dist[u - 1] + 1;
                        }
                    }
                }
               
            }

            return dist[EndNode - 1] != long.MaxValue ? dist[EndNode - 1] : -1;
        }
    }
}
