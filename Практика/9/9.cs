using Microsoft.VisualBasic;
using System;

namespace _9
{
    //класс элемента списка
    public class Elem<T>
    {
        public T Data { get; set; }
        public Elem<T> Next { get; set; }

        //конструктор по умолчанию
        public Elem()
        {
            Next = null;
            Data = default(T);
        }

        //конструктор создающий новый элемент с данными
        public Elem(T data)
        {
            Data = data;
            Next = null;
        }

        //приведение элемента к строке
        public override string ToString()
        {
            return Data.ToString();
        }
    }


    //список элементов
    public class List<T>
    {
        //хранение первого элемента списка
        public Elem<T> Beg { get; set; }

        //конструктор списка по умолчанию
        public List()
        {
            Beg = null;
        }


        //создание списка длины size
        public List(int size)
        {
            if (size <= 0)
                Beg = null;
            else
            {
                Beg = new Elem<T>();
                Elem<T> p = Beg;
                for (int i = 1; i < size; i++)
                {
                    Elem<T> tmp = new Elem<T>();
                    tmp.Next = p;
                    p = tmp;

                }
            }
        }


        //заполнение списка с неопред кол-вом параметров
        public List(params T[] mas)
        {
            Beg = new Elem<T>();
            Beg.Data = mas[0];
            Elem<T> p = Beg;
            for (int i = 1; i < mas.Length; i++)
            {
                Elem<T> tmp = new Elem<T>();
                tmp.Data = mas[i];
                p.Next = tmp;
                p = tmp;
            }
        }


        //добавление неопред кол-ва элементов
        public void AddElems(params T[] mas)
        {
            int start = 0;
            if (Beg == null)
            {
                Beg.Data = mas[0];
                Beg.Next = null;
                start++;
            }
            Elem<T> p = Beg;
            while (!(p.Next == null))
                p = p.Next;

            for (int i = start; i < mas.Length; i++)
            {
                Elem<T> tmp = new Elem<T>();
                tmp.Data = mas[i];
                p.Next = tmp;
                p = tmp;
            }
        }


        //удлание элементов из списка
        public void Delete(int from, int howMany)
        {
            if (Beg != null)
            {
                Elem<T> p = Beg;
                for (int i = 1; i <= from - 1; i++)
                    if (p != null)
                        p = p.Next;

                Elem<T> pFrom = p;

                for (int i = 1; i <= howMany; i++)
                    if (p != null)
                        p = p.Next;

                pFrom.Next = p;
            }
        }


        //длина списка не рекрсией
        public int LengthNotRecursia
        {
            get
            {
                if (Beg == null) return 0;
                int len = 0;
                Elem<T> p = Beg;
                while (p != null)
                {
                    p = p.Next;
                    len++;
                }
                return len;

            }
        }

        //длина списка рекурсией
        public static int LengthRecursia(Elem<T> tmp, int lenUpToNow = 1)
        {
            if (tmp.Next == null)
                return lenUpToNow;
            else
                return LengthRecursia(tmp.Next, lenUpToNow + 1);
        }


        public override string ToString()
        {
            string s = "";
            Elem<T> p = Beg;
            if (p == null)
                s = "Список пустой";
            else
                while (p != null)
                {
                    s = s + p.ToString() + " ";
                    p = p.Next;
                }
            return s;
        }

    }


    class Program
    {
        //ввод данных для выбора дальнейшего действия : 1 - продолжить, 2 - выход
        static byte InputByteNumber(string action = "Выберите действие", string mistake = "Введите целое число от 1 до 6",
                                   byte left = 1, byte right = 6) //всего 2 действия на выбор
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



        //выбор дальнейшего действия
        static byte ChoiceOfActions()
        {
            Console.WriteLine("\n1 - создать новый список");
            Console.WriteLine("2 - добавить элементы в список");
            Console.WriteLine("3 - удалить элементы из списка");
            Console.WriteLine("4 - количество элементов списка рекурсией");
            Console.WriteLine("5 - количество элементов спика не рекурсией");
            Console.WriteLine("6 - выйти из приложения");
            return InputByteNumber();
        }


        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            byte action = 1;
            //цикл работы приложения
            do
            {
                switch (action)
                {
                    case 1:
                        Console.WriteLine("\nВведите элементы списка, разделенные пробелами");
                        string s = Console.ReadLine();
                        if (s != "")
                            list = new List<string>(s.Split(" "));
                        else
                            list = new List<string>();
                        break;

                    case 2:
                        Console.WriteLine("\nВведите новые элементы списка, разделенные пробелами");
                        s = Console.ReadLine();
                        if (s != "")
                            list.AddElems(s.Split(" "));
                        break;

                    case 3:
                        if (list.LengthNotRecursia != 0)
                        {
                            Console.WriteLine("\nНумерация элементов начинается с 0");
                            int from = InputByteNumber("Удалить с элемента номер", "Введите целое число от 0 до " + list.LengthNotRecursia, 0, (byte)list.LengthNotRecursia);
                            int howMany = InputByteNumber("Удалить элементов", "Введите целое число от 1 до " + (list.LengthNotRecursia - from + 1), 1, (byte)(list.LengthNotRecursia - from + 1));
                            list.Delete(from, howMany);
                        }
                        else
                            Console.WriteLine("\nУдалять нечего");
                        Console.WriteLine("\nНажмите enter для продолжения");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("\nВ списке элементов " + list.LengthNotRecursia);
                        Console.WriteLine("\nНажмите enter для продолжения");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("\nВ списке элементов " + List<string>.LengthRecursia(list.Beg));
                        Console.WriteLine("\nНажмите enter для продолжения");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
                Console.WriteLine(list.ToString());
                action = ChoiceOfActions();
            } while (action != 6);
        }
    }
}
