using System;

namespace Q4
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Func<int, int, bool>[] funcs = new Func<int, int, bool>[3];
            funcs[0] = (s, t) => true;
            funcs[1] = (s, t) => (s % 2 == 0 && t % 2 == 0) || (s % 2 == 1 && t % 2 == 1);
            funcs[2] = (s, t) => (Math.Abs(s - t) % 2 == 1);
            int number_of_subgraphs = 3;
            Graph network = new Graph();
            for(int i = 0; i < number_of_subgraphs; i++)
            {
                int nodes = rnd.Next(3, 8);
                int start = network.Nodes.Count;
                int end = start + nodes;
                int methodnumber = rnd.Next(0, 2);
                network.MakeSubGraph(funcs[methodnumber], start, end);
            }
        }
    }
}
