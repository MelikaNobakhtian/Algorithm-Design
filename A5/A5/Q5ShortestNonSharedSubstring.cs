using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q5ShortestNonSharedSubstring : Processor
    {
        public Q5ShortestNonSharedSubstring(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, String>)Solve);

        public class Node
        {
            public Node Parent;
            public int[] Substr;
            public bool InSecond;
            public List<Node> Children { get; set; }
            public Node(bool inSecond)
            {
                InSecond = inSecond;
                Substr = new int[2];
                Children = new List<Node>();
            }
        }
        class SuffixTree
        {
            public Node root;
            int lastidx;
            int LastForOneString;
            string Text;
            string FirstText;
            string SecondText;

            public SuffixTree(string text1, string text2, int length)
            {
                FirstText = text1;
                SecondText = text2;
                LastForOneString = length - 1;
                Text = text1 + text2;
                root = new Node(false);
                root.Substr[0] = int.MaxValue;
                lastidx = Text.Length - 1;
                long len = Text.Length;
                bool IsSecond = false;
                for (int i = 0; i < len; i++)
                {
                    if (i > LastForOneString)
                        IsSecond = true;
                    this.AddSuffix(i, IsSecond);
                }
            }

            public string BFs()
            {
                string result = null;
                int stidx = 0;
                Queue<Node> nodes = new Queue<Node>();
                nodes.Enqueue(root);
                while (nodes.Count != 0)
                {
                    Node node = nodes.Dequeue();
                    if (node.Substr[0] != int.MaxValue)
                    {
                        if (!node.InSecond)
                        {
                            string tmp = FindPath(node).Trim('#');
                            int idx = FirstText.IndexOf(tmp);
                            if (!SecondText.Contains(tmp))
                            {
                                if (result == null)
                                {
                                    result = tmp;
                                    stidx = idx;
                                }
                                else if (tmp.Length < result.Length)
                                {
                                    result = tmp;
                                    stidx = idx;
                                }
                                else if (tmp.Length == result.Length && idx <= stidx)
                                {
                                    result = tmp;
                                    stidx = idx;
                                }
                            }
                        }
                     
                    }
                    foreach (var child in node.Children)
                        nodes.Enqueue(child);
                }
                return result;

            }

            private string FindPath(Node node)
            {
                string result = null;
                result += Text[node.Substr[0]];
                Node parent = node.Parent;
                while (parent != root)
                {
                    result = Text.Substring(parent.Substr[0], parent.Substr[1] - parent.Substr[0] + 1) + result;
                    parent = parent.Parent;
                }
                return result;
            }

            private void AddSuffix(int start, bool isSecond)
            {
                int i = start;
                Node source = root;
                int last = isSecond ? lastidx : LastForOneString;
                while (i <= last)
                {
                    int childidx = 0;
                    Node presentnode;
                    char ch = Text[i];
                    while (true)
                    {
                        var child = source.Children;
                        if (childidx == child.Count)
                        {
                            Node node = new Node(isSecond);
                            node.Parent = source;
                            node.Substr[0] = start;
                            node.Substr[1] = last;
                            source.Children.Add(node);
                            return;
                        }
                        presentnode = child[childidx];
                        if (Text[presentnode.Substr[0]] == ch)
                            break;
                        childidx++;
                    }
                    int idx = presentnode.Substr[0];
                    int k = 0;
                    int end = presentnode.Substr[1];
                    while (idx <= end)
                    {
                        if (Text[idx] != Text[i + k])
                        {
                            Node node = new Node(isSecond);
                            node.Substr[0] = presentnode.Substr[0];
                            node.Substr[1] = idx - 1;
                            node.Parent = source;
                            presentnode.Parent = node;
                            node.Children.Add(presentnode);
                            presentnode.Substr[0] = idx;
                            source.Children[childidx] = node;
                            presentnode = node;
                            break;
                        }
                        idx++;
                        k++;
                    }
                    i += k;
                    start = i;
                    source = presentnode;
                }
            }
        }

        private string Solve(string text1, string text2)
        {
            SuffixTree suffixTree = new SuffixTree(text1 + "#", text2 + "$", text2.Length + 1);
            string result = suffixTree.BFs();
            return result;
        }
    }
}
