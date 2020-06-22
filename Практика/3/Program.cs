using System;

namespace _3
{
    class Program
    {

        /* ввод координат типа double
         * на вход строка описывающая необходимые требования
         * на выход число типа double */
        static double InputNumber(string action, string mistake = "Нужно число от +-5,0 * 10^(-324) до +-1,7 * 10^(308)") //границы типа double
        {
            Console.Write("   Введите координату " + action + ": ");
            double z;
            string input;
            do
            {
                input = Console.ReadLine();
                if (!double.TryParse(input, out z))
                    Console.Write("      " + mistake + ": ");
            } while (!double.TryParse(input, out z)); //пока юзер не введёт нужное число
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


        /* проверка введенных координат на принадлежность области
         * принадлежит - на выход true, нет - false */
        static bool CheckPoint(double x, double y)
        {
            //область можно поделить на 2 составляющие (y >= 1) и (|x| <= y)
            if (y >= 1 || y >= Math.Abs(x))
                return true;
            else
                return false;
        }


        //1 итерация программы
        static void Actions()
        {
            //ввод координат
            Console.WriteLine("Введите координаты точки для проверки на принадлежность области:");
            double x = InputNumber("X");
            double y = InputNumber("Y");

            //проверка на принадлежность точки обасти
            bool res = CheckPoint(x, y);

            //вывод результата
            if (res)
                Console.WriteLine("\nТочка входит в заданнай диапозон.\n");
            else
                Console.WriteLine("\nТочка НЕ входит в заданнай диапозон.\n");
        }


        //выбор дальнейшего действия
        static byte ChoiceOfActions()
        {
            Console.WriteLine("1 - продолжить проверку точек");
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
