namespace SeaBattle
{
    /// <summary>
    /// Класс контроллера игрока-компьютера.
    /// </summary>
    public class ComputerFormController : IComputerController
    {
        int computerSteps = 0;

        public int ComputerSteps
        {
            get { return computerSteps; }
        }

        /// <summary>
        /// Представление
        /// </summary>
        private GameView _gameView;
        /// <summary>
        /// Модель.
        /// </summary>
        private Model _model;
        /// <summary>
        /// Игрок.
        /// </summary>
        private Player _player;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ComputerFormController(GameView parGameView, Model parModel, Player parPlayer)
        {
            _gameView = parGameView;
            _model = parModel;
            _player = parPlayer;
            _model.GetAllPositions();
        }
        
        /// <summary>
        /// Отображение кораблей на поле компьютера
        /// </summary>
        public void ShowShips()
        {
            _model.CreateRandomShips();
            foreach (Ship ship in _model.ComputerShips)
            {
                _gameView.ShowComputerShip(ship);
            }
        }

        /// <summary>
        /// Выстрел компьютера по сгенерированной точке поля игрока.
        /// </summary>
        /// <returns></returns>
        public bool ShootByComputer()
        {
            Point point = _model.GenerateShootPoint();
            computerSteps++;

            return Shoot(point);
        }

        /// <summary>
        /// Проверка на попадание компьютера по кораблю игрока.
        /// </summary>
        /// <param name="parPoint"></param>
        /// <returns></returns>
        public bool Shoot(Point parPoint)
        {
            Deck shootDeck = new Deck(new Point(parPoint.X, parPoint.Y));
            foreach (Ship ship in _model.PlayerShips)
            {
                foreach (Deck currentDeck in ship.AliveDecks)
                {
                    if (shootDeck.Position == currentDeck.Position)
                    {
                        _gameView.ShowShootedUserDeck(currentDeck);
                        ship.ShotedDeck(currentDeck);
                        if (ship.AliveDecks.Count == 0)
                        {
                            _model.ComputerShips.Remove(ship);
                            _model.ComputerDeadShips.Add(ship);
                            _gameView.AddMessageToMessageBox("\n\nВыстрел компьютера:\nИнф-ия о корабле:" + ship.ToString());
                            _gameView.AddMessageToMessageBox("Статус: Корабль убит!\n");
                        }
                        else
                        {
                            _gameView.AddMessageToMessageBox("\n\nВыстрел компьютера:Корабль ранен!");
                        }
                        if (_model.ComputerShips.Count == 0)
                        {
                            GameFormController.RecordScore(_player);
                            _gameView.GameOver(_player, false);
                        }
                        return true;
                    }
                }
            }

            _model.MissedCellsByComputer.Add(shootDeck);
            _gameView.AddMessageToMessageBox("\n\nВыстрел компьютера:Промах!");
            _gameView.ShowMissedComputerShoot(new Point(shootDeck.Position.X, shootDeck.Position.Y));
            return false;
        }
    }
}
