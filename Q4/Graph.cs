using System;
using System.Collections.Generic;
using System.Text;

namespace Q4
{
    public class Graph
    {
        public List<Node> Nodes { get; set; }
        public Graph()
        {
            Nodes = new List<Node>();
        }

        public void MakeSubGraph(Func<int,int,bool> func,int start,int end)
        {
            for(int i = start; i < end; i++)
            {
                Nodes.Add(new Node(i));
            }
            for(int i = start; i < end; i++)
            {
                for(int j = i + 1; j < end; j++)
                {
                    if (func(i, j))
                    {
                        Nodes[i].Edges.Add(j);
                        Nodes[j].Edges.Add(i);
                    }
                }
            }
        }
    }
}
