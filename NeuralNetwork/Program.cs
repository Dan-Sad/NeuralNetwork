using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var pictures = new List<byte[]>()
            {
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture1.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture2.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture3.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture4.bmp")),
            };

            Pictures.PrintPicturesConsole(pictures[0]);



            // массив входных обучающих векторов
            Vector[] X = {
                new Vector(0, 0),
                new Vector(0, 1),
                new Vector(1, 0),
                new Vector(1, 1)
            };

                        // массив выходных обучающих векторов
            //Vector[] Y = {
            //    new Vector(0.0), // 0 ^ 0 = 0
            //    new Vector(1.0), // 0 ^ 1 = 1
            //    new Vector(1.0), // 1 ^ 0 = 1
            //    new Vector(0.0) // 1 ^ 1 = 0
            //};

            //Network network = new Network(new int[] { 2, 4, 1 }); // создаём сеть с двумя входами, тремя нейронами в скрытом слое и одним выходом

            //network.Train(X, Y, 0.8, 1e-3, 5000); // запускаем обучение сети 

            //for (int i = 0; i < 4; i++)
            //{
            //    Vector output = network.Forward(X[i]);
            //    Console.WriteLine($"X: {X[i][0]} {X[i][1]}, Y: {Y[i][0]}, output: {output[0]}");
            //}

            Console.ReadKey();
        }

    }
}
