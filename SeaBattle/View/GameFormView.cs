using System;
using System.Drawing;
using System.Windows.Forms;

namespace SeaBattle
{
    /// <summary>
    /// Представление игры.
    /// </summary>
    public class GameFormView : GameView
    {
        /// <summary>
        /// Событие постановки корабляю
        /// </summary>
        public override event EventHandler OnSetShip;
        /// <summary>
        /// Событие передвижения корабля.
        /// </summary>
        public override event EventHandler OnMoveShip;
        /// <summary>
        /// Событие снятия корабля с позиции.
        /// </summary>
        public override event EventHandler OnLeaveShip;

        /// <summary>
        /// Событие выбора однопалубного корабляю
        /// </summary>
        public override event EventHandler OnSelectOneDeckShip;
        /// <summary>
        /// Событие выбора двухпалубного корабля.
        /// </summary>
        public override event EventHandler OnSelectTwoDeckShip;
        /// <summary>
        /// Событие выбора трёхпалубного корабля.
        /// </summary>
        public override event EventHandler OnSelectThreeDeckShip;
        /// <summary>
        /// Событие выбора четырёхпалубного корабля.
        /// </summary>
        public override event EventHandler OnSelectFourDeckShip;

        /// <summary>
        /// Событие изменения ориентации корабля.
        /// </summary>
        public override event EventHandler OnChangeShipOrientation;

        /// <summary>
        /// Событие начала игры.
        /// </summary>
        public override event EventHandler OnStartGame;
        /// <summary>
        /// Событие выстрела.
        /// </summary>
        public override event EventHandler OnShoot;
        /// <summary>
        /// Событие окончания игры.
        /// </summary>
        public override event EventHandler OnGameOver;

        #region Fields
        /// <summary>
        /// Форма игры
        /// </summary>
        private GameForm _gameForm;
        /// <summary>
        /// Поле игрока.
        /// </summary>
        private DataGridView _userFied;
        /// <summary>
        /// Поле компьютера.
        /// </summary>
        private DataGridView _computerField;
        /// <summary>
        /// Поле вывода кол-ва очков игрока.
        /// </summary>
        private TextBox _outputScores;
        /// <summary>
        /// Поле вывода сообщений при игре.
        /// </summary>
        private RichTextBox _messageSpace;

        /// <summary>
        /// Надпись корабли игрока.
        /// </summary>
        private Label _playerLabel;
        /// <summary>
        /// Надпись корабли компьютера.
        /// </summary>
        private Label _computerLabel;
        /// <summary>
        /// Надпись кол-во очков.
        /// </summary>
        private Label _scores;

        /// <summary>
        /// Панель правил игры.
        /// </summary>
        private Panel _rulesPanel;
        /// <summary>
        /// Правила игры.
        /// </summary>
        private Label _rulesText;

        /// <summary>
        /// Кнопка начала игры.
        /// </summary>
        private Button _startGame;
        /// <summary>
        /// Кнопка изменения ориентации корабля.
        /// </summary>
        private Button _changeShipOrientation;
        /// <summary>
        /// Кнопка окончания игры.
        /// </summary>
        private Button _endGameBtn;

        /// <summary>
        /// Группировка выбора типа кораблей.
        /// </summary>
        private GroupBox _playerShipsGroupBox;

        /// <summary>
        /// Выбор однопалубного корабля.
        /// </summary>
        private RadioButton _oneDeckShip;
        /// <summary>
        /// Выбор двухпалубного корабля.
        /// </summary>
        private RadioButton _twoDeckShip;
        /// <summary>
        /// Выбор трёхпалубного корабля.
        /// </summary>
        private RadioButton _threeDeckShip;
        /// <summary>
        /// Выбор четырёхпалубного корабля.
        /// </summary>
        private RadioButton _fourDeckShip;
#endregion
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parForm">Экземпляр формы</param>
        public GameFormView()
        {
            CreateForm();
        }

