using System.Collections.Generic;
using System.Windows.Forms;

namespace SeaBattle
{
    /// <summary>
    /// Класс представления отображения рекордов.
    /// </summary>
    public class RecordsFormView : RecordsView
    {
        /// <summary>
        /// Форма.
        /// </summary>
        private RecordForm _recordForm;
        /// <summary>
        /// Надпись.
        /// </summary>
        private Label _playerInfo;
        /// <summary>
        /// Таблица рекордов.
        /// </summary>
        private DataGridView _recordTable;
        /// <summary>
        /// Список игроков, отображаемых в таблице.
        /// </summary>
        private List<Player> _allPlayers;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parPlayers">Список игроков.</param>
        public RecordsFormView(List<Player> parPlayers)
        {
            InitForm();
            _allPlayers = parPlayers;
            InitLabel();
            InitGrid();

            _recordForm.Controls.Add(_playerInfo);
            _recordForm.Controls.Add(_recordTable);
        }

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        private void InitForm()
        {
            _recordForm = new RecordForm();
            _recordForm.Width = 300;
            _recordForm.Height = 400;
            _recordForm.Text = "Морской бой | Рекорды";
        }

        /// <summary>
        /// Инициализации надписи.
        /// </summary>
        private void InitLabel()
        {
            _playerInfo = new Label();
            _playerInfo.Top = 10;
            _playerInfo.Left = 20;
            _playerInfo.AutoSize = true;
            _playerInfo.Text = "Таблица рекордов:";
        }

        /// <summary>
        /// Инициализация таблицы.
        /// </summary>
        private void InitGrid()
        {
            _recordTable = new DataGridView();
            _recordTable.Width = 250;
            _recordTable.Height = 250;
            _recordTable.Top = 40;
            _recordTable.Left = 15;
            _recordTable.AllowUserToAddRows = false;
            _recordTable.AllowUserToDeleteRows = false;
            _recordTable.AllowUserToResizeColumns = false;
            _recordTable.AllowUserToResizeRows = false;
            _recordTable.ReadOnly = true;
            _recordTable.DataSource = _allPlayers;
        }

        /// <summary>
        /// Запуск представления.
        /// </summary>
        public override void Run()
        {
            _recordForm.ShowDialog();
        }
    }
}
