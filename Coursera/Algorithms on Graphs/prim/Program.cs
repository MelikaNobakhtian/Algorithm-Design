using System;

namespace prim
{
    class Program
    {

        public static long[] rank;
        public static long[] parent;
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[][] points = new long[n][];
            for(int i = 0; i < n; i++)
            {
                var arr = Console.ReadLine().Split(' ');
                long[] point = new long[2];
                point[0] = long.Parse(arr[0]);
                point[1] = long.Parse(arr[1]);
                points[i] = point;
            }
            long cluster = long.Parse(Console.ReadLine());
            Console.WriteLine(Solve(n, points, cluster));
        }

        public static double Solve(long pointCount, long[][] points, long clusterCount)
        {

            parent = new long[pointCount];
            rank = new long[pointCount];
            long edgeCount = pointCount * (pointCount - 1) / 2;
            double[] edgeweight = new double[edgeCount];
            long[][] edges = new long[edgeCount][];
            long k = 0;
            for (int i = 0; i < pointCount; i++)
            {
                parent[i] = i;
                rank[i] = 0;
                for (int j = i + 1; j < pointCount; j++)
                {
                    double dist = RealDist(points[i][0], points[i][1], points[j][0], points[j][1]);
                    edgeweight[k] = dist;
                    edges[k] = new long[] { i, j };
                    k++;
                }
            }
            Array.Sort(edgeweight, edges);
            return Math.Round(Kruskal(pointCount, clusterCount, edgeweight, edges), 6);
        }

        public static double RealDist(long x1, long y1, long x2, long y2)
        {
           return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static double Kruskal(long pointCount, long clusters, double[] weights, long[][] edges)
        {
            long count = weights.Length;
            for (int i = 0; i < count; i++)
            {
                if (Find(edges[i][0]) != Find(edges[i][1]))
                {
                    if (pointCount == clusters)
                        return weights[i];
                    Union(edges[i][0], edges[i][1]);
                    pointCount--;
                }
            }

            return 0;
        }

        public static long Find(long i)
        {
            if (i != parent[i])
                parent[i] = Find(parent[i]);
            return parent[i];
        }

        public static void Union(long i, long j)
        {
            var i_id = Find(i);
            var j_id = Find(j);
            if (i_id == j_id)
                return;
            if (rank[i_id] > rank[j_id])
            {
                parent[j_id] = i_id;
            }
            else
            {
                parent[i_id] = j_id;
            }

            if (rank[i_id] == rank[j_id])
                rank[j_id] = rank[i_id] + 1;
        }
    }
}
