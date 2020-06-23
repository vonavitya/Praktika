using Microsoft.VisualStudio.TestTools.UnitTesting;
using _4;

namespace Тесты_4
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Class_Drob()
        {
            //конструкторы
            Drob d1 = new Drob();
            Assert.IsTrue(d1.chisl == d1.znam && d1.znam == 1);

            d1 = new Drob(1, 2);
            Assert.IsNotNull(d1);

            //сокращение дроби
            d1.chisl = 0;
            d1.znam = 5;
            Drob.DecDrob(ref d1);
            Assert.IsTrue(d1.chisl == 0 && d1.znam == 5);

            d1.chisl = 50;
            d1.znam = 5;
            Drob.DecDrob(ref d1);
            Assert.IsTrue(d1.chisl == 10 && d1.znam == 1);

            d1.chisl = 1;
            d1.znam = 5;
            Drob.DecDrob(ref d1);
            Assert.IsTrue(d1.chisl == 1 && d1.znam == 5);

            //умножение дробей
            Drob d2 = new Drob(2, 10);
            d1 = d1 * d2;
            Assert.IsTrue(d1.chisl == 1 && d1.znam == 25);

            //сложение дробей
            d1 = d1 + d2;
            Assert.IsTrue(d1.chisl == 6 && d1.znam == 25);

            //приведение к строке
            string s = d1.ToString();
            Assert.IsTrue(s == d1.chisl + "/" + d1.znam);

            d1.chisl = 10;
            d1.znam = 2;
            s = d1.ToString();
            Assert.IsTrue(s == (d1.chisl / d1.znam).ToString());

            //вычисление выражения в задании
            Drob[] mas = new Drob[9];
            mas[0] = new Drob(1, 3);
            mas[1] = new Drob(1, 3);
            mas[2] = new Drob(1, 3);
            mas[3] = new Drob(1, 3);
            mas[4] = new Drob(1, 3);
            mas[5] = new Drob(1, 3);
            mas[6] = new Drob(1, 3);
            mas[7] = new Drob(1, 3);
            mas[8] = new Drob(1, 2);
            Drob d = Drob.Calculated(mas);
            Assert.IsTrue(d.chisl == 85 && d.znam == 128);
        }
    }
}
