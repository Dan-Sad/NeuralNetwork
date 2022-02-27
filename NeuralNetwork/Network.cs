using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    class Network
    {
        struct Layer
        {
            public Vector x; // вход слоя
            public Vector z; // активированный выход слоя
            public Vector df; // производная функции активации слоя
        }

        Matrix[] weights; // матрицы весов слоя
        Layer[] valueOnLayer; // значения на каждом слое
        Vector[] deltas; // дельты ошибки на каждом слое

        int countLayers; // число слоёв

        public Network(int[] sizes)
        {
            Random random = new Random(DateTime.Now.Millisecond); // создаём генератор случайных чисел

            countLayers = sizes.Length - 1; // запоминаем число слоёв

            weights = new Matrix[countLayers]; // создаём массив матриц весовых коэффициентов
            valueOnLayer = new Layer[countLayers]; // создаём массив значений на каждом слое
            deltas = new Vector[countLayers]; // создаём массив для дельт

            for (int k = 1; k < sizes.Length; k++)
            {
                weights[k - 1] = new Matrix(sizes[k], sizes[k - 1], random); // создаём матрицу весовых коэффициентов

                valueOnLayer[k - 1].x = new Vector(sizes[k - 1]); // создаём вектор для входа слоя
                valueOnLayer[k - 1].z = new Vector(sizes[k]); // создаём вектор для выхода слоя
                valueOnLayer[k - 1].df = new Vector(sizes[k]); // создаём вектор для производной слоя

                deltas[k - 1] = new Vector(sizes[k]); // создаём вектор для дельт
            }
        }

        // прямое распространение
        public Vector Forward(Vector input)
        {
            for (int k = 0; k < countLayers; k++)
            {
                if (k == 0)
                {
                    for (int i = 0; i < input.n; i++)
                        valueOnLayer[k].x[i] = input[i];
                }
                else
                {
                    for (int i = 0; i < valueOnLayer[k - 1].z.n; i++)
                        valueOnLayer[k].x[i] = valueOnLayer[k - 1].z[i];
                }

                for (int i = 0; i < weights[k].n; i++)
                {
                    double y = 0;

                    for (int j = 0; j < weights[k].m; j++)
                        y += weights[k][i, j] * valueOnLayer[k].x[j];

                    // активация с помощью сигмоидальной функции
                    valueOnLayer[k].z[i] = 1 / (1 + Math.Exp(-y));
                    valueOnLayer[k].df[i] = valueOnLayer[k].z[i] * (1 - valueOnLayer[k].z[i]);

                    // активация с помощью гиперболического тангенса
                    //L[k].z[i] = Math.Tanh(y);
                    //L[k].df[i] = 1 - L[k].z[i] * L[k].z[i];

                    // активация с помощью ReLU
                    //L[k].z[i] = y > 0 ? y : 0;
                    //L[k].df[i] = y > 0 ? 1 : 0;
                }
            }

            return valueOnLayer[countLayers - 1].z; // возвращаем результат
        }

        // обратное распространение
        void Backward(Vector output, ref double error)
        {
            int last = countLayers - 1;

            error = 0; // обнуляем ошибку

            for (int i = 0; i < output.n; i++)
            {
                double e = valueOnLayer[last].z[i] - output[i]; // находим разность значений векторов

                deltas[last][i] = e * valueOnLayer[last].df[i]; // запоминаем дельту
                error += e * e / 2; // прибавляем к ошибке половину квадрата значения
            }

            // вычисляем каждую предудущю дельту на основе текущей с помощью умножения на транспонированную матрицу
            for (int k = last; k > 0; k--)
            {
                for (int i = 0; i < weights[k].m; i++)
                {
                    deltas[k - 1][i] = 0;

                    for (int j = 0; j < weights[k].n; j++)
                        deltas[k - 1][i] += weights[k][j, i] * deltas[k][j];

                    deltas[k - 1][i] *= valueOnLayer[k - 1].df[i]; // умножаем получаемое значение на производную предыдущего слоя
                }
            }
        }

        // обновление весовых коэффициентов, alpha - скорость обучения
        void UpdateWeights(double alpha)
        {
            for (int k = 0; k < countLayers; k++)
            {
                for (int i = 0; i < weights[k].n; i++)
                {
                    for (int j = 0; j < weights[k].m; j++)
                    {
                        weights[k][i, j] -= alpha * deltas[k][i] * valueOnLayer[k].x[j];
                    }
                }
            }
        }

        public void Train(Vector[] X, Vector[] Y, double alpha, double eps, int epochs)
        {
            int epoch = 1; // номер эпохи

            double error; // ошибка эпохи

            do
            {
                error = 0; // обнуляем ошибку

                // проходимся по всем элементам обучающего множества
                for (int i = 0; i < X.Length; i++)
                {
                    Forward(X[i]); // прямое распространение сигнала
                    Backward(Y[i], ref error); // обратное распространение ошибки
                    UpdateWeights(alpha); // обновление весовых коэффициентов
                }

                if (epoch < 20 || epoch % 100 == 0)
                    Console.WriteLine($"epoch: {epoch}, error: {error}"); // выводим в консоль номер эпохи и величину ошибку

                epoch++; // увеличиваем номер эпохи
            } while (epoch <= epochs && error > eps);
        }
    }
}
