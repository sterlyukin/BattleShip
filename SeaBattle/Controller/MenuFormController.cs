using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SeaBattle
{
    /// <summary>
    /// Контроллер меню
    /// </summary>
    public class MenuFormController : IMenuController
    {
        /// <summary>
        /// Представление меню
        /// </summary>
        private MenuView _startView;
        /// <summary>
        /// Представление игры.
        /// </summary>
        private GameView _gameView;
        /// <summary>
        /// Представление результатов игры.
        /// </summary>
        private RecordsView _gameOverView;
        /// <summary>
        /// Имя игрока.
        /// </summary>
        private string _playerName;
        /// <summary>
        /// Имя игрока.
        /// </summary>
        public string PlayerName
        {
            get { return _playerName; }
        }
        /// <summary>
        /// Список игроков.
        /// </summary>
        private List<Player> Players;
        /// <summary>
        /// Файл рекордов
        /// </summary>
        private const string RECORDS_FILE_PATH = @"Records.bin";

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MenuFormController()
        {
            _startView = new MenuFormView();
            _startView.OnStartGame += delegate { CheckName(); };
            _startView.OnShowRecords += delegate { LoadRecords(); };
            _startView.Run();
        }

        /// <summary>
        /// Проверка условия, что было введено имя игрока.
        /// </summary>
        public void CheckName()
        {
            if(string.IsNullOrEmpty(_startView.Name))
            {
                _startView.ShowError();
            }
            if (!string.IsNullOrEmpty(_startView.Name))
            {
                _playerName = _startView.Name;
                Player player = new Player(_playerName);
                _gameView = new GameFormView();
                GameFormController playerController = new GameFormController(_gameView, player);
            }
        }
        /// <summary>
        /// Загрузка таблицы рекордов.
        /// </summary>
        public void LoadRecords()
        {
            Players = new List<Player>();

            using (var stream = File.OpenText(RECORDS_FILE_PATH))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] info = line.Split('|');
                    Player player = new Player(info[0]);
                    player.Scores = int.Parse(info[1]);
                    Players.Add(player);
                }
            }
            Players = Players.OrderByDescending(x => x.Scores).ToList();

            _gameOverView = new RecordsFormView(Players);
            _gameOverView.Run();
        }
    }
}
