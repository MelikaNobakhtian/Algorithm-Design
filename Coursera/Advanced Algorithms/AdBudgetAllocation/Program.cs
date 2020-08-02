using System;
using System.Collections.Generic;

namespace AdBudgetAllocation
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long equ = long.Parse(arr[0]);
            long var = long.Parse(arr[1]);
            long[][] equa = new long[equ][];
            for(int i = 0; i < equ; i++)
            {
                var arr1 = Console.ReadLine().Split(' ');
                equa[i] = new long[var];
                for (int j = 0; j < var; j++)
                    equa[i][j] = long.Parse(arr1[j]);
            }
            var arr3 = Console.ReadLine().Split(' ');
            long[] b = new long[equ];
            for (int i = 0; i < equ; i++)
                b[i] = long.Parse(arr3[i]);
            var r = Solve(equ, var, equa, b);
            
            for (int i = 0; i < r.Length; i++)
                Console.WriteLine(r[i]);
        }

        public static string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
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
                        string str = null;
                        for (int f = 0; f < notans.Count; f++)
                            str += notans[f].ToString() + " ";
                        cnf.Add(str+"0");
                    }

                }
            }
            if (cnf.Count == 1)
                cnf.Add("1 -1 0");
            cnf[0] = (cnf.Count - 1).ToString()+" "+varCount.ToString();
            return cnf.ToArray();
        }
    }
}
