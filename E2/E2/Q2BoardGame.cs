using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q2BoardGame : Processor
    {
        public Q2BoardGame(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, long[,], string[]>)Solve);

        public string[] Solve(int BoardSize, long[,] Board)
        {
            int none = 1;
            int red = 2;
            int blue = 3;
            List<string> results = new List<string>();
            List<long> inorout = new List<long>();
            results.Add(" ");
            long[] rowred = new long[BoardSize];
            long[] rownone = new long[BoardSize];
            long[] colnone = new long[BoardSize];
            long[] colred = new long[BoardSize];
            long[] rowblue = new long[BoardSize];
            long[] colblue = new long[BoardSize];
            int k = 0;
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    if (Board[i, j] == 2)
                    {
                        var cl = Identifier(i, j, blue, BoardSize);
                        var no = Identifier(i, j, none, BoardSize);
                        var r = Identifier(i, j, red, BoardSize);
                        results.Add($"{cl} {no} 0");
                        results.Add($"-{r} 0");
                        inorout.Add(Identifier(i, j, blue, BoardSize));
                    }
                    else if (Board[i, j] == 3)
                    {
                        inorout.Add(Identifier(i, j, red, BoardSize));
                        var cl = Identifier(i, j, blue, BoardSize);
                        var no = Identifier(i, j, none, BoardSize);
                        var r = Identifier(i, j, red, BoardSize);
                        results.Add($"{r} {no} 0");
                        results.Add($"-{cl} 0");
                    }
                    else
                    {
                        results.Add($"{Identifier(i, j, none, BoardSize)} 0");
                        results.Add($"-{Identifier(i, j, blue, BoardSize)} 0");
                        results.Add($"-{Identifier(i, j, red, BoardSize)} 0");

                    }
                    rowred[k] = Identifier(i, j, red, BoardSize);
                    colred[k] = Identifier(j, i, red, BoardSize);
                    rownone[k] = Identifier(i, j, none, BoardSize);
                    colnone[k] = Identifier(j, i, none,BoardSize);
                    rowblue[k] = Identifier(i, j, blue, BoardSize);
                    colblue[k] = Identifier(j, i, blue, BoardSize);
                    results.Add($"-{rowred[k]} -{rowblue[k]} 0");
                    results.Add($"-{rowred[k]} -{rownone[k]} 0");
                    results.Add($"-{rownone[k]} -{rowblue[k]} 0");
                    results.Add($"-{colred[k]} -{colblue[k]} 0");
                    results.Add($"-{colred[k]} -{colnone[k]} 0");
                    results.Add($"-{colnone[k]} -{colblue[k]} 0");
                    results.Add($"{rownone[k]} {rowred[k]} {rowblue[k]} 0");
                    results.Add($"{colnone[k]} {colred[k]} {colblue[k]} 0");
                    k++;
                }
                k = 0;
                results.Add(string.Join(' ', rowred) +" "+string.Join(' ',rowblue)+" 0");
                results.Add(string.Join(' ', colred) +" "+string.Join(' ',colblue)+ " 0");
                for(int t = 0; t < BoardSize; t++)
                {
                    for (int f = 0; f < BoardSize; f++)
                        results.Add($"-{colred[t]} -{colblue[f]} 0");
                }
               
            }
            //results.Add(string.Join(' ', inorout) + " 0");
            results[0] = $"{results.Count-1} {BoardSize * BoardSize * 3}";
            return results.ToArray();
        }

        public long Identifier(int i, int j, int color, int n) => n * 3 * i + j * 3 + color;
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
