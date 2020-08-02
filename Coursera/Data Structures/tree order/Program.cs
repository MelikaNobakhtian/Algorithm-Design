using System;
using System.Collections.Generic;

namespace tree_order
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] key = new long[n];
            long[][] child = new long[n][];
            for(int i = 0; i < n; i++)
            {
                var arr = Console.ReadLine().Split(' ');
                key[i] = long.Parse(arr[0]);
                child[i] = new long[2];
                child[i][0] = long.Parse(arr[1]);
                child[i][1] = long.Parse(arr[2]);
            }
            InOrder(0, key, child);
            Console.WriteLine();
            PreOrder(0, key, child);
            Console.WriteLine();
            PostOrder(0, key, child);
           // Console.WriteLine(string.Join(' ', PostOrder(0, key, child)));
        }

        public static void PostOrder(long root,long[] key,long[][] child)
        {
           // long[] post = new long[key.Length];
            //int i = 0;
            Stack<long> nodes = new Stack<long>();
            while (true)
            {
                while (root != -1)
                {
                    nodes.Push(root);
                    nodes.Push(root);
                    root = child[root][0];
                }

                if (nodes.Count == 0)
                    return ;
                root = nodes.Pop();

                if (nodes.Count != 0 && nodes.Peek() == root)
                    root = child[root][1];
                else
                {
                    Console.Write(key[root].ToString() + " ");
                    //post[i] = key[root];
                    //i++;
                    root = -1;
                }
            }
        }

        public static void PreOrder(long root,long[] key,long[][] child)
        {
            //long[] pre = new long[key.Length];
           // int iterator = 0;
            Stack<long> nodes = new Stack<long>();
            long present = root;
            //nodes.Push(root);
            while (nodes.Count != 0 || present!=-1)
            {
                while (present != -1)
                {
                    Console.Write(key[present].ToString() + " ");
                   // pre[iterator] = key[present];
                    //iterator++;
                    if (child[present][1] != -1)
                        nodes.Push(child[present][1]);
                    present = child[present][0];
                }

                if (nodes.Count != 0)
                    present = nodes.Pop();
            }

            //return pre;
        }

        public static void InOrder(long root,long[] key,long[][] child)
        {
            //long[] In = new long[key.Length];
            //int iterator = 0;
            Stack<long> nodes = new Stack<long>();
            long present = root;
            while(present!=-1 || nodes.Count != 0)
            {
                while (present != -1)
                {
                    nodes.Push(present);
                    present = child[present][0];
                }

                present = nodes.Pop();
                Console.Write(key[present].ToString() + ' ');
                //In[iterator] = key[present];
                //iterator++;
                present = child[present][1];

            }
            //return In;

        }
    }
}
