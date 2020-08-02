using System;

namespace edit_distance
{
    class Program
    {
        static void Main(string[] args)
        {
            string first = Console.ReadLine();
            string second = Console.ReadLine();
            Console.WriteLine(Solve(first, second));
        }
        public static long Solve(string str1, string str2)
        {
            long[,] distance = new long[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i <= str1.Length; i++)
                distance[i, 0] = i;
            for (int j = 0; j <= str2.Length; j++)
                distance[0, j] = j;
            for (int j = 1; j <= str2.Length; j++)
                for (int i = 1; i <= str1.Length; i++)
                {
                    var insertion = distance[i, j - 1] + 1;
                    var deletion = distance[i - 1, j] + 1;
                    var match = distance[i - 1, j - 1];
                    var mismatch = distance[i - 1, j - 1] + 1;
                    if (str1[i - 1] == str2[j - 1])
                        distance[i, j] = Math.Min(Math.Min(insertion, deletion), match);
                    if (str1[i - 1] != str2[j - 1])
                        distance[i, j] = Math.Min(Math.Min(insertion, deletion), mismatch);
                }

            return distance[str1.Length, str2.Length];
        }
    }
}
