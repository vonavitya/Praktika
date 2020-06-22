using System;
using System.Globalization;

namespace _8
{
    class Program
    {
        //ввод данных для выбора дальнейшего действия : 1 - продолжить, 2 - выход
        static byte InputByteNumber(string action = "Выберите действие", string mistake = "Введите либо 1, либо 2",
                                   int left = 1, int right = 2) //всего 2 действия на выбор
        {
            Console.Write(action + ": ");
            byte z;
            string input;
            do
            {
                input = Console.ReadLine();
                if (!byte.TryParse(input, out z) || z < left || z > right)
                    Console.Write("   " + mistake + ": ");
            } while (!byte.TryParse(input, out z) || z < left || z > right); //пока юзер не введёт нужное число
            return z;
        }


        //ввод матрицы смежности
        static byte[,] InputMatrix(out byte[,] matrix, byte n)
        {
            matrix = new byte[n, n];
            Console.WriteLine("\nВведите матрицу смежности графа. В каждой строке д.б. одинаковое\nколичество элементов (элемент - это либо 0, либо 1), равное n.\nМатрица не д.б ориентированной.\n");

            bool ok = true; // проверка корректности ввода
            do
            {
                if (ok == false) //если в предудущей итерации введен ориентированный граф
                    Console.WriteLine("Вы ввели ориентированный граф. Повторите ввод!");

                //n вершин = n строк
                for (int i = 0; i < n; i++)
                {
                    ok = true;
                    do
                    {
                        if (!ok) //если в предудущей итерации введена некорректная строка
                            Console.WriteLine("Некорректная строка, повторите ввод!");
                        ok = true;

                        //ввод строки
                        string input = Console.ReadLine();

                        //удаление лишних пробелов
                        input = input.Trim();
                        while (input.IndexOf("  ") != -1)
                            input = input.Replace("  ", " ");
                        while (input != "" && input[input.Length - 1] == ' ')
                            input = input.Remove(input.Length - 1, 1);

                        //если нечего не осталось ввод некорректный
                        if (input == "")
                            ok = false;
                        else
                        {
                            //деление строки по пробелам на элементы - 0/1
                            string[] points = input.Split(' ');
                            //если нет элементов
                            if (points.Length != n)
                                ok = false;
                            else
                                //преобразование всех считанных элементов в строку матрицы смежности
                                for (int j = 0; j < n; j++)
                                    if (!byte.TryParse(points[j], out matrix[i, j]) || matrix[i, j] > 1 || matrix[i, j] < 0)
                                        ok = false;
                        }

                    } while (ok == false); //покаа ввод строки некорректен
                }

                //проверка матрицы, чтобы была неориентированнная
                for (int i = 0; i < n - 1; i++)
                    for (int j = i + 1; j < n; j++)
                        if (matrix[i, j] != matrix[j, i])
                            ok = false;

            } while (ok == false); //пока ввод матрицы некорректен

            return matrix;
        }


        //рандомное формирование матрицы смежности
        static void RandomMatrix(out byte[,] matrix, int n)
        {
            matrix = new byte[n, n];
            int sochl = InputByteNumber("Введите количество точек сочленения для генерации", "Нужно целое число от 0 до " + (n - 2), 0, (n - 2));

            int[] pointMas = new int[n];    //номер элемента[номер вершины графа]
            for (int i = 0; i < n; i++)
                pointMas[i] = i;
            int notUsedPoint = n; //количество элементов массива неюзанных вершин, чтобы не использовать ресайз

            Random rnd = new Random();

            int[] gPoints;           //группа вершин
            int k = n / (sochl + 1); //количество вершин в группе
            int r = n % (sochl + 1); //если поравну неполучается, у r групп будет на вершину больше
            int firstPoint = -1;     //для хранения точки сочленения группы

            //проход по всемм группам
            for (byte i = 1; i <= sochl + 1; i++)
            {
                //сколько вершин в группе
                int tekK;
                if (i <= r)
                    tekK = k + 1;
                else
                    tekK = k;

                //выбор вершин для группы
                gPoints = new int[tekK];
                for (int j = 0; j < tekK; j++)
                {
                    //выюор из массива неюзанных вершин
                    byte tekPoint = (byte)rnd.Next(0, notUsedPoint);
                    gPoints[j] = pointMas[tekPoint];

                    //удаление использованной вершины из массива неиспользованных
                    for (int h = tekPoint; h < pointMas.Length - 1; h++)
                        pointMas[h] = pointMas[h + 1];
                    notUsedPoint--;
                }

                //группа образует клику в исходном графе
                for (int j = 0; j < tekK; j++)
                    for (int h = j + 1; h < tekK; h++)
                    {
                        matrix[gPoints[j], gPoints[h]] = 1;
                        matrix[gPoints[h], gPoints[j]] = 1;
                    }

                if (firstPoint != -1)
                {
                    for (int j = 0; j < tekK; j++)
                    {
                        matrix[firstPoint, gPoints[j]] = 1;
                        matrix[gPoints[j], firstPoint] = 1;
                    }

                }

                firstPoint = gPoints[0];


            }
        }


