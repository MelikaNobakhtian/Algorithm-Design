using System;
using System.Collections.Generic;

namespace stack
{
    class Program
    {
        public static List<char> brackets = new List<char> { '}', ')', ']' };

        static void Main(string[] args)
        {
            long result = Solve(Console.ReadLine());
            if (result == -1)
                Console.WriteLine("Success");
            else
                Console.WriteLine(result);
        }

        public static long Solve(string str)
        {
            Stack<char> signs = new Stack<char>();
            Stack<long> index = new Stack<long>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(' || str[i] == '[' || str[i] == '{')
                {
                    signs.Push(str[i]);
                    index.Push(i);
                }
                else
                {
                    if (brackets.Contains(str[i]) && signs.Count == 0)
                        return i + 1;
                    else if (!brackets.Contains(str[i]))
                        continue;
                    if (signs.Count != 0)
                    {
                        var top = signs.Pop();
                        index.Pop();
                        if (top == '[' && str[i] != ']')
                            return i + 1;
                        if (top == '(' && str[i] != ')')
                            return i + 1;
                        if (top == '{' && str[i] != '}')
                            return i + 1;
                    }
                }
            }
            long first = 0;
            if (signs.Count == 0)
                return -1;
            while (signs.Count != 0)
            {
                signs.Pop();
                first = index.Pop();
            }
            return first + 1;
        }
    }
}
