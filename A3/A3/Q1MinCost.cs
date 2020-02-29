using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            var result = Dijkstra(edges, startNode - 1, endNode - 1, nodeCount);
            return result[endNode - 1]!=long.MaxValue ? result[endNode-1] : -1;
        }

        public long[] Dijkstra(long[][] edges, long start, long end, long nodecount)
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
                for (int i = 0; i < edges.Length; i++)
                {
                    if (edges[i][0] - 1 == v)
                    {
                        var u = edges[i][1] - 1;
                        var newdist = dist[v] + edges[i][2];
                        if (edges[i][1] - 1 != start && newdist < dist[u] && newdist > 0)
                        {
                            dist[u] = newdist;
                            var j = idx.IndexOf(u);
                            ChangePriority(j, newdist, priorityqueue, idx);
                            if (u == end && !idx.Contains(u)) return dist;
                        }
                    }


                }
                BuildHeap(priorityqueue, idx);
            }

            return dist;
        }

        public void ChangePriority(int i, long p, List<long> heap, List<long> idx)
        {
            var oldvalue = heap[i];
            heap[i] = p;
            if (p > oldvalue)
                SiftDown(i, heap, idx);
            else
                SiftUp(i, heap, idx);
        }

        public void SiftUp(int i, List<long> heap, List<long> idx)
        {
            while (i > 0 && heap[(i - 1) / 2] > heap[i])
            {
                (heap[i], heap[(i - 1) / 2]) = (heap[(i - 1) / 2], heap[i]);
                (idx[i], idx[(i - 1) / 2]) = (idx[(i - 1) / 2], idx[i]);
            }
        }

        public void BuildHeap(List<long> array, List<long> idx)
        {
            int last = array.Count / 2;
            for (int i = last; i >= 0; i--)
                SiftDown(i, array, idx);
        }

        public void SiftDown(int i, List<long> heap, List<long> idx)
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
                (heap[i], heap[minindex]) = (heap[minindex], heap[i]);
                (idx[i], idx[minindex]) = (idx[minindex], idx[i]);
                SiftDown(minindex, heap, idx);
            }

        }
    }
}
