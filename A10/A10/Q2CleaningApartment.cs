using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<string> result = new List<string>();
            long[] posOfNode = new long[V];
            long[] oneNodeInPos = new long[V];
            bool[][] HasEdge = new bool[V][];
            result.Add(" ");
            for (int i = 0; i < V; i++)
            {
                HasEdge[i] = new bool[V];
                for (int j = 0; j < V; j++)
                {
                    posOfNode[j] = HamiltonPos(i, j, V);
                    oneNodeInPos[j] = HamiltonPos(j, i, V);
                }
                result.Add(string.Join(" ", oneNodeInPos) + " 0");
                result.Add(string.Join(" ", posOfNode) + " 0");
                for (int k = 0; k < V - 1; k++)
                    for (int h = k + 1; h < V; h++)
                    {
                        result.Add($"-{posOfNode[k]} -{posOfNode[h]} 0");
                        result.Add($"-{oneNodeInPos[k]} -{oneNodeInPos[h]} 0");
                    }

            }
            for (int i = 0; i < E; i++)
            {
                HasEdge[matrix[i, 0] - 1][matrix[i, 1] - 1] = true;
                HasEdge[matrix[i, 1] - 1][matrix[i, 0] - 1] = true;
            }

            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                {
                    if (HasEdge[i][j]) continue;
                    for (int k = 0; k < V - 1; k++)
                    {
                        result.Add($"-{HamiltonPos(i, k, V)} -{HamiltonPos(j, k + 1, V)} 0");
                        result.Add($"-{HamiltonPos(j, k, V)} -{HamiltonPos(i, k + 1, V)} 0");
                    }
                }
            result[0] = $"{result.Count - 1} {V * V}";
            return result.ToArray();
        }

        public static long HamiltonPos(int i, int j, int v) => i * v + j + 1;

    }
}
