using System;

namespace _4
{
    public class Drob
    {
        public int chisl; //числитель
        public int znam;  //знаменатель

        //конструктор с параметрами
        public Drob(int chislitel, int znamenatel)
        {
            chisl = chislitel;
            znam = znamenatel;
        }
        //конструктор без параметров (дробь = 1)
        public Drob()
        {
            chisl = 1;
            znam = 1;
        }


        //сокращение дроби
        public static void DecDrob(ref Drob dr)
        {
            if (dr.chisl != 0)
            {
                bool ok = true;
                while (ok)
                {
                    ok = false;
                    for (int i = 2; i <= dr.chisl; i++)
                        if (dr.chisl % i == 0 && dr.znam % i == 0)
                        {
                            dr.chisl = dr.chisl / i;
                            dr.znam = dr.znam / i;
                            ok = true;
                            break;
                        }
                }
            }
        }


        //умножение дробей с последующим сокращением
        public static Drob operator *(Drob d1, Drob d2)
        {
            Drob d = new Drob();
            d.chisl = d1.chisl * d2.chisl;
            d.znam = d1.znam * d2.znam;
            DecDrob(ref d);
            return d;
        }


        //сложение дробей с последующим сокращением
        public static Drob operator +(Drob d1, Drob d2)
        {
            Drob d = new Drob();
            d.chisl = d1.chisl * d2.znam + d2.chisl * d1.znam;
            d.znam = d1.znam * d2.znam;
            DecDrob(ref d);
            return d;
        }


        //вычисление выражения ао методу горнера
        public static Drob Calculated(Drob[] mas)
        {
            Drob res = new Drob();
            res.chisl = mas[7].chisl;
            res.znam = mas[7].znam;
            for (int i = 6; i >= 0; i--)
                res = res * mas[8] + mas[i];
            return res;
        }


        //преобразование дроби к строке для вывода
        override public string ToString()
        {
            if (this.chisl % this.znam == 0)
                return (this.chisl / this.znam).ToString();
            else
                return this.chisl.ToString() + "/" + this.znam.ToString();
        }

    }


    class Program
    {
        //ввод целого числа из отрезка от left до right
        static int InputIntNumber(string action = "Введите целое число", string mistake = "Нужно целое число",
                                  int left = -2147483648, int right = 2147483647, bool znam = false) //границы целого типа int
        {
            Console.Write(action + ": ");
            int z;
            string input;
            do
            {
                input = Console.ReadLine();
                if (!int.TryParse(input, out z) || z < left || z > right || znam && z == 0)
                    Console.Write("   " + mistake + ": ");
            } while (!int.TryParse(input, out z) || z < left || z > right || znam && z == 0); //пока юзер не введёт нужное число
            return z;
        }






        //1 итерация программы
        public static void Actions()
        {
            //ввод значений
            Console.WriteLine("Чтобы вычислить многочлен введите 9 дробей (n - числитель, d - знаменатель)");
            Drob[] mas = new Drob[9]; // 8 дробей n/d и дробь a/b
            for (int i = 0; i < 8; i++)
                mas[i] = new Drob(InputIntNumber("\nВведите n" + i), InputIntNumber("Введите d" + i, "Нужно целое ненулевое значение", znam: true));
            mas[8] = new Drob(InputIntNumber("\nВведите a"), InputIntNumber("Введите b", "Нужно целое ненулевое значение", znam: true));

            //сокращение дробей
            for (int i = 0; i < 9; i++)
                Drob.DecDrob(ref mas[i]);

            //вычисление выражения и вывод ответа
            Console.WriteLine("\nРезультат вычисления: " + Drob.Calculated(mas).ToString());
        }





        //выбор дальнейшего действия
        static int ChoiceOfActions()
        {
            Console.WriteLine("\n1 - вычислить следующее выражение");
            Console.WriteLine("2 - выйти из приложения");
            return InputIntNumber("Выберите действтвие", "Введите либо 1, либо 2", 1, 2);
        }


        static void Main(string[] args)
        {
            //цикл работы приложения
            do
            {
                Console.Clear();
                Console.WriteLine("Программа вычисляет значение выражения (n0/d0) + (n1/d1)*(a/b)^1 + (n2/d2)^(a/b)^2 + ... + (n7/d7)^(a/b)^7 методом Горнера.\n");
                Actions();
            } while (ChoiceOfActions() == 1);
        }
    }
}
