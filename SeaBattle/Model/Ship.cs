using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle
{
    /// <summary>
    /// Класс корабль.
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// Размер корабля.
        /// </summary>
        private int _size;
        /// <summary>
        /// Живые палубы.
        /// </summary>
        private List<Deck> _aliveDecks = new List<Deck>();
        /// <summary>
        /// Убитые палубы.
        /// </summary>
        private List<Deck> _deadDecks = new List<Deck>();
        /// <summary>
        /// Ориентация корабля: горизонтальная или вертикальная.
        /// </summary>
        private ShipOrientation _orientation;
        /// <summary>
        /// Список точек, на которых располагаются палубы корабля.
        /// </summary>
        private List<Point> _points;
        /// <summary>
        /// Тип владельца корабля: пользователь или компьютер.
        /// </summary>
        private PlayerTypes _playerType = PlayerTypes.User;

#region Properties
        /// <summary>
        /// Размер корабля.
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
        /// <summary>
        /// Список живых палуб корабля.
        /// </summary>
        public List<Deck> AliveDecks
        {
            get { return _aliveDecks; }
            set { _aliveDecks = value; }
        }
        /// <summary>
        /// Список убитых палуб корабля.
        /// </summary>
        public List<Deck> DeadDecks
        {
            get { return _deadDecks; }
            set { _deadDecks = value; }
        }
        /// <summary>
        /// Ориентация корабля.
        /// </summary>
        public ShipOrientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }
        /// <summary>
        /// Список точек, на которых располагаются палубы корабля.
        /// </summary>
        public List<Point> Points
        {
            get { return _points; }
            set { _points = value; }
        }
#endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parSize">Длина корабля</param>
        /// <param name="parOrientation">Ориентация корабля</param>
        /// <param name="parPosition">Позиция начальной точки корабля</param>
        /// <param name="parPlayerType">Тип игрока, владеющего кораблём</param>
        public Ship(int parSize, ShipOrientation parOrientation, Point parPosition, PlayerTypes parPlayerType = PlayerTypes.User)
        {
            _size = parSize;
            _orientation = parOrientation;
            _playerType = parPlayerType;
            AddPosition(parPosition);
        }

        /// <summary>
        /// Добавление корабля
        /// </summary>
        /// <param name="parPosition"></param>
        private void AddPosition(Point parPosition)
        {
            _points = new List<Point>();
            if (_orientation == ShipOrientation.Horizontal)
            {
                for (int i = parPosition.Y; i < parPosition.Y + Size; i++)
                {
                    if (i < 10)
                    {
                        Point point = new Point(parPosition.X, i);
                        _aliveDecks.Add(new Deck(point));

                        _points.Add(point);
                    }
                    else
                    {
                        Point point = new Point(parPosition.X,  i - Size);
                        _aliveDecks.Add(new Deck(point));

                        _points.Add(point);
                    }
                }
            }
            else
            {
                for (int i = parPosition.X; i < parPosition.X + Size; i++)
                {
                    if (i < 10)
                    {
                        Point point = new Point(i, parPosition.Y);
                        _aliveDecks.Add(new Deck(point));

                        _points.Add(point);
                    }
                    else
                    {
                        Point point = new Point(i - Size, parPosition.Y);
                        _aliveDecks.Add(new Deck(point));

                        _points.Add(point);
                    }
                }
            }

            _points = _points.OrderBy(cell => cell.X).ThenBy(cell => cell.Y).ToList();
        }

        /// <summary>
        /// Убитая палуба.
        /// </summary>
        /// <param name="parDeck">Экземпляр палубы</param>
        public void ShotedDeck(Deck parDeck)
        {
            _aliveDecks.Remove(parDeck);
            _deadDecks.Add(parDeck);
        }

        /// <summary>
        /// Вывод сообщения о корабле.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append("Длина:").Append(Size).AppendLine();
            message.Append("Ориентация:").
                Append(Orientation == ShipOrientation.Horizontal ? "горизонтальная" : "вертикальная").AppendLine();

            return message.ToString();
        }
    }
}
