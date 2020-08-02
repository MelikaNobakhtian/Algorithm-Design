using System;
using System.Collections.Generic;

namespace dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine().Split(' ');
            long n = long.Parse(arr[0]);
            long m = long.Parse(arr[1]);
            List < List<long> > graph = new List<List<long>>();
            List<List<long>> w = new List<List<long>>();
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<long>());
                w.Add(new List<long>());
            }
            for (int i = 0; i < m; i++)
            {
                var arr1 = Console.ReadLine().Split(' ');
                var u = long.Parse(arr1[0]);
                var v = long.Parse(arr1[1]);
                graph[(int)u - 1].Add(v - 1);
                w[(int)u - 1].Add(long.Parse(arr1[2]));
            }
            var arr2 = Console.ReadLine().Split(' ');
            Console.WriteLine(Solve(n, graph, w, long.Parse(arr2[0]), long.Parse(arr2[1])));
        }

        public static long Solve(long nodeCount, List<List<long>> graph, List<List<long>> w, long startNode, long endNode)
        {
            var result = Dijkstra(graph,w, startNode - 1, endNode - 1, nodeCount);
            return result[endNode - 1] != long.MaxValue ? result[endNode - 1] : -1;
        }

        public static long[] Dijkstra(List<List<long>> graph, List<List<long>> w, long start, long end, long nodecount)
        {
            long[] dist = new long[nodecount];
            List<long> idx = new List<long>();
            List<long> priorityqueue = new List<long>();
            for (int i = 0; i < nodecount; i++)
            {
                priorityqueue.Add(long.MaxValue);
                idx.Add(i);
                dist[i] = long.MaxValue;
            }
            dist[start] = 0;
            priorityqueue[0] = 0;
            idx[0] = start;
            idx[(int)start] = 0;
            while (idx.Count != 0)
            {
                var v = idx[0];
                if (v == end) return dist;
                var index = idx.IndexOf(v);
                idx.Remove(v);
                priorityqueue.RemoveAt(index);
                BuildHeap(priorityqueue, idx);
                for(int i = 0; i < graph[(int)v].Count; i++)
                {
                    var u = graph[(int)v][i];
                    var newdist = dist[v] + w[(int)v][i];
                    if(u!=start && newdist<dist[u] && newdist > 0)
                    {
                        dist[u] = newdist;
                        var j = idx.IndexOf(u);
                        ChangePriority(j, newdist, priorityqueue, idx);
                        if (u == end && !idx.Contains(u)) return dist;
                    }
                }
              
                BuildHeap(priorityqueue, idx);
            }

            return dist;
        }

        public static void ChangePriority(int i, long p, List<long> heap, List<long> idx)
        {
            var oldvalue = heap[i];
            heap[i] = p;
            if (p > oldvalue)
                SiftDown(i, heap, idx);
            else
                SiftUp(i, heap, idx);
        }
        
        public static void Swap(List<long> list,int i,int j)
        {
            var tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }

        public static void SiftUp(int i, List<long> heap, List<long> idx)
        {
            while (i > 0 && heap[(i - 1) / 2] > heap[i])
            {
                Swap(heap, i, (i - 1) / 2);
                Swap(idx, i, (i - 1) / 2);
            }
        }

        public static void BuildHeap(List<long> array, List<long> idx)
        {
            int last = array.Count / 2;
            for (int i = last; i >= 0; i--)
                SiftDown(i, array, idx);
        }

        public static void SiftDown(int i, List<long> heap, List<long> idx)
        {
            int minindex = i;
            int leftchild;
            int rightchild;
            if (2 * i + 1 < heap.Count)
            {
                leftchild = 2 * i + 1;
                if (heap[leftchild] < heap[minindex])
                    minindex = leftchild;
            }
            if (2 * i + 2 < heap.Count)
            {
                rightchild = 2 * i + 2;
                if (heap[minindex] > heap[rightchild])
                    minindex = rightchild;
            }
            if (i != minindex)
            {
                Swap(heap, i, minindex);
                Swap(idx,i, minindex);
                SiftDown(minindex, heap, idx);
            }

        }
    }
}
