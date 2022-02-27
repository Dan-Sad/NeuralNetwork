using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var pictures = new List<double[]>()
            {
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture1.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture2.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture3.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture4.bmp")),

                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture1_noise.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture2_noise.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture3_noise.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture4_noise.bmp")),

                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture1_noise2.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture2_noise2.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture3_noise2.bmp")),
                Pictures.ImageToByte(new Bitmap(@"C:\Users\sad1d\OneDrive\Изображения\Picture4_noise2.bmp")),
            };

            for (int i = 0; i < pictures.Count; i++)
            {
                //Pictures.PrintPicturesConsole(pictures[i]);
                //Console.WriteLine();
            }

            var count = new List<double[]>();

            foreach (var picture in pictures)
            {
                count.Add(Pictures.CountPixelsOnSix(picture));
            }

            // массив входных обучающих векторов
            Vector[] inputLayer = {
                new Vector(count[0]),
                new Vector(count[1]),
                new Vector(count[2]),
                new Vector(count[3]),
            };

            // массив выходных обучающих векторов
            Vector[] outputLayer = {
                new Vector(new double[]{ 1, 0, 0, 0}), 
                new Vector(new double[]{ 0, 1, 0, 0}), 
                new Vector(new double[]{ 0, 0, 1, 0}), 
                new Vector(new double[]{ 0, 0, 0, 1}),
            };

            Network network = new Network(new int[] { 6, 56, 4 }); // создаём сеть с двумя входами, тремя нейронами в скрытом слое и одним выходом

            network.Train(inputLayer, outputLayer, 0.1, 1e-3, 500); // запускаем обучение сети 

            for (int i = 0; i < inputLayer.Length; i++)
            {
                Vector resultNetwork = network.Forward(inputLayer[i]);
                Console.WriteLine($"Y: {outputLayer[i][0]}, {outputLayer[i][1]}, {outputLayer[i][2]}, {outputLayer[i][3]}; output: {resultNetwork[0]}, {resultNetwork[1]}, {resultNetwork[2]}, {resultNetwork[3]}");
            }

            Console.WriteLine();

            Vector[] testX = {
                new Vector(count[4]),
                new Vector(count[5]),
                new Vector(count[6]),
                new Vector(count[7]),
            };

            Vector[] testX2 = {
                new Vector(count[8]),
                new Vector(count[9]),
                new Vector(count[10]),
                new Vector(count[11]),
            };

            for (int i = 0; i < testX.Length; i++)
            {
                Vector output = network.Forward(testX[i]);
                Console.WriteLine($"Y: {outputLayer[i][0]}, {outputLayer[i][1]}, {outputLayer[i][2]}, {outputLayer[i][3]}; output: {output[0]}, {output[1]}, {output[2]}, {output[3]}");
            }

            Console.WriteLine();

            for (int i = 0; i < testX2.Length; i++)
            {
                Vector output = network.Forward(testX2[i]);
                Console.WriteLine($"Y: {outputLayer[i][0]}, {outputLayer[i][1]}, {outputLayer[i][2]}, {outputLayer[i][3]}; output: {output[0]}, {output[1]}, {output[2]}, {output[3]}");
            }

            Console.ReadKey();
        }

    }
}
