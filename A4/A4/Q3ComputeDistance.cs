using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using GeoCoordinatePortable;
using Priority_Queue;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };
        public static readonly char[] NewLineChars = new char[] { '\n', '\r' };
        private static double[][] ReadTree(IEnumerable<string> lines)
        {
            return lines.Select(line =>
                line.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(n => double.Parse(n)).ToArray()
                            ).ToArray();
        }
        public override string Process(string inStr)
        {
            return Process(inStr, (Func<long, long, double[][], double[][], long,
                                    long[][], double[]>)Solve);
        }
        public static string Process(string inStr, Func<long, long, double[][]
                                  , double[][], long, long[][], double[]> processor)
        {
            var lines = inStr.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
            long[] count = lines.First().Split(IgnoreChars,
                                               StringSplitOptions.RemoveEmptyEntries)
                                         .Select(n => long.Parse(n))
                                         .ToArray();
            double[][] points = ReadTree(lines.Skip(1).Take((int)count[0]));
            double[][] edges = ReadTree(lines.Skip(1 + (int)count[0]).Take((int)count[1]));
            long queryCount = long.Parse(lines.Skip(1 + (int)count[0] + (int)count[1])
                                         .Take(1).FirstOrDefault());
            long[][] queries = ReadTree(lines.Skip(2 + (int)count[0] + (int)count[1]))
                                        .Select(x => x.Select(z => (long)z).ToArray())
                                        .ToArray();

            return string.Join("\n", processor(count[0], count[1], points, edges,
                                queryCount, queries));
        }
        public double[] Solve(long nodeCount,
                            long edgeCount,
                            double[][] points,
                            double[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            //this.ExcludeTestCaseRangeInclusive(11, 13);
            List<double>[] neighbours = new List<double>[nodeCount];
            List<double>[] w = new List<double>[nodeCount];
            double[] results = new double[queriesCount];
            for (int i = 0; i < nodeCount; i++)
            {
                neighbours[i] = new List<double>();
                w[i] = new List<double>();
            }
            for (int i = 0; i < edgeCount; i++)
            {
                neighbours[(int)edges[i][0] - 1].Add(edges[i][1] - 1);
                w[(int)edges[i][0] - 1].Add(edges[i][2]);
            }
            for (int i = 0; i < queriesCount; i++)
            {
                double result = Astar(w, neighbours, nodeCount, queries[i][0] - 1, queries[i][1] - 1, points);
                results[i] = result != int.MaxValue ? result : -1;
            }

            return results;
        }

        public double RealDist(double x1, double y1, double x2, double y2, bool geo)
        {
            if (!geo)
                return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            else
            {
                GeoCoordinate pin1 = new GeoCoordinate(x1, y1);
                GeoCoordinate pin2 = new GeoCoordinate(x2, y2);
                return pin1.GetDistanceTo(pin2);
            }
        }
        public double Astar(List<double>[] w, List<double>[] neighbours, long nodeCount, long start, long end, double[][] points)
        {
            double[] dist = new double[nodeCount];
            //double[] cost = new double[nodeCount];
            //bool[] process = new bool[nodeCount];
            //Dictionary<long, double> cost = new Dictionary<long, double>();
            SimplePriorityQueue<long, double> cost = new SimplePriorityQueue<long, double>();
            bool geo = false;
            //if (points[start][0] - (int)points[start][0] > 0)
            //    geo = true;
            for (int i = 0; i < nodeCount; i++)
            {
                cost.Enqueue(i, int.MaxValue);
                dist[i] = int.MaxValue;
                // cost[i] = int.MaxValue;
            }
            long n = nodeCount;
            dist[start] = 0;
            cost.UpdatePriority(start, RealDist(points[start][0], points[start][1], points[end][0], points[end][1], geo));
            //cost[start] = RealDist(points[start][0], points[start][1], points[end][0], points[end][1], geo);
            //cost[start] = RealDist(points[start][0], points[start][1], points[end][0], points[end][1],geo);
            while (n != 0)
            {
                //long v = FindMin(cost,nodeCount,process);
                //var value = cost.Min(x=>x.Value);
                long v = cost.Dequeue();
                //cost.Remove(v);
                // process[v] = true;
                n--;
                long count = neighbours[v].Count;
                if (v == end)
                    return dist[v];
                for (int i = 0; i < count; i++)
                {
                    if (cost.Contains((int)neighbours[v][i]) && dist[(int)neighbours[v][i]] > dist[v] + w[v][i])
                    {
                        long u = (long)neighbours[v][i];
                        dist[u] = dist[v] + w[v][i];
                        double realdist = RealDist(points[u][0], points[u][1], points[end][0], points[end][1], geo);
                        cost.UpdatePriority(u, dist[u] + realdist);
                    }
                }
            }

            return dist[end];
        }

    

        }

    }
