using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using Priority_Queue;

namespace A4
{
    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);

        public double Solve(long pointCount, long[][] points)
        {
            return Math.Round(Prim(pointCount, points),6);
        }

        public double Prim(long pointCount,long[][] points)
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
            priorityQ.UpdatePriority(0, 0);
            while (priorityQ.Count != 0)
            {
                long v = priorityQ.Dequeue();
                path += cost[v];
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

        public double RealDist(long x1, long y1, long x2, long y2) => Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }
}
