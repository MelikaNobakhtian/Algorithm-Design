using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
    public class Q3SchoolBus : Processor
    {
        public Q3SchoolBus(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long[][], Tuple<long, long[]>>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.TSPVerifier;

        public virtual Tuple<long, long[]> Solve(long nodeCount, long[][] edges)
        {
            // Key:integer of binary number of subset,destination
            // Value: Cost , Previous Node in Path
            Dictionary<Tuple<int, long>, Tuple<long, long>> AllCost = new Dictionary<Tuple<int, long>, Tuple<long, long>>();
            int[] nodenumber = new int[nodeCount - 1];
            long[][] adjmat = new long[nodeCount][];
            for (int i = 0; i < nodeCount; i++)
            {
                if (i > 0)
                    nodenumber[i - 1] = i;
                adjmat[i] = new long[nodeCount];
                for (int j = 0; j < nodeCount; j++)
                    adjmat[i][j] = 10000;
                adjmat[i][i] = 0;
            }
            // Build adjancy matrix
            for (int i = 0,n= edges.Length; i < n; i++)
            {
                long u = edges[i][0] - 1;
                long v = edges[i][1] - 1;
                adjmat[u][v] = adjmat[v][u] = edges[i][2];
            }
            //set initial values
            for (int i = 1; i < nodeCount; i++)
            {
                AllCost.Add(new Tuple<int, long>(1 + (1 << i), i), new Tuple<long, long>(adjmat[0][i], 0));
            }
            //find cost of subsets
            for (int i = 2; i < nodeCount; i++)
            {
                List<int[]> SubsetK = new List<int[]>();
                SubsetofSizeM(SubsetK, nodenumber, i, nodeCount - 1, new int[i], 0, 0);
                for (int j = 0,n= SubsetK.Count; j < n; j++)
                {
                    int subidentify = 1;
                    for (int k = 0,c= SubsetK[j].Length; k < c; k++)
                        subidentify += 1 << SubsetK[j][k];
                    for (int k = 0,len= SubsetK[j].Length; k < len; k++)
                    {
                        int previous = subidentify ^ (1 << SubsetK[j][k]);
                        long mincost = long.MaxValue;
                        long pre = -1;
                        var newkey = new Tuple<int, long>(subidentify, SubsetK[j][k]);
                        AllCost.Add(newkey, new Tuple<long, long>(0, 0));
                        for (int h = 0,c= SubsetK[j].Length; h <c ; h++)
                        {
                            if (h == k)
                                continue;
                            var Key = new Tuple<int, long>(previous, SubsetK[j][h]);
                            if (AllCost[Key].Item1 + adjmat[SubsetK[j][h]][SubsetK[j][k]] < mincost)
                            {
                                pre = SubsetK[j][h];
                                mincost = AllCost[Key].Item1 + adjmat[SubsetK[j][h]][SubsetK[j][k]];
                            }
                        }
                        AllCost[newkey] = new Tuple<long, long>(mincost, pre);
                    }
                }


            }
            int MainSet = (int)Math.Pow(2, nodeCount) - 1;
            long min = long.MaxValue, prev = -1;
            Tuple<int, long> present;
            for (int i = 1; i < nodeCount; i++)
            {
                present = new Tuple<int, long>(MainSet, i);
                if (AllCost[present].Item1 + adjmat[0][i] < min)
                {
                    min = AllCost[present].Item1 + adjmat[0][i];
                    prev = i;
                }
            }
            if (min >= 10000)
                return new Tuple<long, long[]>(-1, new long[nodeCount]);
            long[] cycle = new long[nodeCount];
            cycle[0] = 1;
            for (int i = (int)nodeCount - 1; i > 0; i--)
            {
                cycle[i] = prev + 1;
                present = new Tuple<int, long>(MainSet, prev);
                MainSet = MainSet ^ (1 << (int)prev);
                prev = AllCost[present].Item2;
            }

            return new Tuple<long, long[]>(min, cycle);

        }

        public void SubsetofSizeM(List<int[]> Allsubsets, int[] set, long m, long size, int[] subset, long indexofset, long indexofsubset)
        {
            if (indexofsubset == m)
            {
                int[] sub = new int[m];
                for (int i = 0; i < m; i++)
                    sub[i] = subset[i];
                Allsubsets.Add(sub);
                return;
            }
            if (indexofset >= size)
                return;
            subset[indexofsubset] = set[indexofset];
            SubsetofSizeM(Allsubsets, set, m, size, subset, indexofset + 1, indexofsubset + 1);
            SubsetofSizeM(Allsubsets, set, m, size, subset, indexofset + 1, indexofsubset);
        }
    }
}
