using System;
using System.Collections.Generic;

namespace FrequencyAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            var V = int.Parse(arr[0]);
            int E = int.Parse(arr[1]);
            long[,] mat = new long[E, 2];
            for (int i = 0; i < E; i++)
            {
                var arr1 = Console.ReadLine().Split(' ');
                mat[i, 0] = long.Parse(arr1[0]);
                mat[i, 1] = long.Parse(arr1[1]);
            }
            var result = Solve(V, E, mat);
            for (int i = 0; i < result.Length; i++)
                Console.WriteLine(result[i]);
        }

        public static String[] Solve(int V, int E, long[,] matrix)
        {
            int color = 3;
            List<string> cnf = new List<string>();
            cnf.Add(" ");
            long[] colors = new long[3];
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < color; j++)
                    colors[j] = ColorIdentifier(i, j);
                cnf.Add(string.Join(" ", colors) + " 0");

                for (int k = 0; k < color - 1; k++)
                    for (int h = k + 1; h < color; h++)
                        cnf.Add("-"+colors[k].ToString()+" -"+colors[h].ToString()+" 0");
            }

            for (int i = 0; i < E; i++)
            {
                for (int k = 0; k < color; k++)
                {
                    long firstcolor = ColorIdentifier(matrix[i, 0] - 1, k);
                    long secondcolor = ColorIdentifier(matrix[i, 1] - 1, k);
                    cnf.Add("-"+firstcolor.ToString()+" -"+secondcolor.ToString()+" 0");
                }
            }
            cnf[0]=(cnf.Count-1).ToString()+" "+(V * 3).ToString();
            //cnf.Reverse();
            return cnf.ToArray();


        }

        public static long ColorIdentifier(long i, long j, int color = 3) {

          return  i* color +j + 1; 
        }
    } }

