namespace SeaBattle
{
    /// <summary>
    /// Интерфейс контроллера меню.
    /// </summary>
    public interface IMenuController
    {
        /// <summary>
        /// Проверка имени.
        /// </summary>
        void CheckName();

        /// <summary>
        /// Загрузка таблицы рекордов.
        /// </summary>
        void LoadRecords();
    }
}
