using System;

namespace _5
{
    class Program
    {
        /* ввод чисел типа double
         * на вход строка описывающая необходимые требования
         * на выход число типа double */
        static double InputNumber(string action, string mistake = "Нужно число от +-5,0 * 10^(-324) до +-1,7 * 10^(308)") //границы типа double
        {
            Console.Write(action + ": ");
            double z;
            string input;
            do
            {
                input = Console.ReadLine();
                if (!double.TryParse(input, out z))
                    Console.Write("   " + mistake + ": ");
            } while (!double.TryParse(input, out z)); //пока юзер не введёт нужное число
            return z;
        }


        //ввод целых чисел
        static int InputIntNumber(string action, string mistake,
                                   int left = -2147483648, int right = 2147483647)
        {
            Console.Write(action + ": ");
            int z;
            string input;
            do
            {
                input = Console.ReadLine();
                if (!int.TryParse(input, out z) || z < left || z > right)
                    Console.Write("   " + mistake + ": ");
            } while (!int.TryParse(input, out z) || z < left || z > right); //пока юзер не введёт нужное число
            return z;
        }


        //выбор рандомно или вручную формируется матрица
        static byte ROI()
        {
            Console.WriteLine("\n1 - ввести матрицу с клавиатуры;");
            Console.WriteLine("2 - сгенерировать матрицу автоматически.");
            byte choice = (byte)InputIntNumber(action: "Выберите действие",
                                                mistake: "Нужно целое число из отрезака от 1 до 2",
                                                left: 1, right: 2);
            return choice;
        }


        //создание матрицы
        static void MakeArray(out double[,] mas, int n, byte ROI = 1)
        {
            switch (ROI)
            {
                case 2:  //заполнение массива случайными числами
                    RandomArray(out mas, n);
                    break;
                default: //ввод с клавиатуры по умолчанию
                    InputArray(out mas, n);
                    break;
            }
            Console.Clear();
        }


        //ввод матрицы с клавиатуры
        static void InputArray(out double[,] mas, int n)
        {
            double[,] FutureMas = new double[n, n]; //массив который будет заполняться
            Console.WriteLine("\nВвод матрицы:\n");
            //цикл ввода
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    FutureMas[i, j] = InputNumber("Элемент (" + (i + 1) + ", " + (j + 1) + ")");
                Console.WriteLine();
            }

            mas = FutureMas;
        }


        //заполнение матрицы случайными числами
        static void RandomArray(out double[,] mas, int n)
        {
            double[,] FutureMas = new double[n, n];         //массив для заполнения
            Random rand = new Random();
            for (int i = 0; i < n; i++)     //цикл заполнения
                for (int j = 0; j < n; j++)
                    FutureMas[i, j] = ((double)rand.Next(-10000, 10000)) / ((double)rand.Next(1, 1000));
            mas = FutureMas;
        }


        //печать массива
        static void PrintArray(double[,] mas, string description = "Массив")
        {
            Console.WriteLine(description + " (все числа выводятся с 3 знаками после запятой, но сохраняются в памяти полностью): ");
            if (mas.Length == 0)
                Console.WriteLine("массив пустой");
            //если массив не пустой, то вывод всех элементов:
            else
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                        Console.Write("{0,10:0.000} ", mas[i, j]);
                    Console.WriteLine();
                }
        }


        //1 итерация программы
        static void Actions()
        {
            double[,] mas;
            int n = InputIntNumber("Введите размерность матрицы (n >= 2)", "Нужно целое число, большее 1", 2);
            MakeArray(out mas, n, ROI());
            PrintArray(mas);

            double max = mas[0, 0];
            for (int i = 0; i < n; i++)
                for (int j = 0; (j <= i && i <= n / 2) || (j <= n - i && i > n / 2); j++)
                    if (mas[i, j] > max)
                        max = mas[i, j];
            Console.WriteLine("\nНаибольшее значение в заданной области = {0:0.000}", max);

        }


        //выбор дальнейшего действия
        static int ChoiceOfActions()
        {
            Console.WriteLine("\n1 - найти наибольший элемент другой матрицы");
            Console.WriteLine("2 - выйти из приложения");
            return InputIntNumber("Выберите действие", "Введите либо 1, либо 2", 1, 2);
        }


        static void Main(string[] args)
        {
            //цикл работы приложения
            do
            {
                Console.Clear();
                Actions();
            } while (ChoiceOfActions() == 1);
        }
    }
}
