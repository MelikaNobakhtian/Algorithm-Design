using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q4Vaccine : Processor
    {
        public Q4Vaccine(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, string>)Solve);
        public class Node
        {
            public int[] Substr;
            public List<Node> Children { get; set; }
            public Node()
            {
                Substr = new int[2];
                Children = new List<Node>();
            }
        }

        class SuffixTree
        {
            public Node root;
            public string[] results;
            long NodeCount = 0;
            int lastidx;
            string Text;
            public SuffixTree(string text)
            {
                Text = text;
                root = new Node();
                root.Substr[0] = int.MaxValue;
                lastidx = text.Length - 1;
                long len = text.Length;
                for (int i = 0; i < len; i++)
                {
                    this.AddSuffix(i);
                }
            }

           

            private void AddSuffix(int start)
            {
                int i = start;
                Node source = root;
                while (i <= lastidx)
                {
                    int childidx = 0;
                    Node presentnode;
                    char ch = Text[i];
                    while (true)
                    {
                        var child = source.Children;
                        if (childidx == child.Count)
                        {
                            Node node = new Node();
                            NodeCount++;
                            node.Substr[0] = start;
                            node.Substr[1] = lastidx;
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
                            Node node = new Node();
                            NodeCount++;
                            node.Substr[0] = presentnode.Substr[0];
                            node.Substr[1] = idx - 1;
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
        public string Solve(string dna, string pattern)
        {
            SuffixTree text = new SuffixTree(dna + "$");
            for(int i = 0; i < text.root.Children.Count; i++)
            {
                var result = matchproblem(dna + "$", pattern, text.root.Children[i]);
                if (result.Item2 > 1)
                    continue;
                if (result.Item1 == false)
                    break;
            }
            return null;
            //this.ExcludeTestCaseRangeInclusive(40, 106);
            //string result = null;
            //for(int i = 0; i <= dna.Length - pattern.Length;i++)
            //{
            //    if (match(dna, pattern, i))
            //        result += i + " ";
            //}
            //if (result == null)
            //    result = "No Match!";
            //return result.Trim();
        }

        //public List<int> find(Node node)
        //{
        //    List<int> maybe = new List<int>();
        //}

        public Tuple<bool,int> matchproblem(string text, string pattern,Node child)
        {
            bool smaller = false;
            int distance = 0;
            for(int i = 0; i < pattern.Length; i++)
            {
                if (child.Substr[1] < child.Substr[0] + i)
                {
                    smaller = true;
                    break;
                }
                if (pattern[i] != text[child.Substr[0] + i])
                    distance++;
                if (distance > 1)
                    return new Tuple<bool, int>(false, 2);
            }
            return new Tuple<bool, int>(smaller, distance);
        }
       public bool match (string text,string pattern,int i)
        {
            int dis = 0;
            for(int j = 0; j < pattern.Length && dis<2; j++)
            {
                if (pattern[j] != text[i + j])
                    dis++;
            }
            if (dis < 2)
                return true;
            else
                return false;
        }
    }
}
