using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public Dictionary<char, int> Letters = new Dictionary<char, int>()
        {
            {'A',0 },
            {'T',1 },
            {'C',2 },
            {'G',3 }
        };
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

        public long[] Solve(string text, long n, string[] patterns)
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
            if (results.Count == 0)
                results.Add(-1);

            return results.ToArray();

        }
    }
}
