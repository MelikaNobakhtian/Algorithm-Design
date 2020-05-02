using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TestCommon;
using E1;

namespace Exam1
{
    public class Q3SeamCarving1 : Processor // Calculate Energy
    {
        public Q3SeamCarving1(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            // Parse input file
            var result = inStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int row = result.Length;
            int column = result[0].Split('|').Length;
            //result= inStr.Split(new char[] { '\r', '\n' ,'|'}, StringSplitOptions.RemoveEmptyEntries);
            Color[,] data = new Color[row,column];
            for(int i = 0; i < row; i++)
            {
                var st= result[i].Split('|');
                for (int j = 0; j < column; j++)
                {
                    var another = st[j].Split(',');
                    int red = int.Parse(another[0]);
                    int green = int.Parse(another[1]);
                    int blue = int.Parse(another[2]);
                    Color color = System.Drawing.Color.FromArgb(red,green,blue);
                    data[i, j] = color;
                }
            }
            var solved = Solve(data,row,column);
            // convert solved into output string
            string final = null;
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    if (j == column - 1)
                    {
                        final += solved[i, j];
                        break;
                    }
                    final += solved[i, j] + ",";

                }
                final += '\n';
            }
            return final;
        }
            

        public double[,] Solve(Color[,] data,int row,int column)
        {
            var result = Program.ComputeEnergy(data, row, column);
            return result;
        }
    }

    public class Q3SeamCarving2 : Processor // Find Seam
    {
        public Q3SeamCarving2(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            var result = inStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int row = result.Length;
            int column = result[0].Split(',').Length;
            // Parse input file
            double[,] data = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                var st = result[i].Split(',');
                for (int j = 0; j < column; j++)
                {
                    data[i, j] = double.Parse(st[j]);
                }
            }
            var solved = Solve(data,row,column);
            string final = null;
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < solved[i].Length; j++)
                {
                    if (j == solved[i].Length - 1)
                    {
                        final += $"{solved[i][j]}";
                        break;
                    }
                    final += $"{solved[i][j]}" + ',';
                }
                final += '\n';
            }
            // convert solved into output string
            return final;
        }


        public int[][] Solve(double[,] data,int row,int column)
        {
            var result1= Program.findVerticalSeam(data, row, column);
            var result2 = Program.findHorizontalSeam(data, row, column);
            int[][] result = new int[2][];
            result[0] = result1;
            result[1] = result2;
            return result;
        }
    }

    public class Q3SeamCarving3 : Processor // Remove Seam
    {
        public Q3SeamCarving3(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            var result = inStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int rows = int.Parse(result[0]);
            int column = result[1].Split(',').Length;
            double[,] energy = new double[rows, column];
            for(int i = 1; i <= rows; i++)
            {
                string[] dt = result[i].Split(',');
                for(int j = 0; j < column; j++)
                {
                    energy[i-1, j] =Convert.ToDouble(dt[j]);
                }
            }
            int numbers = int.Parse(result[rows+1]);
            char[] modes = new char[numbers];
            int[][] pixels = new int[numbers][];
            for(int i = rows + 2,j=0; i < result.Length; i++,j++)
            {
                modes[j] = result[i][0];
                List<int> pix = new List<int>();
                for(int k = 2; k < result[i].Length; k++)
                {
                    if(result[i][k]!=',')
                         pix.Add(int.Parse(result[i][k].ToString()));
                }
                pixels[j] = pix.ToArray();
            }
            var solved = Solve(energy,rows,column,modes,pixels);
            // convert solved into output string
            string final = null;
            rows = solved.GetUpperBound(0)+1;
            column = solved.GetUpperBound(1)+1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <=column; j++)
                {
                    if (j == column-1 )
                    {
                        final += solved[i, j].ToString("F"+2);
                        break;
                    }
                    final += solved[i, j].ToString("F" + 2) + ",";

                }
                final += '\n';
            }
            return final;
        }


        public double[,] Solve(double[,] data,int row,int column,char[] modes,int[][] pixels)
        {
            double[,] result = null;
            for(int i = 0; i < modes.Length; i++)
            {
                if (modes[i] == 'v')
                {
                    result=Program.removeVerticalSeam(data, pixels[i], row, column);
                }
                else
                {
                    result = Program.removeHorizontalSeam(data, pixels[i], row, column);
                }
            }
            return result;
        }
    }
}
