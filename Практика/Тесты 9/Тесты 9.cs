using Microsoft.VisualStudio.TestTools.UnitTesting;
using _9;
using System.Runtime.InteropServices.ComTypes;

namespace Тесты_9
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //класс Elem<T>
            //конструктор по умолчанию
            Elem<int> p = new Elem<int>();
            Assert.IsTrue(p.Next == null && p.Data == 0);

            //конструктор с параметром
            p = new Elem<int>(6);
            Assert.IsTrue(p.Next == null && p.Data == 6);

            //прелюразование к строке
            Assert.IsTrue(p.ToString() == "6");
            //##############################################

            //класс List<T>
            //конструктор по умолчанию
            List<int> s = new List<int>();
            Assert.IsTrue(s.Beg == null);
            s.Delete(3, 5);
            Assert.IsTrue(s.ToString() == "Список пустой");
            Assert.IsTrue(s.Beg == null);
            //добавление элементов при пустом списке
            s.AddElems(1, 2, 3, 4, 5, 6);
            p = s.Beg;
            int i = 0;
            bool ok = true;
            while (p != null)
            {
                i++;
                if (p.Data != i)
                    ok = false;
                p = p.Next;
            }
            Assert.IsTrue(s.LengthNotRecursia == 6 && ok);

            //конструктор с размером списка
            s = new List<int>(5);
            Assert.IsTrue(s.LengthNotRecursia == 5);
            s = new List<int>(-7);
            Assert.IsTrue(s.LengthNotRecursia == 0);

            //конструктор с элементами
            s = new List<int>(1, 2, 3, 4, 5);
            p = s.Beg;
            i = 0;
            ok = true;
            while (p != null)
            {
                i++;
                if (p.Data != i)
                    ok = false;
                p = p.Next;
            }
            Assert.IsTrue(s.LengthNotRecursia == 5 && ok);

            //добавить элементы
            s.AddElems(6, 7, 8, 9, 10);
            p = s.Beg;
            i = 0;
            ok = true;
            while (p != null)
            {
                i++;
                if (p.Data != i)
                    ok = false;
                p = p.Next;
            }
            Assert.IsTrue(s.LengthNotRecursia == 10 && ok);

            //удаление элементов
            s.Delete(5, 10);
            Assert.IsTrue(s.LengthNotRecursia == 5);

            //вычисление длины списка
            s = new List<int>(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.IsTrue(s.LengthNotRecursia == List<int>.LengthRecursia(s.Beg));

            Assert.IsTrue(s.ToString() == "1 2 3 4 5 6 7 8 ");

        }
    }
}
