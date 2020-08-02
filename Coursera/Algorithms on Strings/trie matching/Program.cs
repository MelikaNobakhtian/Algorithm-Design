using System;
using System.Collections.Generic;

namespace trie_matching
{
    public class Node
    {
        public bool End;
        public Node[] Children { get; set; }
        public Node()
        {
            End = false;
            Children = new Node[4];
        }
    }
    class Program
    {
        public static Dictionary<char, int> Letters = new Dictionary<char, int>()
        {
            {'A',0 },
            {'T',1 },
            {'C',2 },
            {'G',3 }
        };
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            long n = long.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
                patterns[i] = Console.ReadLine();
            var results = Solve(text, n, patterns);
            string result = null;
            foreach (var r in results)
                result += r + " ";
            Console.WriteLine(result);
        }

        public static long[] Solve(string text, long n, string[] patterns)
        {
            List<long> results = new List<long>();
            Node root = new Node();
            for (int i = 0; i < n; i++)
            {
                Node levelroot = root;
                long length = patterns[i].Length;
                for (int j = 0; j < length; j++)
                {
                    if (levelroot.Children[Letters[patterns[i][j]]] == null)
                    {
                        Node child = new Node();
                        levelroot.Children[Letters[patterns[i][j]]] = child;
                        levelroot = child;
                        if (j == length - 1)
                        {
                            levelroot.End = true;
                            break;
                        }
                        continue;
                    }
                    levelroot = levelroot.Children[Letters[patterns[i][j]]];
                    if (j == length - 1)
                    {
                        levelroot.End = true;
                        break;
                    }
                }
            }
            long len = text.Length;
            for (int i = 0; i < len; i++)
            {
                Node levelroot = root;
                long start = i;
                for (int j = i; j < len; j++)
                {
                    if (levelroot.Children[Letters[text[j]]] != null)
                    {
                        levelroot = levelroot.Children[Letters[text[j]]];
                        if (levelroot.End == true)
                        {
                            results.Add(start);
                            break;
                        }
                    }
                    else
                        break;
                }
            }
            

            return results.ToArray();

        }
    }
}
