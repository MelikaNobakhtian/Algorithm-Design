using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;
using Priority_Queue;

namespace Exam1
{
    public class Q2Outbreak : Processor
    {
        public Q2Outbreak(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);

        public static Tuple<int, int, int[,], int[,]> ProcessQ2(string[] data)
        {
            var temp = data[0].Split();
            int N = int.Parse(temp[0]);
            int M = int.Parse(temp[1]);
            int[,] carriers = new int[N, 2];
            int[,] safe = new int[M, 2];
            for (int i = 0; i < N; i++)
            {
                carriers[i, 0] = int.Parse(data[i + 1].Split()[0]);
                carriers[i, 1] = int.Parse(data[i + 1].Split()[1]);
            }

            for (int i = 0; i < M; i++)
            {
                safe[i, 0] = int.Parse(data[i + N + 1].Split()[0]);
                safe[i, 1] = int.Parse(data[i + N + 1].Split()[1]);
            }
            return Tuple.Create(N, M, carriers, safe);
        }
        public string Solve(string[] input)
        {
            var data = ProcessQ2(input);
            return Solve(data.Item1,data.Item2,data.Item3,data.Item4).ToString();
        }
        public double Solve(int N, int M, int[,] carrier, int[,] safe)
        {
            long[,] hh = new long[2, 4];
            var f = hh.Length;
            double max = double.MaxValue;
            double maxofall = 0;
            //double[] maxes = new double[M];
            for (int i = 0; i < N; i++)
            {
                max =double.MaxValue;
                for(int j = 0; j < M; j++)
                {
                    double n = RealDist(carrier[i, 0], carrier[i, 1], safe[j, 0], safe[j, 1]);
                    if (n < max)
                        max = n;
                }
                if (maxofall < max)
                    maxofall = max;
                
            }
            return Math.Round(maxofall, 6);
            //long[][] points = new long[M + N][];
            //for(int i = 0; i < N; i++)
            //{
            //    points[i] = new long[2];
            //    points[i][0] = carrier[i, 0];
            //    points[i][1] = carrier[i, 1];

            //}
            //for(int i = 0; i < M; i++)
            //{
            //    points[N  + i] = new long[2];
            //    points[N + i][0] = safe[i, 0];
            //    points[N + i][1] = safe[i, 1];
            //}

            //return Math.Round(Prim(M + N, points), 6);

        }

        public double RealDist(long x1, long y1, long x2, long y2) => Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

        public double Prim(long pointCount, long[][] points)
        {
            double path = 0;
            double[] cost = new double[pointCount];
            SimplePriorityQueue<long, double> priorityQ = new SimplePriorityQueue<long, double>();
            for (int I = 0; I < pointCount; I++)
            {
                cost[I] = int.MaxValue;
                priorityQ.Enqueue(I, int.MaxValue);
            }
            cost[0] = 0;
            long v = 0;
            priorityQ.UpdatePriority(0, 0);
            while (priorityQ.Count != 0)
            {
                v = priorityQ.Dequeue();
                if (cost[v] > path)
                    path = cost[v];
                for (int i = 0; i < pointCount; i++)
                {
                    if (i == v || !priorityQ.Contains(i))
                        continue;
                    double dist = RealDist(points[v][0], points[v][1], points[i][0], points[i][1]);
                    if (cost[i] > dist)
                    {
                        priorityQ.UpdatePriority(i, dist);
                        cost[i] = dist;
                    }
                }
                
            }

            return path;
        }
    }
}
