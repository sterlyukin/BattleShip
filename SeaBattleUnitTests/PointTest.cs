using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle;

namespace SeaBattleUnitTests
{
    /// <summary>
    /// Класс, тестирующий Point.
    /// </summary>
    [TestClass]
    public class PointTest
    {
        /// <summary>
        /// Экземпляр точки.
        /// </summary>
        Point _point;

        /// <summary>
        /// Проверка присваивания корректного значения X.
        /// </summary>
        [TestMethod]
        public void XCorrectValueTest()
        {
            int expected = 2;
            _point = new Point(2, 4);

            Assert.AreEqual(expected, _point.X);
        }

        /// <summary>
        /// Проверка присваивания корректного значения Y.
        /// </summary>
        [TestMethod]
        public void YCorrectValueTest()
        {
            int expected = 7;
            _point = new Point(3, 7);

            Assert.AreEqual(expected, _point.Y);
        }

        /// <summary>
        /// Проверка присваивания некорректного значения X.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XNegativeValueTest()
        {
            _point = new Point(-3, 4);
        }

        /// <summary>
        /// Проверка присваивания некорректного значения Y.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void YNegativeValueTest()
        {
            _point = new Point(1, -4);
        }

        /// <summary>
        /// Проверка равенства двух точек с помощью оператора ==.
        /// </summary>
        [TestMethod]
        public void EqualityOperatorPositiveTest()
        {
            Point pointA = new Point(2, 2);
            Point pointB = new Point(2, 2);

            bool actual = pointA == pointB;

            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// Проверка неравенства двух точек с помощью оператора ==.
        /// </summary>
        [TestMethod]
        public void EqualityOperatorNegativeTest()
        {
            Point pointA = new Point(2, 2);
            Point pointB = new Point(2, 1);

            bool actual = pointA == pointB;

            Assert.AreEqual(false, actual);
        }

        /// <summary>
        /// Проверка равенства двух точек с помощью Equals.
        /// </summary>
        [TestMethod]
        public void EqualsPositiveTest()
        {
            Point pointA = new Point(4, 4);
            Point pointB = new Point(4, 4);

            bool actual = pointA.Equals(pointB);

            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// Проверка неравенства двух точек с помощью Equals.
        /// </summary>
        [TestMethod]
        public void EqualsNegativeTest()
        {
            Point pointA = new Point(4, 4);
            Point pointB = new Point(3, 4);

            bool actual = pointA.Equals(pointB);

            Assert.AreEqual(false, actual);
        }

        /// <summary>
        /// Проверка равенства хэш-кода двух одинаковых точек.
        /// </summary>
        [TestMethod]
        public void HashCodePositiveTest()
        {
            Point pointA = new Point(3, 3);
            Point pointB = new Point(3, 3);

            Assert.AreEqual(pointA.GetHashCode(), pointB.GetHashCode());
        }

        /// <summary>
        /// Проверка неравенства хэш-кода двух разных точек.
        /// </summary>
        [TestMethod]
        public void HashCodeNegativeTest()
        {
            Point pointA = new Point(3, 3);
            Point pointB = new Point(4, 3);

            Assert.AreNotEqual(pointA.GetHashCode(), pointB.GetHashCode());
        }
    }
}
