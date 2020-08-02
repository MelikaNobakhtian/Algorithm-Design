using System;

namespace merging_table
{
    class Program
    {
        public static long[] parent;
        public static long[] TableSizes;
        public static long[] rank;

        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            long[] tablesizes = new long[n];
            var arr1 = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; i++)
                tablesizes[i] = long.Parse(arr1[i]);
            long[] source = new long[m];
            long[] target = new long[m];
            for(int i = 0; i < m; i++)
            {
                var arr2 = Console.ReadLine().Split(' ');
                source[i] = long.Parse(arr2[0]);
                target[i] = long.Parse(arr2[1]);
            }
            var result = Solve(tablesizes, target, source);
            foreach (var r in result)
                Console.WriteLine(r);
        }

        public static long[] Solve(long[] tableSizes, long[] targetTables, long[] sourceTables)
        {
            long[] results = new long[targetTables.Length];
            parent = new long[tableSizes.Length];
            rank = new long[tableSizes.Length];
            TableSizes = tableSizes;
            long maxsize = 0;
            for (int i = 0; i < tableSizes.Length; i++)
            {
                parent[i] = i;
                if (TableSizes[i] > maxsize)
                    maxsize = TableSizes[i];
            }
            for (int i = 0; i < targetTables.Length; i++)
            {
                if (targetTables[i] == sourceTables[i])
                {
                    results[i] = maxsize;
                    continue;
                }

                var idx = Union(sourceTables[i], targetTables[i]);
                if (TableSizes[idx] > maxsize)
                {
                    results[i] = TableSizes[idx];
                    maxsize = TableSizes[idx];
                }
                else
                    results[i] = maxsize;

            }

            return results;
        }


        public static long Find(long i)
        {
            if (i != parent[i])
                parent[i] = Find(parent[i]);
            return parent[i];
        }

        public static long Union(long i, long j)
        {
            long presentparent = 0;
            var i_id = Find(i - 1);
            var j_id = Find(j - 1);
            if (i_id == j_id)
                return i_id;
            if (rank[i_id] > rank[j_id])
            {
                presentparent = i_id;
                parent[j_id] = i_id;
                TableSizes[i_id] = TableSizes[i_id] + TableSizes[j_id];
                TableSizes[j_id] = 0;
            }
            else
            {
                presentparent = j_id;
                parent[i_id] = j_id;
                TableSizes[j_id] = TableSizes[i_id] + TableSizes[j_id];
                TableSizes[i_id] = 0;
            }

            if (rank[i_id] == rank[j_id])
                rank[j_id] = rank[i_id] + 1;

            return presentparent;
        }

    }
}
