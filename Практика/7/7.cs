using System;
using System.Globalization;

namespace _7
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


        //считывание булевой функции с проверкой на корректность
        static byte[] ReadFunct(out byte nVar /*кол-во переменных*/, out int[] empt /*места пропусков*/)
        {
            byte[] f;          //вектор, заающий функцию
            bool okF = true;   //проверка введённого вектора на корректность
            int nEmpt = 0;     //количество пропусков

            Console.WriteLine("Введите булеву функцию, заданную вектором,\nна месте пропуска поставьте одну звёздочку:");
            do
            {
                //считывание вектора
                string s = Console.ReadLine();

                //создание массивов для хранения данных
                f = new byte[s.Length];
                empt = new int[s.Length];

                //вычисление кол-ва переменных
                nVar = Degree2(s.Length);
                //если 0 - то длина вектора не является степенью двойки
                if (nVar == 0)
                    okF = false;

                //если длина вектора = степень двойки
                else
                    //преобразование вектора в массив 0 / 1 / *(тут тоже 0 т к неважно что тут)
                    for (int tekF = 0; tekF < s.Length; tekF++)
                        //если введён неправильный символ
                        if (!(s[tekF] == '0' || s[tekF] == '1' || s[tekF] == '*'))
                            okF = false;
                        //если введены 0/1/*
                        else
                        {
                            //если введена звездочка то запись места звездочки в массив пустот
                            if (s[tekF] == '*')
                            {
                                empt[nEmpt] = tekF;
                                nEmpt++;
                            }
                            else
                                f[tekF] = Convert.ToByte(s[tekF].ToString());
                        }

                if (!okF)
                    Console.Write("   Некорректная функция. Повторите попытку:\n   ");

            } while (!okF);

            //массив был создан с расчетом то то что все места пустые
            Array.Resize(ref empt, nEmpt);

            return f;
        }


        //проверка на степень двойки
        static byte Degree2(int number)
        {
            if (number == 2)
                return 1;

            else if (number % 2 == 0)
                return (byte)(Degree2(number / 2) + 1);

            else //число не является степенью двойки
                return 0;
        }


        //подстановка в вектор f дополнения номер tekAdd, переведённого в двоичную систему
        static void AddEmptInFunct(ref byte[] f, int[] empt, int tekAdd)
        {
            for (int tekEmpt = empt.Length - 1; tekEmpt >= 0; tekEmpt--)
            {
                f[empt[tekEmpt]] = (byte)(tekAdd % 2);
                tekAdd = tekAdd / 2;
            }
        }


        //определение, линейна ли функций (полином жегалкина строится методом треугольника
        static bool FIsLine(byte[] f, byte nVar)
        {
            //треугольник (строки массива = столбцы треугольника)
            //первый элемент каждой строки = очередной к-нт в полиноме жегалкина
            byte[][] koef = new byte[f.Length][];
            koef[0] = f;

            //заполнение треугольника
            for (int j = 1; j < f.Length; j++)
            {
                koef[j] = new byte[f.Length - j];

                for (int i = 0; i < koef[j].Length; i++)
                {
                    if (koef[j - 1][i] != koef[j - 1][i + 1])
                        koef[j][i] = 1;
                }
            }

            //проверка на линейность
            for (int j = nVar + 1; j < f.Length; j++)
            {
                if (koef[j][0] == 1)
                    return false;
            }

            return true;
        }


        //1 итерация программы
        static void Actions()
        {
            Console.WriteLine("Программа доопределит булеву функцию так, чтобы она была линейной.\n");
            byte nVar;       //количество переменных
            int[] empt;      //места, где пусто
            byte[] f = ReadFunct(out nVar, out empt);

            int nAdd = 0;    //количество возможных доопределений

            if (empt.Length > 0)
            {
                Console.Write("\nДоопределения:");
                //проход по всем доопределениям (по всех их номерам)
                for (int tekAdd = 0; tekAdd < Math.Pow(2, empt.Length); tekAdd++)
                {
                    //доопределение функции
                    AddEmptInFunct(ref f, empt, tekAdd);
                    //если функция линейна
                    if (!FIsLine(f, nVar))
                    {
                        nAdd++;
                        //вывод доопределённой функции
                        Console.Write("\n" + nAdd + ": ");
                        for (int tekF = 0; tekF < f.Length; tekF++)
                            Console.Write(f[tekF]);
                    }
                }
            }

            if (nAdd == 0)
                Console.WriteLine("\nНет возможных доопределений!");
        }


        //выбор дальнейшего действия
        static byte ChoiceOfActions()
        {
            Console.WriteLine("\n\n1 - продолжить работу");
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
