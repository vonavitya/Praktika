using System;
using System.Runtime.InteropServices.ComTypes;

namespace _6
{
    class Program
    {
        /* ввод чисел типа double
         * на вход строка описывающая необходимые требования
         * на выход число типа double */
        static double InputNumber(string action, string mistake = "Нужно число от +-5,0 * 10^(-324) до +-1,7 * 10^(308)", bool needPlus = false) //границы типа double
        {
            Console.Write("Введите " + action + ": ");
            double z;
            string input;
            do
            {
                input = Console.ReadLine();
                if (!double.TryParse(input, out z) || (needPlus && z < 0))
                    Console.Write("   " + mistake + ": ");
            } while (!double.TryParse(input, out z) || (needPlus && z < 0)); //пока юзер не введёт нужное число
            return z;
        }


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


        //построение последовательности
        static void Posled(double M, double a1, double a2, ref double a3, ref int j, out bool ok)
        {
            double a4 = (1.5 * a3) - (2 * a2 / 3) - (a1 / 3);
            //сразу вывод, чтобы не хранить всю последовательность
            if (!double.IsNaN(a4))
            {
                a1 = a2; a2 = a3; a3 = a4;
                j++;
                Console.WriteLine(j + ": " + a3);
                ok = true;
                if (Math.Abs(a3) > M)
                {
                    //если условие не достигнуто - снова уход на рекурсию
                    Posled(M, a1, a2, ref a3, ref j, out ok);
                }
            }
            else
            {
                Console.WriteLine("Невозможно достичь выполнения условия |a(j)| <= M");
                ok = false;
            }

        }


        //1 итерация программы
        static void Actions()
        {
            //ввод данных
            double a1 = InputNumber("a1"),
                a2 = InputNumber("a2"),
                a3 = InputNumber("a3"),
                M = InputNumber("M", "Нужо число от 0 до +-1,7 * 10^(308)", needPlus: true),
                N = InputNumber("N");
            int j = 3;


            //вывод первых членов последовательности
            Console.WriteLine("\nПоследовательность:\n1: " + a1);
            Console.WriteLine("2: " + a2.ToString());
            Console.WriteLine("3: " + a3.ToString());
            bool ok;

            Posled(M, a1, a2, ref a3, ref j, out ok);
            //построение последовательности с помощью рекурсии
            if (ok)
            {
                //Проверка равенства a(j) = M
                if (Math.Abs(a3) == M)
                    Console.WriteLine(".\nРавенство a(j) = M выполняется");
                else
                    Console.WriteLine("\nРавенство a(j) = M НЕ выполняется");

                //сравнение j и N
                if (j == N)
                    Console.WriteLine("J = N");
                else
                    if (j > N)
                    Console.WriteLine("J = " + j + " > N");
                else
                    Console.WriteLine("J = " + j + " < N");
            }
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
                Actions();
            } while (ChoiceOfActions() == 1);
        }
    }
}
