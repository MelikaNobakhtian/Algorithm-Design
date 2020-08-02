using System;
using System.Collections.Generic;

namespace IsBSThard
{
    class Program
    {
        public static List<long> inorder;
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            if (n != 0)
            {
                long[] key = new long[n];
                long[][] child = new long[n][];
                for (int i = 0; i < n; i++)
                {
                    var arr = Console.ReadLine().Split(' ');
                    child[i] = new long[2];
                    child[i][0] = long.Parse(arr[1]);
                    child[i][1] = long.Parse(arr[2]);
                    key[i] = long.Parse(arr[0]);
                }
                inorder = new List<long>();
                InOrderTraversal(key, child, 0);
                if (IsBST(key, child))
                    Console.WriteLine("CORRECT");
                else
                    Console.WriteLine("INCORRECT");
            }
            else
                Console.WriteLine("CORRECT");
        }

        public static void InOrderTraversal(long[] key, long[][] child, long v)
        {
            if (v == -1)
                return;
            InOrderTraversal(key, child, child[v][0]);
            inorder.Add(v);
            InOrderTraversal(key, child, child[v][1]);
        }

        public static bool IsBST(long[] key, long[][] child)
        {
            for (int i = 0; i < inorder.Count-1; i++)
            {
                long first = inorder[i];
                long second = inorder[i + 1];
                if (key[first] > key[second])
                    return false;
                if (key[first] == key[second] && child[second][0] == first)
                    return false;
            }

            return true;
        }
    }
}