        /// <summary>
        /// Инициализация представления игры. Подписка на события.
        /// </summary>
        public override void InitGameView()
        {
            InitForm();
            _userFied.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(OnSetShip);
            _userFied.CellMouseMove += new DataGridViewCellMouseEventHandler(OnMoveShip);
            _userFied.CellMouseLeave += new DataGridViewCellEventHandler(OnLeaveShip);
            _changeShipOrientation.Click += new EventHandler(OnChangeShipOrientation);
            _startGame.Click += new EventHandler(OnStartGame);
            _computerField.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(OnShoot);

            _endGameBtn.Click += OnGameOver;


            InitShipLength();
            Run();
        }

        /// <summary>
        /// Подписка на события изменения длины корабля.
        /// </summary>
        public void InitShipLength()
        {
            _messageSpace.TextChanged += delegate { _messageSpace.ScrollToCaret(); };

            _oneDeckShip.CheckedChanged += OnSelectOneDeckShip;
            _twoDeckShip.CheckedChanged += OnSelectTwoDeckShip;
            _threeDeckShip.CheckedChanged += OnSelectThreeDeckShip;
            _fourDeckShip.CheckedChanged += OnSelectFourDeckShip;
        }

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        public void InitForm()
        {
            CreateOutputScores();
            CreateScoreLabel();

            CreateStartGameButton();
            CreateChangeShipOrientationButton();
            CreateLabels();
            CreatePlayerGroupBox();
            CreateShipModelsRadioButtons();
            CreateUserField();
            CreateComputerField();
            CreateMessageSpace();
            CreateRulesPanel();
            CreateGameOverButton();

            _gameForm.Controls.Add(_startGame);
            _gameForm.Controls.Add(_outputScores);
            _gameForm.Controls.Add(_scores);
            _gameForm.Controls.Add(_playerLabel);
            _gameForm.Controls.Add(_computerLabel);
            _gameForm.Controls.Add(_playerShipsGroupBox);
            _gameForm.Controls.Add(_userFied);
            _gameForm.Controls.Add(_computerField);
            _gameForm.Controls.Add(_messageSpace);
            _gameForm.Controls.Add(_rulesPanel);
            _gameForm.Controls.Add(_endGameBtn);

            _rulesPanel.Controls.Add(_rulesText);

            _playerShipsGroupBox.Controls.Add(_oneDeckShip);
            _playerShipsGroupBox.Controls.Add(_twoDeckShip);
            _playerShipsGroupBox.Controls.Add(_threeDeckShip);
            _playerShipsGroupBox.Controls.Add(_fourDeckShip);
            _playerShipsGroupBox.Controls.Add(_changeShipOrientation);

        }

        /// <summary>
        /// Запуск.
        /// </summary>
        public void Run()
        {
            _gameForm.ShowDialog();
        }

        /// <summary>
        /// Инициализация окна формы.
        /// </summary>
        private void CreateForm()
        {
            _gameForm = new GameForm();
            _gameForm.Text = "Морской бой";
            _gameForm.Width = 620;
            _gameForm.Height = 450;
            _gameForm.BackColor = Color.LightBlue;
        }

        #region InitFormComponents
        /// <summary>
        /// Создание вывода правил игры.
        /// </summary>
        private void CreateRulesPanel()
        {
            _rulesPanel = new Panel();
            _rulesPanel.Width = 620;
            _rulesPanel.Height = 20;
            _rulesPanel.BorderStyle = BorderStyle.FixedSingle;

            _rulesText = new Label();
            _rulesText.AutoSize = true;
            _rulesText.Text = "ПРАВИЛА ИГРЫ: 4 - однопалубных корабля; 3 - двухпалубных; 2 - трёхпалубных; 1 - четырёхпалубный";
        }

        /// <summary>
        /// Создание поля вывода кол-ва очков.
        /// </summary>
        private void CreateOutputScores()
        {
            _outputScores = new TextBox();
            _outputScores.Top = 330;
            _outputScores.Left = 10;
            _outputScores.Visible = true;
            _outputScores.Width = 35;
            _outputScores.Height = 20;
            _outputScores.ReadOnly = true;
            _outputScores.Text = 0.ToString();
        }

