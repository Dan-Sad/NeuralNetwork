using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork
{
    class Pictures
    {
        static int Height, Width;

        public static byte[] ImageToByte(Image img)
        {
            var bmp = new Bitmap(img);
            var bytes = new byte[bmp.Width * bmp.Height];
            Height = img.Height;
            Width = img.Width;

            int i = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var IsWhite = bmp.GetPixel(x, y).R >= 230 &&
                        bmp.GetPixel(x, y).G >= 230 && bmp.GetPixel(x, y).B >= 230;
                    bytes[i++] = (byte)(IsWhite ? 0 : 1);
                }
            }

            return bytes;
        }

        public static void PrintPicturesConsole(byte[] picture)
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
    }
}
