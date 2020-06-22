using System;
using System.IO;

namespace _1
{
    class Program
    {
        //ввод данных для выбора дальнейшего действия : 1 - продолжить, 2 - выход
        static byte InputByteNumber(string action = "Выберите действие", string mistake = "Введите либо 1, либо 2",
                                   byte left = 1, byte right = 2) //всего 2 действия на выбор
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


        //1 итерация программы
        static void Actions()
        {
            int x = 0, y = 0, z = 0,
                t,       //длина текущего хода
                d = 0,   //расстояние от оси Х до текущей клетки по горизонтали (типо перпендикуляра)
                n;       //количество ходов
            string nameOfFile = "INPUT.TXT";

            //создание файла для INPUT, в случае если он отсутствует
            using (FileStream f = new FileStream(nameOfFile, FileMode.OpenOrCreate)) { }
            using (StreamReader f = new StreamReader(nameOfFile))
            {
                //если файла не было - при создании образуется пустой файл
                if (f.Peek() == -1)
                    Console.WriteLine("Файл пустой");
                //в первой строке д.б. количество ходов - неотрицательное число
                else if (!int.TryParse(f.ReadLine().Trim(), out n))
                    Console.WriteLine("Неверный формат количества ходов");
                else
                {
                    bool ok = true; //проверка корректности ввода
                    for (int i = 1; i <= n; i++)
                    {
                        //считывание очередного хода из файла
                        string s = f.ReadLine();
                        //если конец файла (нулевая строка - конец файла)
                        if (s == null)
                        {
                            Console.WriteLine("Несоответствие указанного количества ходов и реального!");
                            ok = false;
                            break;
                        }
                        else
                        {
                            //убираем лишние пробелы в строке
                            s = s.Trim();
                            while (s.IndexOf("  ") != -1)
                                s = s.Replace("  ", " ");
                            while (s != "" && s[s.Length - 1] == ' ')
                                s = s.Remove(s.Length - 1, 1);

                            //если после убирания пробелов ничего не осталось
                            if (s == "")
                            {
                                Console.WriteLine("Неверный формат хода" + i);
                                ok = false;
                                break;
                            }
                            else
                                switch (s[0]) //считывание координаты, по которой идет движение
                                {
                                    case 'X':
                                        s = s.Remove(0, 2);
                                        try
                                        {
                                            //для оси Х нулевой уровень - ось Y
                                            t = Convert.ToInt32(s);
                                            x = x + t;
                                            //но Y и Z нулевые уровени друг для друга, изменение x влечет изменение y и z
                                            y = y + t;
                                            z = z - t;
                                        }
                                        catch //если не целое число / не число
                                        {
                                            Console.WriteLine("Неверный формат хода" + i);
                                            ok = false;
                                        }
                                        break;

                                    case 'Y':
                                        s = s.Remove(0, 2);
                                        try
                                        {
                                            t = Convert.ToInt32(s);
                                            y = y + t;
                                            //при движениии вдоль Z и Y изменяется рассточение до оси X
                                            d = d + t;
                                            //для осей Х и Z нулевой уровень - ось Y, поэтому х и z не меняются
                                        }
                                        catch //если не целое число / не число
                                        {
                                            Console.WriteLine("Неверный формат хода" + i);
                                            ok = false;
                                        }
                                        break;

                                    case 'Z':
                                        s = s.Remove(0, 2);
                                        try
                                        {
                                            t = Convert.ToInt32(s);
                                            x = x - t;
                                            z = z + t;
                                            //при движениии вдоль Z и Y изменяется рассточение до оси X
                                            d = d + t;
                                        }
                                        catch //если не целое число / не число
                                        {
                                            Console.WriteLine("Неверный формат хода" + i);
                                            ok = false;
                                        }
                                        break;

                                    default: //не обнаружено "X", "Y", "Z" в начале строки
                                        Console.WriteLine("Неверная ось передвижения" + i);
                                        ok = false;
                                        break;

                                }
                        }
                    }

                    if (ok) //если входные данные корректны
                    {
                        //создание выходного файла
                        using (FileStream fOut = new FileStream("OUTPUT.TXT", FileMode.Create)) { }
                        using (StreamWriter fOut = new StreamWriter("OUTPUT.TXT"))
                        {
                            //запись результата в выходной файл
                            fOut.Write(MinWay(x, y, z, d).ToString());
                        }

                        Console.WriteLine("Файл с ответом готов.");
                    }



                }
            }

        }


        //поиск кратчайшего пути до точки (0,0,0)
        static int MinWay(int x, int y, int z, int d)
        {
            //между осями Z и Y (боковые обасти) кратчайший путь = расстояние до оси X
            if ((z * y) >= 0)
                return Math.Abs(d);
            //нулевой уровень оси Х - ось Y, поэтому есть обасти, где x ходов - и есть кратчайший путь т.к. образуется равносторонний треугольник
            else if ((d * x) >= 0)
                return Math.Abs(d) + Math.Abs(x);
            else return Math.Abs(x);
        }


        //выбор дальнейшего действия
        static byte ChoiceOfActions()
        {
            Console.WriteLine("1 - продолжить работу");
            Console.WriteLine("2 - выйти из приложения");
            return InputByteNumber();
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
