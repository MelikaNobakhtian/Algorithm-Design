using System;
using System.Drawing;
using System.Collections.Generic;

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
        public static int[] findHorizontalSeam(List<List<double>> energy)
        {
            throw new NotImplementedException();
        }


        // sequence of indices for vertical seam
        public static int[] findVerticalSeam(List<List<double>> energy)
        {
            var result = min(energy);
            throw new NotImplementedException();
        }

        public static int[] min (List<List<double>> energy)
        {
            int[] result = new int[2];
            double min = double.MaxValue;
            for(int i = 0; i < energy.Count; i++)
            {
                for(int j = 0; j < energy[i].Count; j++)
                {
                    if (energy[i][j] < min)
                    {
                        min = energy[i][j];
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }
            return result;
        }

        // energy of pixel at column x and row y
        public static List<List<double>> ComputeEnergy(List<List<Color>> bmp)
        {
            List<List<double>> energy = new List<List<double>>();
            for(int i = 0; i < bmp.Count; i++)
            {
                energy.Add(new List<double>());
                for(int j = 0; j < bmp[i].Count; j++)
                {
                    energy[i].Add(0);
                    if (i - 1 < 0 || i + 1 >= bmp.Count || j-1<0 || j+1>=bmp.Count)
                    {
                        energy[i][j] = 1000;
                        continue;
                    }
                    var Rx = bmp[i + 1][j].R - bmp[i - 1][j].R;
                    var Gx= bmp[i + 1][j].G - bmp[i - 1][j].G;
                    var Bx= bmp[i + 1][j].B - bmp[i - 1][j].B;
                    var Ry = bmp[i][j + 1].R - bmp[i][j - 1].R;
                    var Gy= bmp[i][j + 1].G - bmp[i][j - 1].G;
                    var By= bmp[i][j + 1].B - bmp[i][j - 1].B;
                    var deltax = Math.Pow(Rx, 2) + Math.Pow(Gx, 2) + Math.Pow(Bx, 2);
                    var deltay = Math.Pow(Ry, 2) + Math.Pow(Gy, 2) + Math.Pow(By, 2);
                    energy[i][j] = Math.Sqrt(deltax + deltay);
                }
            }
            return energy;
        }

       

        public static int ArgMin(int t, int minIdx, List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

        // remove horizontal seam from current picture
        public static void removeHorizontalSeam(int[] seam, ref List<List<Color>> bmp, ref List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

        // remove vertical seam from current picture
        public static void removeVerticalSeam(int[] seam, ref List<List<Color>> bmp, ref List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

    }
}
