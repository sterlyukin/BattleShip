using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    /// <summary>
    /// Класс модель.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Кол-во однопалубных кораблей.
        /// </summary>
        private const int ONE_DECK_SHIPS_AMOUNT = 4;
        /// <summary>
        /// Кол-во двухпалубных кораблей.
        /// </summary>
        private const int TWO_DECK_SHIPS_AMOUNT = 3;
        /// <summary>
        /// Кол-во трёхпалубных кораблей.
        /// </summary>
        private const int THREE_DECK_SHIPS_AMOUNT = 2;
        /// <summary>
        /// Кол-во четырёхпалубных кораблей.
        /// </summary>
        private const int FOUR_DECK_SHIPS_AMOUNT = 1;

        /// <summary>
        /// Модель.
        /// </summary>
        private static Model _instance = new Model();
        /// <summary>
        /// Список кораблей игрока.
        /// </summary>
        private List<Ship> _playerShips = new List<Ship>();
        /// <summary>
        /// Список кораблей компьютера.
        /// </summary>
        private List<Ship> _computerShips = new List<Ship>();

        /// <summary>
        /// Список убитых кораблей компьютера при выстреле игрока.
        /// </summary>
        private List<Ship> _computerDeadShips = new List<Ship>();
        /// <summary>
        /// Список пробитых клеток компьютера при выстреле игрока.
        /// </summary>
        private List<Deck> _missedCellsByUser = new List<Deck>();
        /// <summary>
        /// Список пробитых клеток игрока при выстреле компьютера.
        /// </summary>
        private List<Deck> _missedCellsByComputer = new List<Deck>();

        /// <summary>
        /// Координаты позиций палуб, проставленных компьютером.
        /// </summary>
        private List<Point> _computerDeckPositions = new List<Point>();
        /// <summary>
        /// Свободные позиции для палуб.
        /// </summary>
        private List<Point> _freePositions = new List<Point>();
        
        /// <summary>
        /// Все позиции поля игрока.
        /// </summary>
        private List<Point> _allPositions = new List<Point>();

        /// <summary>
        /// Клетки поля игрока, в которые уже стреляли.
        /// </summary>
        private List<Point> _shootedPositions = new List<Point>();
        /// <summary>
        /// Позиции палуб.
        /// </summary>
        public List<Point> DeckPositions
        {
            get { return _computerDeckPositions; }
        }
        /// <summary>
        /// Свободные позиции.
        /// </summary>
        public List<Point> FreePositions
        {
            get { return _freePositions; }
        }
        /// <summary>
        /// Все позиции.
        /// </summary>
        public List<Point> AllPositions
        {
            get { return _allPositions; }
        }
        /// <summary>
        /// Клетки, в которые уже стреляли.
        /// </summary>
        public List<Point> ShootedPositions
        {
            get { return _shootedPositions; }
        }

        /// <summary>
        /// Список кораблей игрока.
        /// </summary>
        public List<Ship> PlayerShips
        {
            get { return _playerShips; }
        }

        /// <summary>
        /// Список кораблей компьютера.
        /// </summary>
        public List<Ship> ComputerShips
        {
            get { return _computerShips; }
        }

        /// <summary>
        /// Список убитых кораблей компьютера.
        /// </summary>
        public List<Ship> ComputerDeadShips
        {
            get { return _computerDeadShips; }
        }

        /// <summary>
        /// Список убитых палуб компьютера при выстреле игрока.
        /// </summary>
        public List<Deck> MissedCellsByUser
        {
            get { return _missedCellsByUser; }
        }

        public List<Deck> MissedCellsByComputer
        {
            get { return _missedCellsByComputer; }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        private Model()
        {
        }

        /// <summary>
        /// Инициализация экземпляра.
        /// </summary>
        public static Model Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Проверка наличия всех необходимых кораблей игрока для готовности к игре.
        /// </summary>
        /// <param name="parShipLength"></param>
        /// <returns></returns>
        public bool CheckSetPossibilityByNumber(int parShipLength)
        {
            switch (parShipLength)
            {
                case (int)ShipLengthCategories.OneDeckShip:
                    if (this.CheckShipsAmount(parShipLength) == ONE_DECK_SHIPS_AMOUNT)
                    {
                        return false;
                    }
                    return true;
                case (int)ShipLengthCategories.TwoDeckShip:
                    if (this.CheckShipsAmount(parShipLength) == TWO_DECK_SHIPS_AMOUNT)
                    {
                        return false;
                    }
                    return true;
                case (int)ShipLengthCategories.ThreeDeckShip:
                    if (this.CheckShipsAmount(parShipLength) == THREE_DECK_SHIPS_AMOUNT)
                    {
                        return false;
                    }
                    return true;
                case (int)ShipLengthCategories.FourDeckShip:
                    if (this.CheckShipsAmount(parShipLength) == FOUR_DECK_SHIPS_AMOUNT)
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Проверка кол-ва кораблей определённой длины
        /// </summary>
        /// <param name="parShipLength"></param>
        /// <returns></returns>
        public int CheckShipsAmount(int parShipLength)
        {
            int count = 0;
            foreach (Ship ship in this.PlayerShips)
            {
                if (ship.Size == parShipLength)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Проверка возможности поставить корабль в определённой позиции.
        /// </summary>
        /// <param name="parCurrentShip"></param>
        /// <param name="parShips"></param>
        /// <returns></returns>
        public bool CheckSetPossibilityByPosition(Ship parCurrentShip, List<Ship> parShips)
        {
            foreach (Ship ship in parShips)
            {
                for (int shipCount = 0; shipCount < ship.Size; shipCount++)
                {
                    for (int currentShipCount = 0; currentShipCount < parCurrentShip.Size; currentShipCount++)
                    {
                        bool samePositionsCondition = ship.Points[shipCount].X == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y == parCurrentShip.Points[currentShipCount].Y;

                        bool leftPositionsCondition = ship.Points[0].X == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[0].Y - 1 == parCurrentShip.Points[currentShipCount].Y;

                        bool rightPositionsCondition = ship.Points[0].X == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[ship.Size - 1].Y + 1 == parCurrentShip.Points[currentShipCount].Y;

                        bool topPositionsCondition = ship.Points[shipCount].X - 1 == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y == parCurrentShip.Points[currentShipCount].Y;

                        bool bottomPositionsCondition = ship.Points[shipCount].X + 1 == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y == parCurrentShip.Points[currentShipCount].Y;

                        bool rightTopDiagonalPositionsCondition = ship.Points[shipCount].X - 1 == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y + 1 == parCurrentShip.Points[currentShipCount].Y;

                        bool leftTopDiagonalPositionsCondition = ship.Points[shipCount].X - 1 == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y - 1 == parCurrentShip.Points[currentShipCount].Y;

                        bool rightBottomDiagonalPositionsCondition = ship.Points[shipCount].X + 1 == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y + 1 == parCurrentShip.Points[currentShipCount].Y;

                        bool leftBottomDiagonalPositionsCondition = ship.Points[shipCount].X + 1 == parCurrentShip.Points[currentShipCount].X &&
                            ship.Points[shipCount].Y - 1 == parCurrentShip.Points[currentShipCount].Y;

                        bool allConditions = samePositionsCondition || leftPositionsCondition || rightPositionsCondition || topPositionsCondition
                            || bottomPositionsCondition || rightTopDiagonalPositionsCondition || leftTopDiagonalPositionsCondition
                            || rightBottomDiagonalPositionsCondition || leftBottomDiagonalPositionsCondition;

                        if (allConditions)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Выстрел в клетку.
        /// </summary>
        /// <param name="parDeck"></param>
        public void MissCell(Deck parDeck)
        {
            _missedCellsByUser.Add(parDeck);
        }

        /// <summary>
        /// Рандомное расставление кораблей.
        /// </summary>
        /// <param name="parShipLength">Количество палуб корабля</param>
        /// <param name="parShipsCount">Количество кораблей такого типа</param>
        public void Random(int parShipLength, int parShipsCount)
        {
            for (int i = 0; i < parShipsCount; i++)
            {
                Point point;
                Ship ship = null;
                do
                {
                    point = _freePositions.GetRandom();
                    Random rand = new Random(DateTime.Now.Millisecond);
                    switch((ShipOrientation)rand.Next(2))
                    {
                        case ShipOrientation.Horizontal:
                            ship = new Ship(parShipLength, ShipOrientation.Horizontal, point, PlayerTypes.Computer);
                            break;
                        case ShipOrientation.Vertical:
                            ship = new Ship(parShipLength, ShipOrientation.Vertical, point, PlayerTypes.Computer);
                            break;
                    }
                    rand = null;
                } while (_computerDeckPositions.Contains(point) || (!CheckSetPossibilityByPosition(ship, ComputerShips)));
                _computerDeckPositions.Add(point);
                _freePositions.Remove(point);
                ComputerShips.Add(ship);
            }
        }

        /// <summary>
        /// Рандомная генерация кораблей.
        /// </summary>
        public void CreateRandomShips()
        {
            Random(1, 4);
            Random(2, 3);
            Random(3, 2);
            Random(4, 1);
        }

        /// <summary>
        /// Генерация точки для выстрела компьютера по полю игрока.
        /// </summary>
        /// <returns></returns>
        public Point GenerateShootPoint()
        {
            Point point;
            do
            {
                point = _allPositions.GetRandom();

            } while (_shootedPositions.Contains(point));
            _shootedPositions.Add(point);
            _allPositions.Remove(point);

            return point;
        }

        /// <summary>
        /// Первоначальная инициализация пустых клеток поля.
        /// </summary>
        public void GetAllPositions()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _freePositions.Add(new Point(i, j));
                    _allPositions.Add(new Point(i, j ));
                }
            }
        }

        /// <summary>
        /// Обновление счёта пользователя.
        /// </summary>
        /// <param name="parRank">Попал или нет.</param>
        /// <returns></returns>
        public int UpdateScore(bool parRank, Player parPlayer)
        {
            if(parRank)
            {
                parPlayer.Scores += 5;
                return parPlayer.Scores;
            }

            parPlayer.Scores -= 1;
            return parPlayer.Scores;
        }

        /// <summary>
        /// Изменение ориентации корабля.
        /// </summary>
        /// <param name="parShipOrientation">Ориентация корабля</param>
        /// <returns></returns>
        public ShipOrientation ChangeShipOrientation(ShipOrientation parShipOrientation)
        {
            if (parShipOrientation == ShipOrientation.Horizontal)
            {
                return ShipOrientation.Vertical;
            }
            return ShipOrientation.Horizontal;
        }

        /// <summary>
        /// Вывод сообщения при постановке корабля.
        /// </summary>
        /// <param name="parShip"></param>
        /// <param name="parPlayerType"></param>
        /// <returns></returns>
        public string ShowMessageOnShipSet(Ship parShip, PlayerTypes parPlayerType)
        {
            string message = "";
            if (parPlayerType == PlayerTypes.User)
            {
                StringBuilder userMessage = new StringBuilder();
                userMessage.Append("Игрок поставил корабль:").AppendLine();
                userMessage.Append(parShip.ToString());

                message = userMessage.ToString();
            }

            return message;
        }
    }
}
