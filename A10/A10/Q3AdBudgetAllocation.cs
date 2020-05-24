using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q3AdBudgetAllocation : Processor
    {
        public Q3AdBudgetAllocation(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            List<string> cnf = new List<string>();
            string subset = null;
            cnf.Add(" ");
            int nonzero, idx;
            long sum;
            for (int i = 0; i < eqCount; i++)
            {
                nonzero = 0;
                for (int j = 0; j < varCount; j++)
                    if (A[i][j] != 0)
                        nonzero++;
                int numberofsubsets = (int)Math.Pow(nonzero, 2);
                if (numberofsubsets == 1)
                    numberofsubsets++;
                for (int k = 0; k < numberofsubsets; k++)
                {
                    sum = 0;
                    idx = 3;
                    subset = Convert.ToString(k, 2).PadLeft(3, '0');
                    for (int h = 0; h < varCount; h++)
                        if (A[i][h] != 0)
                        {
                            idx--;
                            if (subset[idx] == '1')
                                sum += A[i][h];
                        }
                    if (sum > b[i])
                    {
                        idx = 2;
                        List<int> notans = new List<int>();
                        for (int h = 0; h < varCount; h++)
                        {
                            if (A[i][h] != 0)
                            {
                                int var = subset[idx] == '0' ? h + 1 : (h + 1) * -1;
                                notans.Add(var);
                                idx--;
                            }
                        }
                        notans.Add(0);
                        cnf.Add(string.Join(' ', notans));
                    }

                }
            }

            cnf[0] = $"{cnf.Count - 1} {varCount}";
            return cnf.ToArray();
        }
    }
}
