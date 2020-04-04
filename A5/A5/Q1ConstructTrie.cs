using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public class Node
        {
            public long Index { get; set; }
            public Dictionary<char,Node> Children { get; set; }
            public Node(long idx)
            {
                Index = idx;
                Children = new Dictionary<char, Node>();
            }
        }
        public string[] Solve(long n, string[] patterns)
        {
            long counter = 0;
            List<string> results = new List<string>();
            Node root = new Node(0);
            for(int i = 0; i < n; i++)
            {
                Node levelroot = root;
                long length = patterns[i].Length;
                for(int j = 0; j < length; j++)
                {
                    if (!levelroot.Children.ContainsKey(patterns[i][j]))
                    {
                        counter++;
                        Node child = new Node(counter);
                        levelroot.Children.Add(patterns[i][j], child);
                        results.Add($"{levelroot.Index}->{counter}:{patterns[i][j]}");
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
