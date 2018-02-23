using System;
using System.Windows.Forms;

namespace SeaBattle
{
    /// <summary>
    /// Стартовое представление.
    /// </summary>
    public class MenuFormView : MenuView
    {
        /// <summary>
        /// Начало игры.
        /// </summary>
        public override event EventHandler OnStartGame;
        /// <summary>
        /// Просмотр рекордов.
        /// </summary>
        public override event EventHandler OnShowRecords;

        #region Fields
        /// <summary>
        /// Форма меню.
        /// </summary>
        private MainForm _startForm;
        /// <summary>
        /// Поле ввода имени игрока.
        /// </summary>
        private TextBox _inputText;
        /// <summary>
        /// Кнопка начала игры.
        /// </summary>
        private Button _startGameBtn;
        /// <summary>
        /// Кнопка отмены игры.
        /// </summary>
        private Button _exitGameBtn;
        /// <summary>
        /// Кнопка просмотра рекордов.
        /// </summary>
        private Button _showRecords;

        #endregion
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parForm">Экземпляр формы</param>
        public MenuFormView()
        {
            InitForm();
            CreateInputText();
            CreateStartGameButton();
            CreateExitGameButton();
            CreateShowRecordsButton();

            _startForm.Controls.Add(_inputText);
            _startForm.Controls.Add(_startGameBtn);
            _startForm.Controls.Add(_exitGameBtn);
            _startForm.Controls.Add(_showRecords);

            Subscribe();
        }

        /// <summary>
        /// Подписка на события.
        /// </summary>
        private void Subscribe()
        {
            _startGameBtn.Click += new EventHandler(NewGame);
            _exitGameBtn.Click += new EventHandler(ExitGame);
            _showRecords.Click += new EventHandler(ShowRecords);
        }

        #region InitFormComponents
        /// <summary>
        /// Инициализация окна формы.
        /// </summary>
        /// <param name="parForm">Экземпляр формы</param>
        private void InitForm()
        {
            _startForm = new MainForm();
            _startForm.Text = "Морской бой";
            _startForm.BackgroundImage = SeaBattle.Resource.BackMenu;
            _startForm.Width = 440;
            _startForm.Height = 330;
            _startForm.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Создание поля ввода имени.
        /// </summary>
        private void CreateInputText()
        {
            _inputText = new TextBox();
            _inputText.Left = 145;
            _inputText.Top = 55;
            _inputText.Width = 195;
            _inputText.Height = 50;
        }
        /// <summary>
        /// Создание кнопки начала игры.
        /// </summary>
        private void CreateStartGameButton()
        {
            _startGameBtn = new Button();
            _startGameBtn.Left = 145;
            _startGameBtn.Top = 90;
            _startGameBtn.Text = "Начать игру";
            _startGameBtn.Width = 90;
            _startGameBtn.Height = 30;
        }
        /// <summary>
        /// Создание кнопки отмены игры.
        /// </summary>
        private void CreateExitGameButton()
        {
            _exitGameBtn = new Button();
            _exitGameBtn.Left = 250;
            _exitGameBtn.Top = 90;
            _exitGameBtn.Text = "Выйти";
            _exitGameBtn.Width = 90;
            _exitGameBtn.Height = 30;
        }
        /// <summary>
        /// Создание кнопки просмотра рекордов.
        /// </summary>
        private void CreateShowRecordsButton()
        {
            _showRecords = new Button();
            _showRecords.Left = 170;
            _showRecords.Top = 130;
            _showRecords.Text = "Рекорды";
            _showRecords.Width = 150;
            _showRecords.Height = 40;
        }
        #endregion

        /// <summary>
        /// Новая игра.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewGame(object sender, EventArgs e)
        {
            Name = _inputText.Text;
            OnStartGame(sender, e);
        }

        /// <summary>
        /// Выход из игры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExitGame(object sender, EventArgs e)
        {
            _startForm.Close();
        }

        /// <summary>
        /// Вывод рекордов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowRecords(object sender, EventArgs e)
        {
            OnShowRecords(sender, e);
        }
        /// <summary>
        /// Вывод ошибки при невведённом имени.
        /// </summary>
        public override void ShowError()
        {
            MessageBox.Show("Вы не ввели имя игрока!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Запуск.
        /// </summary>
        public override void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(_startForm);
        }
    }
}
