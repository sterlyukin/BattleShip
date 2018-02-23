using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SeaBattle
{
    /// <summary>
    /// Класс контроллера игрока-пользователя.
    /// </summary>
    public class GameFormController : IGameController
    {
        Stopwatch stopwatch = new Stopwatch();

        int allClicks = 0;

        /// <summary>
        /// Модель.
        /// </summary>
        private Model _model;
        /// <summary>
        /// Представление игры.
        /// </summary>
        private GameView _gameView;
        /// <summary>
        /// Экземпляр контроллера игрока-компьютера.
        /// </summary>
        private IComputerController _computerController;

        /// <summary>
        /// Длина корабля.
        /// </summary>
        private int _shipLength = 1;
        /// <summary>
        /// Ориентация корабля.
        /// </summary>
        private ShipOrientation _shipOrientation = ShipOrientation.Horizontal;

        /// <summary>
        /// Определяется стреляет ли следующий пользователь.
        /// </summary>
        private bool _nextShootUser = true;

        /// <summary>
        /// Игрок.
        /// </summary>
        private Player _player;

        /// <summary>
        /// Модель.
        /// </summary>
        public Model Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public GameFormController(GameView parGameView, Player parPlayer)
        {
            _gameView = parGameView;
            _player = parPlayer;
            _model = Model.Instance;
            _gameView.OnSetShip += new EventHandler(SetShip);
            _gameView.OnMoveShip += new EventHandler(MouseMove);
            _gameView.OnLeaveShip += new EventHandler(MouseLeave);

            _gameView.OnSelectOneDeckShip += new EventHandler(SelectOneDeckShip);
            _gameView.OnSelectTwoDeckShip += new EventHandler(SelectTwoDeckShip);
            _gameView.OnSelectThreeDeckShip += new EventHandler(SelectThreeDeckShip);
            _gameView.OnSelectFourDeckShip += new EventHandler(SelectFourDeckShip);

            _gameView.OnChangeShipOrientation += new EventHandler(ChangeShipOrientation);
            _gameView.OnStartGame += new EventHandler(CheckPlayerReady);
            _gameView.OnShoot += new EventHandler(GenerateShootPoint);
            _gameView.OnShoot += new EventHandler(ComputerShot);

            _gameView.OnGameOver += new EventHandler(GameOver);
            //TestTimer();

            InitComputerController(_gameView, _model);
            _gameView.InitGameView();
        }

        public void TestTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.Start();
            timer.Tick += new EventHandler(Test);
        }

        public void Test(object sender, EventArgs e)
        {
            _gameView.Close();
        }

        private int steps = 0;
        public void TestSteps()
        {
            steps++;
        }

        /// <summary>
        /// Инициализация контроллера игрока-компьютера
        /// </summary>
        /// <param name="parGameView"></param>
        /// <param name="parModel"></param>
        public void InitComputerController(GameView parGameView, Model parModel)
        {
            _computerController = new ComputerFormController(parGameView, parModel, _player);
        }

        /// <summary>
        /// Добавление корабля на поле игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetShip(object sender, EventArgs k)
        {
            allClicks++;
            DataGridViewCellMouseEventArgs e = (DataGridViewCellMouseEventArgs)k;
            Ship ship = new Ship(_shipLength, _shipOrientation, new Point(e.RowIndex, e.ColumnIndex));
            if (!_model.CheckSetPossibilityByPosition(ship, _model.PlayerShips))
            {
                _gameView.ShowMessage("Сообщение", "Данная позиция недоступна для постановки корабля");
                return;
            }
            if (!_model.CheckSetPossibilityByNumber(_shipLength))
            {
                _gameView.ShowMessage("Сообщение", "Корабли данного типа закончились");
            }
            else
            {
                _model.PlayerShips.Add(ship);
                ShowShips();

                string info = _model.ShowMessageOnShipSet(ship, PlayerTypes.User);
            }
        }

        /// <summary>
        /// Проверка на готовность игрока с началу игры(количество кораблей должно быть = 10).
        /// </summary>
        /// <returns></returns>
        public void CheckPlayerReady(object sender, EventArgs e)
        {
            allClicks++;
            int count = 0;
            foreach (Ship ship in _model.PlayerShips)
            {
                count++;
            }
            if (count == 10)
            {
                _gameView.AddMessageToMessageBox("Игрок готов!\nКомпьютер готов!");
                _gameView.StartGame();
                _computerController.ShowShips();
                stopwatch.Start();
                return;
            }

            _gameView.ShowMessage("Сообщение", "Расставьте все корабли!");
        }

        /// <summary>
        /// Выстрел компьютера по полю игрока.
        /// </summary>
        public void ComputerShot(object sender, EventArgs e)
        {
            if (!_nextShootUser)
            {
                bool shootComputer = _computerController.ShootByComputer();

                if (shootComputer)
                {
                    _computerController.ShootByComputer();
                }
            }
        }

        /// <summary>
        /// Генерация точки выстрела.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="k"></param>
        public void GenerateShootPoint(object sender, EventArgs k)
        {
            allClicks++;
            DataGridViewCellMouseEventArgs e = (DataGridViewCellMouseEventArgs)k;
            Point point = new Point(e.RowIndex, e.ColumnIndex);
            TestSteps();
            Shoot(point);
        }

        /// <summary>
        /// Проверка на убитые
        /// </summary>
        /// <param name="parDeck"></param>
        /// <returns></returns>
        private bool CheckDeckInComputerDeadShips(Deck parDeck)
        {
            string caption = "Внимание!";
            string message = "Вы уже стреляли в эту клетку!";

            foreach (Ship ship in _model.ComputerDeadShips)
            {
                foreach (Deck deck in ship.DeadDecks)
                {
                    if (deck.Position == parDeck.Position)
                    {
                        _gameView.ShowMessage(caption, message);
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Проверка в кораблях компьютера.
        /// </summary>
        /// <param name="parDeck"></param>
        /// <returns></returns>
        private bool CheckInComputerShips(Deck parDeck)
        {
            string caption = "Внимание!";
            string message = "Вы уже стреляли в эту клетку!";
            foreach (Ship ship in _model.ComputerShips)
            {
                foreach (Deck deck in ship.DeadDecks)
                {
                    if (deck.Position == parDeck.Position)
                    {
                        _gameView.ShowMessage(caption, message);
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Проверка в клетках, в которые пользователь уже стрелял.
        /// </summary>
        /// <param name="parDeck"></param>
        /// <returns></returns>
        private bool CheckMissedCellsByUser(Deck parDeck)
        {
            string caption = "Внимание!";
            string message = "Вы уже стреляли в эту клетку!";
            foreach (Deck deck in _model.MissedCellsByUser)
            {
                if (parDeck.Position == deck.Position)
                {
                    _gameView.ShowMessage(caption, message);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Выстрел игрока по полю компьютера.
        /// </summary>
        /// <param name="parPoint">Точка выстрела.</param>
        /// <returns></returns>
        public bool Shoot(Point parPoint)
        {
            Deck shootDeck = new Deck(parPoint);

            if(CheckDeckInComputerDeadShips(shootDeck) == false)
            {
                return false;
            }

            if(CheckInComputerShips(shootDeck) == false)
            {
                return false;
            }

            if(CheckMissedCellsByUser(shootDeck) == false)
            {
                return false;
            }
            
            foreach (Ship ship in _model.ComputerShips)
            {
                foreach (Deck currentDeck in ship.AliveDecks)
                {
                    if (shootDeck.Position.X == currentDeck.Position.X && shootDeck.Position.Y == currentDeck.Position.Y)
                    {
                        _gameView.ShowShootedComputerDeck(currentDeck);
                        ship.ShotedDeck(currentDeck);

                        UpdateScore(true);
                        if (ship.AliveDecks.Count == 0)
                        {
                            _model.ComputerShips.Remove(ship);
                            _model.ComputerDeadShips.Add(ship);
                            _gameView.AddMessageToMessageBox("\n\nВыстрел игрока:\nИнф-ия о корабле:" + ship.ToString());
                            _gameView.AddMessageToMessageBox("Статус: Корабль убит!\n");
                        }
                        else
                        {
                            _gameView.AddMessageToMessageBox("\n\nВыстрел игрока:Корабль ранен!");
                        }

                        if (_model.ComputerShips.Count == 0)
                        {
                            GameOver(this, EventArgs.Empty);
                        }
                        _nextShootUser = true;
                        return true;
                    }
                }
            }

            _model.MissCell(shootDeck);
            _gameView.AddMessageToMessageBox("\n\nВыстрел игрока:Промах!");
            _gameView.ShowMissedUserShoot(new Point(shootDeck.Position.X, shootDeck.Position.Y));
            UpdateScore(false);
            _nextShootUser = false;
            return false;
        }

        /// <summary>
        /// Запись данных игрока в таблицу рекордов.
        /// </summary>
        /// <param name="parPlayer"></param>
        public static void RecordScore(Player parPlayer)
        {
            string appendText = string.Format(parPlayer.Name + "|" + parPlayer.Scores) + Environment.NewLine;
            File.AppendAllText(@"Records.bin", appendText);
        }

        /// <summary>
        /// Обновление очков пользователя.
        /// </summary>
        /// <param name="parRank"></param>
        private void UpdateScore(bool parRank)
        {
            int scores = _model.UpdateScore(parRank, _player);
            _gameView.UpdateScore(scores);
        }

        /// <summary>
        /// Окончание игры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GameOver(object sender, EventArgs e)
        {
            _gameView.GameOver(_player, true);
            stopwatch.Stop();
            _gameView.TestGameOver(stopwatch.Elapsed.Seconds, steps);
            RecordScore(_player);
        }

        /// <summary>
        /// Выбор однопалубного корабля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOneDeckShip(object sender, EventArgs e)
        {
            allClicks++;
            _shipLength = (int)ShipLengthCategories.OneDeckShip;
        }

        /// <summary>
        /// ВЫбор двухпалубного корабля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectTwoDeckShip(object sender, EventArgs e)
        {
            allClicks++;
            _shipLength = (int)ShipLengthCategories.TwoDeckShip;
        }

        /// <summary>
        /// Выбор трёхпалубного корабля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectThreeDeckShip(object sender, EventArgs e)
        {
            allClicks++;
            _shipLength = (int)ShipLengthCategories.ThreeDeckShip;
        }

        /// <summary>
        /// Выбор четырёхпалубного корабля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFourDeckShip(object sender, EventArgs e)
        {
            allClicks++;
            _shipLength = (int)ShipLengthCategories.FourDeckShip;
        }

        /// <summary>
        /// Смена ориентации корабля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeShipOrientation(object sender, EventArgs e)
        {
            allClicks++;
            _shipOrientation = _model.ChangeShipOrientation(_shipOrientation);
        }

        /// <summary>
        /// Отображение пользовательских кораблей.
        /// </summary>
        public void ShowShips()
        {
            foreach (Ship ship in _model.PlayerShips)
            {
                _gameView.ShowUserShip(ship);
            }
        }

        /// <summary>
        /// Отображение корабля при наведении курсора.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, EventArgs k)
        {
            DataGridViewCellMouseEventArgs e = (DataGridViewCellMouseEventArgs)k;
            switch (_shipOrientation)
            {
                case ShipOrientation.Horizontal:
                    MouseMoveHorizontalShip(e.RowIndex, e.ColumnIndex);
                    break;
                case ShipOrientation.Vertical:
                    MouseMoveVerticalShip(e.RowIndex, e.ColumnIndex);
                    break;
            }
        }

        /// <summary>
        /// Отображение горизонтального корабля при наведении курсора.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        private void MouseMoveHorizontalShip(int parX, int parY)
        {
            switch (_shipLength)
            {
                case 1:
                    DrawOneDeckShip(parX, parY, Color.Black);
                    break;
                case 2:
                    DrawHorizontalTwoDeckShip(parX, parY, Color.Black);
                    break;
                case 3:
                    DrawHorizontalThreeDeckShip(parX, parY, Color.Black);
                    break;
                case 4:
                    DrawHorizontalFourDeckShip(parX, parY, Color.Black);
                    break;
            }
        }

        /// <summary>
        /// Отображение вертикального корабля при наведении курсора.
        /// </summary>
        /// <param name="parX"></param>
        /// <param name="parY"></param>
        private void MouseMoveVerticalShip(int parX, int parY)
        {
            switch (_shipLength)
            {
                case 1:
                    DrawOneDeckShip(parX, parY, Color.Black);
                    break;
                case 2:
                    DrawVerticalTwoDeckShip(parX, parY, Color.Black);
                    break;
                case 3:
                    DrawVerticalThreeDeckShip(parX, parY, Color.Black);
                    break;
                case 4:
                    DrawVerticalFourDeckShip(parX, parY, Color.Black);
                    break;
            }
        }

        /// <summary>
        /// Снятие отображения корабля при передвижении курсора.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeave(object sender, EventArgs k)
        {
            DataGridViewCellEventArgs e = (DataGridViewCellEventArgs)k;
            switch (_shipOrientation)
            {
                case ShipOrientation.Horizontal:
                    MouseLeaveHorizontalShip(e.RowIndex, e.ColumnIndex);
                    break;
                case ShipOrientation.Vertical:
                    MouseLeaveVerticalShip(e.RowIndex, e.ColumnIndex);
                    break;
            }
        }

        /// <summary>
        /// Снятие изображения горизонтального корабля при передвижении курсора.
        /// </summary>
        /// <param name="parX">Кооридната x</param>
        /// <param name="parY">Координата y</param>
        private void MouseLeaveHorizontalShip(int parX, int parY)
        {
            switch (_shipLength)
            {
                case 1:
                    DrawOneDeckShip(parX, parY, Color.White);
                    break;
                case 2:
                    DrawHorizontalTwoDeckShip(parX, parY, Color.White);
                    break;
                case 3:
                    DrawHorizontalThreeDeckShip(parX, parY, Color.White);
                    break;
                case 4:
                    DrawHorizontalFourDeckShip(parX, parY, Color.White);
                    break;
            }
        }

        /// <summary>
        /// Снятие изображения вертикального корабля при передвижении курсора.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        private void MouseLeaveVerticalShip(int parX, int parY)
        {
            switch (_shipLength)
            {
                case 1:
                    DrawOneDeckShip(parX, parY, Color.White);
                    break;
                case 2:
                    DrawVerticalTwoDeckShip(parX, parY, Color.White);
                    break;
                case 3:
                    DrawVerticalThreeDeckShip(parX, parY, Color.White);
                    break;
                case 4:
                    DrawVerticalFourDeckShip(parX, parY, Color.White);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка однопалубного корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания клетки</param>
        private void DrawOneDeckShip(int parX, int parY, Color parColor)
        {
            CreateShipHorizontalView(parY, parY, parX, parColor);
        }

        /// <summary>
        /// Отрисовка двухпалубного горизонтального корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания ячейки</param>
        private void DrawHorizontalTwoDeckShip(int parX, int parY, Color parColor)
        {
            switch(parY)
            {
                case 8:
                    CreateShipHorizontalView(parY, parY + 1, parX, parColor);
                    break;
                case 9:
                    CreateShipHorizontalView(parY - 1, parY, parX, parColor);
                    break;
                default:
                    CreateShipHorizontalView(parY, parY + 1, parX, parColor);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка трёхпалубного горизонтального корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания</param>
        private void DrawHorizontalThreeDeckShip(int parX, int parY, Color parColor)
        {
            switch (parY)
            {
                case 8:
                    CreateShipHorizontalView(parY - 1, parY + 1, parX, parColor);
                    break;
                case 9:
                    CreateShipHorizontalView(parY - 2, parY, parX, parColor);
                    break;
                default:
                    CreateShipHorizontalView(parY, parY + 2, parX, parColor);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка четырёхпалубного горизонтального корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания</param>
        private void DrawHorizontalFourDeckShip(int parX, int parY, Color parColor)
        {
            switch (parY)
            {
                case 7:
                    CreateShipHorizontalView(parY - 1, parY + 2, parX, parColor);
                    break;
                case 8:
                    CreateShipHorizontalView(parY - 2, parY + 1, parX, parColor);
                    break;
                case 9:
                    CreateShipHorizontalView(parY - 3, parY, parX, parColor);
                    break;
                default:
                    CreateShipHorizontalView(parY, parY + 3, parX, parColor);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка двухпалубного вертикального корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания ячейки</param>
        private void DrawVerticalTwoDeckShip(int parX, int parY, Color parColor)
        {
            switch (parX)
            {
                case 9:
                    CreateShipVerticalView(parX - 1, parX, parY, parColor);
                    break;
                default:
                    CreateShipVerticalView(parX, parX + 1, parY, parColor);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка трёхпалубного вертикального корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания ячейки</param>
        private void DrawVerticalThreeDeckShip(int parX, int parY, Color parColor)
        {
            switch (parX)
            {
                case 8:
                    CreateShipVerticalView(parX - 1, parX + 1, parY, parColor);
                    break;
                case 9:
                    CreateShipVerticalView(parX - 2, parX, parY, parColor);
                    break;
                default:
                    CreateShipVerticalView(parX, parX + 2, parY, parColor);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка четырёхпалубного вертикального корабля.
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        /// <param name="parColor">Цвет закрашивания ячейки</param>
        private void DrawVerticalFourDeckShip(int parX, int parY, Color parColor)
        {
            switch (parX)
            {
                case 7:
                    CreateShipVerticalView(parX - 1, parX + 2, parY, parColor);
                    break;
                case 8:
                    CreateShipVerticalView(parX - 2, parX + 1, parY, parColor);
                    break;
                case 9:
                    CreateShipVerticalView(parX - 3, parX, parY, parColor);
                    break;
                default:
                    CreateShipVerticalView(parX, parX + 3, parY, parColor);
                    break;
            }
        }

        /// <summary>
        /// Отрисовка горизонтального корабля.
        /// </summary>
        /// <param name="parStartValue">Начальная координата по столбцам</param>
        /// <param name="parEndValue">Конечная координата по столбцам</param>
        /// <param name="parX">Постоянная величина по строкам</param>
        /// <param name="parColor">Цвет окрашивания</param>
        private void CreateShipHorizontalView(int parStartValue, int parEndValue, int parX, Color parColor)
        {
            for(int i = parStartValue; i <= parEndValue; i++)
            {
                _gameView.SetShipView(parX, i, parColor);
            }
        }

        /// <summary>
        /// Отрисовка вертикального корабля.
        /// </summary>
        /// <param name="parStartValue">Начальная координата по строкам</param>
        /// <param name="parEndValue">Конечная координата по строкам</param>
        /// <param name="parY">Постоянная величина по столбцам</param>
        /// <param name="parColor">Цвет окрашивания</param>
        private void CreateShipVerticalView(int parStartValue, int parEndValue, int parY, Color parColor)
        {
            for (int i = parStartValue; i <= parEndValue; i++)
            {
                _gameView.SetShipView(i, parY, parColor);
            }
        }
    }
}