        //выбор рандомно или вручную формируется матрица
        static byte ROI()
        {
            Console.WriteLine("1 - ввести матрицу смежности с клавиатуры;");
            Console.WriteLine("2 - сгенерировать матрицу смежности автоматически.");
            byte choice = (byte)InputByteNumber(action: "Выберите действие",
                                                mistake: "Нужно целое число из отрезака от 1 до 2",
                                                left: 1, right: 2);
            return choice;
        }


        static void MakeMatrix(out byte[,] matrix, byte ROI)
        {
            byte n = InputByteNumber("\nВведите количество вершин графа n", "Нужно целое число от 2 до 100", 2, 100);
            switch (ROI)
            {
                case 2:  //заполнение массива случайными числами
                    RandomMatrix(out matrix, n);
                    break;
                default: //ввод с клавиатуры по умолчанию
                    InputMatrix(out matrix, n);
                    break;
            }
            Console.Clear();
            PrintArray(matrix);
        }


        //печать массива
        static void PrintArray(byte[,] mas, string description = "Матрица смежности")
        {
            Console.WriteLine(description + ": ");
            if (mas.Length == 0)
                Console.WriteLine("массив пустой");
            //если массив не пустой, то вывод всех элементов:
            else
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                        Console.Write(mas[i, j] + " ");
                    Console.WriteLine();
                }
        }


        //1 итерация программы
        static void Actions()
        {
            byte[,] matrix;
            //создание матрицы смежности графа
            MakeMatrix(out matrix, ROI());
            //вычисление исходного числа компонент связности
            byte nParts = WidthNumeration(ConvertedGraphFromSmeghnToNeighbours(matrix));
            int n = 0; //количество точек сочленения

            //поочередно убираем из массива по вершине и смотрим, изменилось ли количество компонент связности
            for (byte i = 0; i < matrix.GetLength(0); i++)
                if (CountParts(matrix, i) > nParts)
                    n++;

            Console.WriteLine("\nВ графе точек сочленения " + n);
        }


