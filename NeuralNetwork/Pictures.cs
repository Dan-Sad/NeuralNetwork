using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork
{
    class Pictures
    {
        static int Height, Width;

        public static double[] ImageToByte(Image img)
        {
            var bmp = new Bitmap(img);
            var bytes = new double[bmp.Width * bmp.Height];
            Height = img.Height;
            Width = img.Width;

            int i = 0;
            int powerColor = 127;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var IsWhite = bmp.GetPixel(x, y).R >= powerColor &&
                        bmp.GetPixel(x, y).G >= powerColor && bmp.GetPixel(x, y).B >= powerColor;
                    bytes[i++] = (IsWhite ? 1 : 0);
                }
            }

            return bytes;
        }

        public static void PrintPicturesConsole(double[] picture)
        {
            int i = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (picture[i++] == 1)
                        Console.Write("1");
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }
        }

        public static double[] CountPixelsOnSix(double[] picture)
        {
            double[] count = { 0, 0, 0, 0, 0, 0};
            int i = 1;
            int curLayer = 0;

            foreach (var curByte in picture)
            {
                if (i++ % (Height * Width / 6) == 0 && curLayer != 5)
                    curLayer++;

                if (curByte == 1)
                    count[curLayer]++;
            }

            return count;
        }
    }
}
