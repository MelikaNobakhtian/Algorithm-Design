using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace E1
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public string[] Solve(string[] data)
        {
            int dimReduction = int.Parse(data[0].Split()[0]);
            char direction = char.Parse(data[0].Split()[1]);
            string imagePath = data[1];
            var img = Utilities.LoadImage(imagePath);
            var bmp = Utilities.ConvertImageToColorArray(img);
            var res = Solve(bmp, dimReduction, direction);
            Utilities.SavePhoto(res, imagePath, "../../../../asd", direction);
            return Utilities.ConvertColorArrayToRGBMatrix(res);
           // return new string[3];
        }

        private static List<List<Color>> BuildList(Color[,] input, char dim)
        {
            throw new NotImplementedException();
        }

        public static Color[,] Solve(Color[,] input, int reduction, char direction)
        {
            throw new NotImplementedException();
        }



        // sequence of indices for horizontal seam
        public static int[] findHorizontalSeam(double[,] energy,int row,int column)
        {
            //var idx = min(energy, row, column);
            int idx = 0;
            int[] result = new int[column];
            double min = double.MaxValue;
            for(int i = 0; i < row; i++)
            {
                if (energy[i, 1] < min)
                {
                    min = energy[i, 1];
                    idx = i;
                }
            }
            result[0] = result[1] = idx;
            for(int j = 2; j < column-1; j++)
            {
                min = double.MaxValue;
                int tmpidx = 0;
                for(int i = 0; i < row; i++)
                {
                    if(energy[i,j]<=min && Math.Abs(idx - i) <= 1)
                    {
                        tmpidx = i;
                        min = energy[i, j];
                    }
                }
                idx = tmpidx;
                result[j] = idx;
            }
            result[column - 1] = result[column - 2];

            return result;
        }


        // sequence of indices for vertical seam
        public static int[] findVerticalSeam(double[,] energy, int row, int column)
        {
            int idx = 0;
            int[] result = new int[row];
            double min = double.MaxValue;
            for (int i = 0; i < column; i++)
            {
                if (energy[1,i] < min)
                {
                    min = energy[1,i];
                    idx = i;
                }
            }
            result[0] = result[1] = idx;
            for (int j = 2; j < row - 1; j++)
            {
                min = double.MaxValue;
                int tmpidx = 0;
                for (int i = 0; i < column; i++)
                {
                    if (energy[j,i] <= min && Math.Abs(idx - i) <= 1)
                    {
                        tmpidx = i;
                        min = energy[j,i];
                    }
                }
                idx = tmpidx;
                result[j] = idx;
            }
            result[row - 1] = result[row - 2];

            return result;
        }

        

        // energy of pixel at column x and row y
        public static double[,] ComputeEnergy(Color[,] bmp,int row,int column)
        {
            double[,] energy = new double[row, column];
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    if (i - 1 < 0 || i + 1 >= row || j - 1 < 0 || j + 1 >= column)
                    {
                        energy[i,j] = 1000;
                        continue;
                    }
                    var Rx = bmp[i + 1,j].R - bmp[i - 1,j].R;
                    var Gx = bmp[i + 1,j].G - bmp[i - 1,j].G;
                    var Bx = bmp[i + 1,j].B - bmp[i - 1,j].B;
                    var Ry = bmp[i,j + 1].R - bmp[i,j - 1].R;
                    var Gy = bmp[i,j + 1].G - bmp[i,j - 1].G;
                    var By = bmp[i,j + 1].B - bmp[i,j - 1].B;
                    var deltax = Math.Pow(Rx, 2) + Math.Pow(Gx, 2) + Math.Pow(Bx, 2);
                    var deltay = Math.Pow(Ry, 2) + Math.Pow(Gy, 2) + Math.Pow(By, 2);
                    energy[i,j] =Math.Round( Math.Sqrt(deltax + deltay),3);
                }
            }
            return energy;
            //List<List<double>> energy = new List<List<double>>();
            //for(int i = 0; i < bmp.Count; i++)
            //{
            //    energy.Add(new List<double>());
            //    for(int j = 0; j < bmp[i].Count; j++)
            //    {
            //        energy[i].Add(0);
            //        if (i - 1 < 0 || i + 1 >= bmp.Count || j-1<0 || j+1>=bmp.Count)
            //        {
            //            energy[i][j] = 1000;
            //            continue;
            //        }
            //        var Rx = bmp[i + 1][j].R - bmp[i - 1][j].R;
            //        var Gx= bmp[i + 1][j].G - bmp[i - 1][j].G;
            //        var Bx= bmp[i + 1][j].B - bmp[i - 1][j].B;
            //        var Ry = bmp[i][j + 1].R - bmp[i][j - 1].R;
            //        var Gy= bmp[i][j + 1].G - bmp[i][j - 1].G;
            //        var By= bmp[i][j + 1].B - bmp[i][j - 1].B;
            //        var deltax = Math.Pow(Rx, 2) + Math.Pow(Gx, 2) + Math.Pow(Bx, 2);
            //        var deltay = Math.Pow(Ry, 2) + Math.Pow(Gy, 2) + Math.Pow(By, 2);
            //        energy[i][j] = Math.Sqrt(deltax + deltay);
            //    }
            //}
            //return energy;
        }

       

        public static int ArgMin(int t, int minIdx, List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

        // remove horizontal seam from current picture
        public static double[,] removeHorizontalSeam(double[,] energy,int[] seam,int row,int column)
        {
            double[,] newenergy = new double[row - 1, column];
            int k = 0, h = 0;
            for(int j = 0; j < column; j++)
            {
                for(int i = 0; i < row; i++)
                {
                    if (i == seam[j])
                        continue;
                    newenergy[k, h] = energy[i, j];
                    k++;
                }
                h++;
                k = 0;
            }
            return newenergy;

        }

        // remove vertical seam from current picture
        public static double[,] removeVerticalSeam(double[,] energy, int[] seam, int row, int column)
        {
            double[,] newenergy = new double[row , column-1];
            int k = 0, h = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (j == seam[i])
                        continue;
                    newenergy[k, h] = energy[i, j];
                    h++;
                }
                k++;
                h = 0;
            }
            return newenergy;
        }

    }
}