        /// <summary>
        /// Создание надписи кол-ва очков.
        /// </summary>
        public void CreateScoreLabel()
        {
            _scores = new Label();
            _scores.Top = 313;
            _scores.Left = 10;
            _scores.AutoSize = true;
            _scores.Text = "Очки";
        }

        /// <summary>
        /// Создание поля вывода сообщений.
        /// </summary>
        private void CreateMessageSpace()
        {
            _messageSpace = new RichTextBox();
            _messageSpace.Top = 265;
            _messageSpace.Left = 155;
            _messageSpace.Width = 430;
            _messageSpace.Height = 100;
            _messageSpace.ReadOnly = true;

        }

        /// <summary>
        /// Кнопка завершения игры.
        /// </summary>
        private void CreateGameOverButton()
        {
            _endGameBtn = new Button();
            _endGameBtn.Width = 135;
            _endGameBtn.Height = 50;
            _endGameBtn.Top = 360;
            _endGameBtn.Left = 10;
            _endGameBtn.BackColor = Color.LightPink;
            _endGameBtn.Text = "Закончить";
        }

        /// <summary>
        /// Создание поля игрока.
        /// </summary>
        private void CreateUserField()
        {
            _userFied = new DataGridView();
            _userFied.RowCount = 10;
            _userFied.ColumnCount = 10;
            _userFied.Top = 50;
            _userFied.Left = 155;
            _userFied.Width = 203;
            _userFied.Height = 203;
            _userFied.DefaultCellStyle.SelectionBackColor = Color.Black;
            _userFied.ReadOnly = true;
            _userFied.AllowUserToResizeRows = false;
            _userFied.AllowUserToResizeColumns = false;
            _userFied.RowHeadersVisible = false;
            _userFied.ColumnHeadersVisible = false;
            _userFied.MultiSelect = false;
            for (int i = 0; i < _userFied.Columns.Count; i++)
            {
                _userFied.Columns[i].Width = 20;
            }
            for (int i = 0; i < _userFied.Rows.Count; i++)
            {
                _userFied.Rows[i].Height= 20;
            }
            _userFied.Visible = true;
            _userFied.DoubleBuffered(true);
        }

        /// <summary>
        /// Создание поля компьютера.
        /// </summary>
        private void CreateComputerField()
        {
            _computerField = new DataGridView();
            _computerField.RowCount = 10;
            _computerField.ColumnCount = 10;
            _computerField.Top = 50;
            _computerField.Left = 380;
            _computerField.Width = 203;
            _computerField.Height = 203;
            _computerField.DefaultCellStyle.SelectionBackColor = Color.Black;
            _computerField.ReadOnly = true;
            _computerField.AllowUserToResizeRows = false;
            _computerField.AllowUserToResizeColumns = false;
            _computerField.RowHeadersVisible = false;
            _computerField.ColumnHeadersVisible = false;
            _computerField.MultiSelect = false;
            for (int i = 0; i < _userFied.Columns.Count; i++)
            {
                _computerField.Columns[i].Width = 20;
            }
            for (int i = 0; i < _userFied.Rows.Count; i++)
            {
                _computerField.Rows[i].Height = 20;
            }
            _computerField.Visible = false;
            _computerField.DoubleBuffered(true);
        }

        /// <summary>
        /// Создание
        /// </summary>
        private void CreateLabels()
        {
            _playerLabel = new Label();
            _playerLabel.AutoSize = true;
            _playerLabel.Text = "Корабли игрока:";
            _playerLabel.Top = 25;
            _playerLabel.Left = 155;

            _computerLabel = new Label();
            _computerLabel.AutoSize = true;
            _computerLabel.Text = "Корабли компьютера:";
            _computerLabel.Top = 25;
            _computerLabel.Left = 395;
        }

