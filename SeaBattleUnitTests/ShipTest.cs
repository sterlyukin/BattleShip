using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle;
using System.Collections.Generic;
using System.Text;

namespace SeaBattleUnitTests
{
    /// <summary>
    /// Класс, тестирующий Ship
    /// </summary>
    [TestClass]
    public class ShipTest
    {
        /// <summary>
        /// Экземпляр корабля.
        /// </summary>
        Ship _ship;

        /// <summary>
        /// Проверка корректного значения размера.
        /// </summary>
        [TestMethod]
        public void ShipSizeCorrectValueTest()
        {
            int expected = 1;
            _ship = new Ship(1, ShipOrientation.Horizontal, new Point(2, 2));

            Assert.AreEqual(expected, _ship.Size);
        }

        /// <summary>
        /// Проверка корректного значения ориентации корабля.
        /// </summary>
        [TestMethod]
        public void ShipOrientationCorrectValueTest()
        {
            _ship = new Ship(1, ShipOrientation.Horizontal, new Point(2, 2));

            Assert.AreEqual(ShipOrientation.Horizontal, _ship.Orientation);
        }

        /// <summary>
        /// Проверка корректного значения списка занимаемых точек корабля.
        /// </summary>
        [TestMethod]
        public void ShipPointsCorrectValueTest()
        {
            List<Point> expected = new List<Point>();
            expected.Add(new Point(2, 2));

            _ship = new Ship(1, ShipOrientation.Horizontal, new Point(2, 2));

            CollectionAssert.AreEqual(expected, _ship.Points);
        }

        /// <summary>
        /// Проверка корректного значения списка убитых палуб корабля.
        /// </summary>
        [TestMethod]
        public void DeadDecksCorrectValueTest()
        {
            Deck deck = new Deck(new Point(2, 2));

            _ship = new Ship(1, ShipOrientation.Horizontal, new Point(2, 2));

            _ship.ShotedDeck(deck);

            List<Deck> expected = new List<Deck>();
            expected.Add(deck);

            CollectionAssert.AreEqual(expected, _ship.DeadDecks);
        }

        /// <summary>
        /// Проверка корректного значения вывода информации о корабле.
        /// </summary>
        [TestMethod]
        public void ToStringCorrectValueTest()
        {
            _ship = new Ship(1, ShipOrientation.Horizontal, new Point(2, 2));

            StringBuilder expected = new StringBuilder();
            expected.Append("Длина:").Append(_ship.Size).AppendLine();
            expected.Append("Ориентация:").
                Append(_ship.Orientation == ShipOrientation.Horizontal ? "горизонтальная" : "вертикальная")
                .AppendLine();

            Assert.AreEqual(expected.ToString(), _ship.ToString());
        }
    }
}
