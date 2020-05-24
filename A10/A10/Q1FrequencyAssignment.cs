using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            int color = 3;
            List<string> cnf = new List<string>();
            long[] colors = new long[3];
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < color; j++)
                    colors[j] = ColorIdentifier(i, j);
                cnf.Add(string.Join(" ", colors) + " 0");

                for (int k = 0; k < color - 1; k++)
                    for (int h = k + 1; h < color; h++)
                        cnf.Add($"-{colors[k]} -{colors[h]} 0");
            }

            for (int i = 0; i < E; i++)
            {
                for (int k = 0; k < color; k++)
                {
                    long firstcolor = ColorIdentifier(matrix[i, 0] - 1, k);
                    long secondcolor = ColorIdentifier(matrix[i, 1] - 1, k);
                    cnf.Add($"-{firstcolor} -{secondcolor} 0");
                }
            }
            cnf.Add($"{cnf.Count} {V * 3}");
            cnf.Reverse();
            return cnf.ToArray();


        }

        public static long ColorIdentifier(long i, long j, int color = 3) => i * color + j + 1;

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}
