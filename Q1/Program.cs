using System;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = Console.ReadLine();
            string b = Console.ReadLine();
            Console.WriteLine(FindHamming(a,b));
        }

        public static string FindHamming(string a,string b)
        {
            int number = 0;
            int len = a.Length;
            for(int i = 0; i < len; i++)
            {
                if (a[i] != b[i])
                    number++;
            }
            if (number % 2 == 1)
                return "Not possible";
            number /= 2;
            string result = null;
            for(int i = 0; i < len; i++)
            {
                if (a[i] != b[i] && number > 0)
                {
                    result += b[i];
                    number--;
                }
                else
                    result += a[i];
            }

            return result;
        }
    }
}
