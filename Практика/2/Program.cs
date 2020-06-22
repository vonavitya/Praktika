using System;
using System.IO;

namespace _2
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

        static void Actions()
        {
            using (FileStream f1 = new FileStream("INPUT.TXT", FileMode.OpenOrCreate)) { }


            for (int r = 0; r < 1; r++)
            {
                string mas;
                int k; //максимально возможный сдвиг
                string s = "";

                using (StreamReader f1 = new StreamReader("INPUT.TXT"))
                {
                    if (f1.Peek() == -1)
                    {
                        Console.WriteLine("Файл пустой!");
                        break;
                    }
                    if (!int.TryParse(f1.ReadLine(), out k) || k < 0)
                    {
                        Console.WriteLine("Неверный формат K!");
                        break;
                    }
                    if (f1.Peek() != -1)
                    {
                        mas = f1.ReadLine();
                    }
                    else
                    {
                        mas = "";
                    }
                }

                int tek;
                int n = mas.Length;
                if (n > 100000 || n < k)
                {
                    Console.WriteLine("Строка символов не д.б. длиннее 100000 символов и короче K!");
                    break;
                }

                bool[] ok = new bool[n];

                s = "";

                if (n != 0)
                {
                    for (tek = 0; tek < k; tek++)
                    {
                        int iMin = -1;
                        char min = '~';

                        for (int i = 0; (i <= k + tek) && (i < n); i++)
                            if (mas[i] < min && ok[i] == false)
                            {
                                iMin = i;
                                min = mas[i];
                            }

                        ok[iMin] = true;
                        s = s + min;
                    }

                    for (tek = tek; tek < n - k; tek++)
                    {
                        if (ok[tek - k] == false)
                        {
                            s = s + mas[tek - k];
                            ok[tek - k] = true;
                        }
                        else
                        {
                            int iMin = -1;
                            char min = '~';

                            for (int i = tek - k; i <= tek + k; i++)
                                if (mas[i] < min && ok[i] == false)
                                {
                                    iMin = i;
                                    min = mas[i];
                                }

                            ok[iMin] = true;
                            s = s + min;
                        }
                    }

                    for (tek = tek; tek < n; tek++)
                    {
                        if (ok[tek - k] == false)
                        {
                            s = s + mas[tek - k];
                            ok[tek - k] = true;
                        }
                        else
                        {

                            int iMin = -1;
                            char min = '~';

                            for (int i = tek - k; i < n; i++)
                                if (mas[i] < min && ok[i] == false)
                                {
                                    iMin = i;
                                    min = mas[i];
                                }

                            ok[iMin] = true;
                            s = s + min;
                        }
                    }
                }


                using (FileStream f2 = new FileStream("OUTPUT.TXT", FileMode.Create)) { }
                using (StreamWriter f2 = new StreamWriter("OUTPUT.TXT"))
                {
                    f2.Write(s);
                }

                Console.WriteLine("Программа отработала по файлу INPUT.TXT. вы можете изменить\nего содержимое повторить работу программы с измененным файлом.\nНе забудьте сохранить результат, так как при новом запуске\nфайл OUTPUT.TXT будет изменён");


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
