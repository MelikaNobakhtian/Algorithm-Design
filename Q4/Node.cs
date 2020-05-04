using System;
using System.Collections.Generic;
using System.Text;

namespace Q4
{
    public class Node
    {
        public int Number { get; set; }
        public List<int> Edges { get; set; }

        public Node(int number)
        {
            Number = number;
            Edges = new List<int>();
        }
    }
}
