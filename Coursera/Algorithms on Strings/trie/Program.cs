using System;
using System.Collections.Generic;

namespace trie
{
    public class Node
    {
        public long Index { get; set; }
        public Dictionary<char, Node> Children { get; set; }
        public Node(long idx)
        {
            Index = idx;
            Children = new Dictionary<char, Node>();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            string[] strings = new string[n];
            for (int i = 0; i < n; i++)
                strings[i] = Console.ReadLine();
            string[] results = Solve(n, strings);
            foreach (var r in results)
                Console.WriteLine(r);
        }

        public static string[] Solve(long n, string[] patterns)
        {
            long counter = 0;
            List<string> results = new List<string>();
            Node root = new Node(0);
            for (int i = 0; i < n; i++)
            {
                Node levelroot = root;
                long length = patterns[i].Length;
                for (int j = 0; j < length; j++)
                {
                    if (!levelroot.Children.ContainsKey(patterns[i][j]))
                    {
                        counter++;
                        Node child = new Node(counter);
                        levelroot.Children.Add(patterns[i][j], child);
                        results.Add(levelroot.Index+"->"+counter+":"+patterns[i][j]);
                        levelroot = child;
                        continue;
                    }
                    levelroot = levelroot.Children[patterns[i][j]];
                }
            }

            return results.ToArray();
        }
    }
}
