using System;

namespace Задача_11
{
    class Program
    {
        //ввод данных для выбора дальнейшего действия : 1 - продолжить, 2 - выход
        static byte InputByteNumber(string action = "Выберите действие", string mistake = "Введите целое число от 1 до 3",
                                   byte left = 1, byte right = 3) //всего 2 действия на выбор
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


        //декодирование исходной строки s, шаг сдвига по алфавиту влево - n
        static string Decoded(string sBeg, int n)
        {
            string s = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string S = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            string sEnd = "";
            int step;         //для получения шага (он м.б. и отрицательным)

            //проход по всем символам исходной строки
            for (int i = 0; i < sBeg.Length; i++)
            {
                //маленькие русские буквы
                if (sBeg[i] >= 'a' && sBeg[i] <= 'я' || sBeg[i] == 'ё')
                {
                    step = n % 33;
                    int iBeg = s.IndexOf(sBeg[i]);
                    if (iBeg - step < 0)
                        step = step - 33;

                    sEnd += s[iBeg - step];
                }

                //большие русские буквы
                else if (sBeg[i] >= 'А' && sBeg[i] <= 'Я' || sBeg[i] == 'Ё')
                {
                    step = n % 33;
                    int iBeg = S.IndexOf(sBeg[i]);
                    if (iBeg - step < 0)
                        step = step - 33;

                    sEnd += S[iBeg - step];
                }

                //маленькие английские буквы
                else if (sBeg[i] >= 'a' && sBeg[i] <= 'z')
                {
                    step = n % 26;
                    if (sBeg[i] - step < 'a')
                        step = step - 26;

                    sEnd += (char)(sBeg[i] - step);
                }

                //большие английские буквы
                else if (sBeg[i] >= 'A' && sBeg[i] <= 'Z')
                {
                    step = n % 26;
                    if (sBeg[i] - step < 'A')
                        step = step - 26;

                    sEnd += (char)(sBeg[i] - step);
                }

                //цифры
                else if (sBeg[i] >= '0' && sBeg[i] <= '9')
                {
                    step = n % 10;
                    if (sBeg[i] - step < '0')
                        step = step - 10;

                    sEnd += (char)(sBeg[i] - step);
                }
                else
                    sEnd += sBeg[i];
            }


            return sEnd;
        }


        //кодирование исходной строки s, шаг сдвига по алфавиту вправо - n
        static string Coded (string sBeg, int n)
        {
            string s = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string S = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            string sEnd = "";
            int step;         //для получения шага (он м.б. и отрицательным)

            //проход по всем символам исходной строки
            for (int i = 0; i < sBeg.Length; i++)
            {
                //маленькие русские буквы
                if (sBeg[i] >= 'a' && sBeg[i] <= 'я' || sBeg[i] == 'ё')
                {
                    step = n % 33;
                    int iBeg = s.IndexOf(sBeg[i]);
                    if (iBeg + step > 32)
                        step = step - 33;

                    sEnd += s[iBeg + step];
                }

                //большие русские буквы
                else if (sBeg[i] >= 'А' && sBeg[i] <= 'Я' || sBeg[i] == 'Ё')
                {
                    step = n % 33;
                    int iBeg = S.IndexOf(sBeg[i]);
                    if (iBeg + step > 32)
                        step = step - 33;

                    sEnd += S[iBeg + step];
                }

                //маленькие английские буквы
                else if (sBeg[i] >= 'a' && sBeg[i] <= 'z')
                {
                    step = n % 26;
                    if (sBeg[i] + step > 'z')
                        step = step - 26;

                    sEnd += (char)(sBeg[i] + step);
                }

                //большие английские буквы
                else if (sBeg[i] >= 'A' && sBeg[i] <= 'Z')
                {
                    step = n % 26;
                    if (sBeg[i] + step > 'Z')
                        step = step - 26;

                    sEnd += (char)(sBeg[i] + step);
                }

                //цифры
                else if (sBeg[i] >= '0' && sBeg[i] <= '9')
                {
                    //чтобы не ходить по цифрам кругами
                    step = n % 10;
                    if (sBeg[i] + step > '9')
                        step = step - 10;

                    sEnd += (char)(sBeg[i] + step);
                }
                else
                    sEnd += sBeg[i];
            }


            return sEnd;
        }



        //выбор дальнейшего действия
        static byte ChoiceOfActions()
        {
            Console.WriteLine("\n1 - закодировать строку");
            Console.WriteLine("2 - расшифровать строку");
            Console.WriteLine("3 - выйти из приложения");
            return InputByteNumber();
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Программа кодирует введенную строку, изменяя все рус/англ буквы\nна буквы и цифры, слкедующие через n символов за ними в алфавите.");
            
            byte action = 0;
            //цикл работы приложения
            while ((action = ChoiceOfActions()) != 3)
            {
                Console.Clear();
                int n = InputByteNumber("Введите на сколько букв идёт сдвиг по алфафиту:\n  враво при кодировании\n  влево при декодировании\nВвод", "Нужно целое число от 1 до 32", 1, 32);
                switch (action)
                {
                    case 1: //кодирование
                        Console.WriteLine("\nВведите строку для кодировки:");
                        string s = Console.ReadLine();
                        s = Coded(s, n);
                        Console.WriteLine("\nРезультат кодировки:\n" + s);
                        break;

                    case 2: //декодирование
                        Console.WriteLine("\nВведите строку для декодировки:");
                        s = Console.ReadLine();
                        s = Decoded(s, n);
                        Console.WriteLine("\nРезультат декодировки:\n" + s);
                        break;
                }
            }
        }
    }
}