        /// <summary>
        /// Создание кнопки начала игры.
        /// </summary>
        private void CreateStartGameButton()
        {
            _startGame = new Button();
            _startGame.Text = "Начать игру";
            _startGame.Top = 265;
            _startGame.Left = 10;
            _startGame.Width = 135;
            _startGame.Height = 50;
            _startGame.BackColor = Color.LightGreen;
        }

        /// <summary>
        /// Создание кнопки изменения ориентации корабля.
        /// </summary>
        private void CreateChangeShipOrientationButton()
        {
            _changeShipOrientation = new Button();
            _changeShipOrientation.Top = 145;
            _changeShipOrientation.Left = 35;
            _changeShipOrientation.Width = 60;
            _changeShipOrientation.Height = 50;
            _changeShipOrientation.AutoSize = true;
            _changeShipOrientation.BackgroundImage = SeaBattle.Resource.TurnImage;
            _changeShipOrientation.BackgroundImageLayout = ImageLayout.Center;
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(_changeShipOrientation, "Изменить ориентацию корабля");
        }

        /// <summary>
        /// Создание группировки элементов выбора корабля.
        /// </summary>
        private void CreatePlayerGroupBox()
        {
            _playerShipsGroupBox = new GroupBox();
            _playerShipsGroupBox.Top = 43;
            _playerShipsGroupBox.Left = 5;
            _playerShipsGroupBox.Text = "Тип корабля";
            _playerShipsGroupBox.Height = 210;
            _playerShipsGroupBox.Width = 145;
            _playerShipsGroupBox.Visible = true;
        }

        /// <summary>
        /// Создание кнопок выбора типа корабля.
        /// </summary>
        private void CreateShipModelsRadioButtons()
        {
            _oneDeckShip = new RadioButton();
            _oneDeckShip.Text = "Однопалубный";
            _oneDeckShip.AutoSize = true;
            _oneDeckShip.Top = 25;
            _oneDeckShip.Left = 5;

            _twoDeckShip = new RadioButton();
            _twoDeckShip.Text = "Двухпалубный";
            _twoDeckShip.AutoSize = true;
            _twoDeckShip.Top = 55;
            _twoDeckShip.Left = 5;

            _threeDeckShip = new RadioButton();
            _threeDeckShip.Text = "Трёхпалубный";
            _threeDeckShip.AutoSize = true;
            _threeDeckShip.Top = 85;
            _threeDeckShip.Left = 5;

            _fourDeckShip = new RadioButton();
            _fourDeckShip.Text = "Четырёхпалубный";
            _fourDeckShip.AutoSize = true;
            _fourDeckShip.Top = 115;
            _fourDeckShip.Left = 5;
        }
        #endregion
        /// <summary>
        /// Вывод системного сообщения.
        /// </summary>
        /// <param name="caption">Заголовок.</param>
        /// <param name="message">Текст сообщения.</param>
        public override void ShowMessage(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Вывод сообщения в диалог.
        /// </summary>
        /// <param name="parMessage">Текст сообщения</param>
        public override void AddMessageToMessageBox(string parMessage)
        {
            _messageSpace.AppendText(parMessage);
        }

        /// <summary>
        /// Начало игры.
        /// </summary>
        public override void StartGame()
        {
            _computerField.Visible = true;
        }

        /// <summary>
        /// Обновление очков игрока.
        /// </summary>
        /// <param name="parNewValue">Новое значение.</param>
        public override void UpdateScore(int parNewValue)
        {
            _outputScores.Text = parNewValue.ToString();
        }

        /// <summary>
        /// Попадание при выстреле игрока.
        /// </summary>
        /// <param name="parDeck">Палуба компьютера.</param>
        public override void ShowShootedComputerDeck(Deck parDeck)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.BlastImage;
            _computerField.Rows[parDeck.Position.X].Cells[parDeck.Position.Y] = imgCell;
        }

