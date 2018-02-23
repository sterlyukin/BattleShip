using System;

namespace SeaBattle
{
    /// <summary>
    /// Абстрактный класс представления меню.
    /// </summary>
    public abstract class MenuView
    {
        /// <summary>
        /// Событие начала игры.
        /// </summary>
        public abstract event EventHandler OnStartGame;
        /// <summary>
        /// Событие вывода таблицы рекордов.
        /// </summary>
        public abstract event EventHandler OnShowRecords;

        /// <summary>
        /// Введённое имя игрока.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вывод ошибки.
        /// </summary>
        public abstract void ShowError();

        /// <summary>
        /// Запуск.
        /// </summary>
        public abstract void Run();
    }
}