        //удаление из матрицы выершин del и возрврщение количества компонент связности новой матрицы
        static byte CountParts(byte[,] matrix, byte del)
        {
            byte[,] m = new byte[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (byte i = 0; i < matrix.GetLength(0); i++)
                //для обхода строки del
                if (i < del)
                {
                    for (byte j = 0; j < matrix.GetLength(0); j++)
                        //для обхода столбца del
                        if (j < del)
                            m[i, j] = matrix[i, j];
                        else if (j > del)
                            m[i, j - 1] = matrix[i, j];
                }
                else if (i > del)
                {
                    for (byte j = 0; j < matrix.GetLength(0); j++)
                        if (j < del)
                            m[i - 1, j] = matrix[i, j];
                        else if (j > del)
                            m[i - 1, j - 1] = matrix[i, j];
                }
            //вычисление и возрврщение количества компонент связности новой матрицы
            return WidthNumeration(ConvertedGraphFromSmeghnToNeighbours(m));
        }


        /* переделывание матрицы смежности графа к соответствию связанных вершин
         * выход: граф[номер вершины][соседи вершины] */
        private static int[][] ConvertedGraphFromSmeghnToNeighbours(byte[,] graph   /*граф, который нужно переделать*/)
        {
            //создание матрицы соответствия вершин для графа, заданного матрицей соответствия вершин
            int[][] pointNeighbours = new int[graph.GetLength(0)][];

            //цикл прохода по каждой вершине
            for (int tekPoint = 0; tekPoint < graph.GetLength(0); tekPoint++)
            {
                //поскольку заранее низвестно сколько соседей у вершины. создаем массив вершин, где
                //кол-во элементов = кол-во вершин графа - сама вершина
                int[] futureNeighbours = new int[graph.GetLength(1) - 1];
                int tekFutureNeighbour = 0; //для подсчета кол-ва соседей вершины

                //заполняем массив соседей вершины и считаем их
                for (int tekNeighbour = 0; tekNeighbour < graph.GetLength(1); tekNeighbour++)
                    if (tekPoint != tekNeighbour && graph[tekPoint, tekNeighbour] != 0)
                    {
                        futureNeighbours[tekFutureNeighbour] = tekNeighbour;
                        tekFutureNeighbour++;
                    }

                //теперь кол-во соседей известно, переписываем все в чистовой массив соответствия связанных вершин
                pointNeighbours[tekPoint] = new int[tekFutureNeighbour];
                for (int tekNeighbour = 0; tekNeighbour < pointNeighbours[tekPoint].Length; tekNeighbour++)
                    pointNeighbours[tekPoint][tekNeighbour] = futureNeighbours[tekNeighbour];
            }

            return pointNeighbours;
        }


        static byte WidthNumeration(int[][] graph /*граф для подсчета компонент связности*/)
        {
            int[] pointNums = new int[graph.Length];  //исходный номер[присвоенный номер при обходе в ширину] - для очереди
            byte nParts = 0; //подсчет компонент связности

            bool[] alreadyNumed = new bool[graph.Length];  //массив для проверки, пронумерована ли та или иная вершина
            //изначально нет пронумерованных вершин
            for (int tekPoint = 0; tekPoint < graph.Length; tekPoint++)
                alreadyNumed[tekPoint] = false;

            int tekPointNum = 0,                    //место, на котором стоит последняя вершина в массиве нумерации
                tekNAlreadyNumed = 0;               //кол-во развернутых вершина
            pointNums[tekPointNum] = 0;             //самой первой вершиной в нумерации будет нулевая
            alreadyNumed[tekPointNum] = true;       //запоминаем, что нулевая вершина уже пронумерована

            //пока какой-нибудь вершине не будет присвоен последний номер
            while (tekPointNum < graph.Length - 1)
            {
                //запись в очередь всех соседей раскрертывающейся вершины (она под номером tekNalreadyNumed в массиве pointNums)
                for (int tekNeighbour = 0; tekNeighbour < graph[pointNums[tekNAlreadyNumed]].Length; tekNeighbour++)
                    //если этот сосед еще не пронумерован
                    if (alreadyNumed[graph[pointNums[tekNAlreadyNumed]][tekNeighbour]] == false)
                    {
                        tekPointNum++;
                        pointNums[tekPointNum] = graph[pointNums[tekNAlreadyNumed]][tekNeighbour];
                        alreadyNumed[graph[pointNums[tekNAlreadyNumed]][tekNeighbour]] = true;
                    }

                //т.к. всех соседей этой вершиныуже поместили в очередь, она стала развёрнутой
                tekNAlreadyNumed++;

                //в случае если очередь закончилась, а вершины пронумерованы не все, увеличивается число компонент связности
                if (tekPointNum < graph.Length - 1 && pointNums[tekNAlreadyNumed] == 0)
                {
                    nParts++;
                    //поиск первой непронумерованной вершины
                    int tekPoint;
                    for (tekPoint = 1; alreadyNumed[tekPoint] == true; tekPoint++) ;

                    //нумерация этой вершины
                    pointNums[tekNAlreadyNumed] = tekPoint;
                    tekPointNum++;
                    alreadyNumed[tekPoint] = true;
                }

            }

            return nParts;
        }


        //выбор дальнейшего действия
        static byte ChoiceOfActions()
        {
            Console.WriteLine("\n1 - продолжить работу");
            Console.WriteLine("2 - выйти из приложения");
            return InputByteNumber();
        }


        static void Main(string[] args)
        {
            //цикл работы приложения
            do
            {
                Console.Clear();
                Console.WriteLine("Программа вычисляет количество точек сочленения графа, заданного матрицей смежности.\n");
                Actions();
            } while (ChoiceOfActions() == 1);
        }
    }
}