        /// <summary>
        /// Промах при выстреле игрока.
        /// </summary>
        /// <param name="parPoint">Координата выстрела.</param>
        public override void ShowMissedUserShoot(Point parPoint)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.MissBackImage;
            _computerField.Rows[parPoint.X].Cells[parPoint.Y] = imgCell;
        }

        /// <summary>
        /// Попадание при выстреле компьютера.
        /// </summary>
        /// <param name="parDeck">Палуба игрока.</param>
        public override void ShowShootedUserDeck(Deck parDeck)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.BackShooted;
            _userFied.Rows[parDeck.Position.X].Cells[parDeck.Position.Y] = imgCell;
        }

        /// <summary>
        /// Промах при выстреле компьютера.
        /// </summary>
        /// <param name="parPoint">Координата выстрела.</param>
        public override void ShowMissedComputerShoot(Point parPoint)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.MissBackImage;
            _userFied.Rows[parPoint.X].Cells[parPoint.Y] = imgCell;
        }

        /// <summary>
        /// Вывод корабля на поле игрока.
        /// </summary>
        /// <param name="parShip">Экземпляр корабля.</param>
        public override void ShowUserShip(Ship parShip)
        {
            if(parShip.Orientation == ShipOrientation.Horizontal)
            {
                foreach (Deck deck in parShip.AliveDecks)
                {
                    ShowUserHorizontalDeck(deck);
                }
            }
            else
            {
                foreach (Deck deck in parShip.AliveDecks)
                {
                    ShowUserVerticalDeck(deck);
                }
            }
        }

        /// <summary>
        /// Вывод палубы на поле компьютера.
        /// </summary>
        /// <param name="parDeck">Экземпляр палубы</param>
        private void ShowDeck(Deck parDeck)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.BackImage;
            _computerField.Rows[parDeck.Position.X].Cells[parDeck.Position.Y] = imgCell;
        }

        /// <summary>
        /// Вывести отображение корабля компьютера.
        /// </summary>
        /// <param name="parShip">Экземпляр корабля</param>
        public override void ShowComputerShip(Ship parShip)
        {
            foreach (Deck deck in parShip.AliveDecks)
            {
                ShowDeck(deck);
            }
        }

        /// <summary>
        /// Горизонтальное отображение палубы.
        /// </summary>
        /// <param name="parDeck"></param>
        private void ShowUserHorizontalDeck(Deck parDeck)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.DeckImage;
            _userFied.Rows[parDeck.Position.X].Cells[parDeck.Position.Y] = imgCell;
        }

        /// <summary>
        /// Вертикальное отображение палубы.
        /// </summary>
        /// <param name="parDeck"></param>
        private void ShowUserVerticalDeck(Deck parDeck)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = SeaBattle.Resource.VerticalDeckImage;
            _userFied.Rows[parDeck.Position.X].Cells[parDeck.Position.Y] = imgCell;
        }

        /// <summary>
        /// Вывести представление корабля.
        /// </summary>
        /// <param name="parX"></param>
        /// <param name="parY"></param>
        /// <param name="parColor"></param>
        public override void SetShipView(int parX, int parY, Color parColor)
        {
            _userFied.Rows[parX].Cells[parY].Style.BackColor = parColor;
        }

        /// <summary>
        /// Окончание игры.
        /// </summary>
        /// <param name="parPlayer">Игрок.</param>
        /// <param name="parPlayerWon">Выиграл ли игрок.</param>
        public override void GameOver(Player parPlayer, bool parPlayerWon)
        {
            string result = string.Format(parPlayerWon ? "Вы выиграли!" : "Вы проиграли!");

            MessageBox.Show(string.Format(result +"\nИгрок:{0}\nНабрано очков:{1}", parPlayer.Name, parPlayer.Scores), "Игра закончена!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _gameForm.Close();
        }


        public override void Close()
        {
            _gameForm.Close();
        }

        public override void TestGameOver(int time, int steps)
        {
            MessageBox.Show(string.Format("Время:{0}\nКол-во выстрелов:{1}", time, steps), "Игра закончена!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
