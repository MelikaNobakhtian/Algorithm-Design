using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace E1
{
    public class Utilities
    {
        /// <summary>
        /// Loads a photo from a given path.
        /// </summary>
        /// <param name="path">Path to the photo</param>
        /// <returns></returns>
        public static Image LoadImage(string path)
        {
            Image img = Image.FromFile(path);
            return img;
        }
        /// <summary>
        /// Converts Color array to bitmap and saves it to a file.
        /// Be sure to comment every use of this method before you push your code to git.
        /// </summary>
        /// <param name="img">Color array</param>
        /// <param name="path">Directory to save the image to</param>
        /// <param name="name">Name of the saved image</param>
        /// <param name="direction">Vertical('V') or Horizontal('H')</param>
        public static void SavePhoto(Color[,] img, string path, string name, char direction)
        {
            var bmp = ConvertToBitmap(img, direction);
            bmp.Save(path + name + ".jpg");
        }

        /// <summary>
        /// Converts 2D list matrix to 2D array matrix.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>2D Color array</returns>
        public static Color[,] ConvertToArray(List<List<Color>> input)
        {
            Color[,] output = new Color[input.Count, input[0].Count];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Count; j++)
                {
                    output[i, j] = input[i][j];
                }
            }
            return output;
        }

        /// <summary>
        /// Converts Color array to bitmap.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="dir">Vertical('V') or Horizontal('H')</param>
        /// <returns></returns>
        public static Bitmap ConvertToBitmap(Color[,] bmp, char dir)
        {
            switch (dir)
            {
                case 'V':
                    Bitmap picV = new Bitmap(bmp.GetLength(1), bmp.GetLength(0), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    for (int i = 0; i < bmp.GetLength(1); i++)
                    {
                        for (int j = 0; j < bmp.GetLength(0); j++)
                        {
                            picV.SetPixel(i, j, bmp[j, i]);
                        }
                    }
                    return picV;
                case 'H':
                    Bitmap picH = new Bitmap(bmp.GetLength(0), bmp.GetLength(1), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    for (int i = 0; i < bmp.GetLength(0); i++)
                    {
                        for (int j = 0; j < bmp.GetLength(1); j++)
                        {
                            picH.SetPixel(i, j, bmp[i, j]);
                        }
                    }
                    return picH;
            }
            return new Bitmap("");
        }


        /// <summary>
        /// Converts image to 2D Color array.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Color[,] ConvertImageToColorArray(Image img)
        {
            var a = new Color[img.Width, img.Height];
            using (Bitmap bmp = new Bitmap(img))
            {
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        a[i, j] = bmp.GetPixel(i, j);
                    }
                }
            }
            return a;
        }


        /// <summary>
        /// Converts 2D Color array to string array in a form of (R,G,B).
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string[] ConvertColorArrayToRGBMatrix(Color[,] img)
        {
            string[] RGBMatrix = new string[img.GetLength(1)];
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    RGBMatrix[j] += $"({img[i, j].R},{img[i, j].G},{img[i, j].B}) ";
                }
            }
            return RGBMatrix;
        }

        public static void ProcessQ3(string path)
        {

        }
    }
}
