namespace SeaBattle
{
    /// <summary>
    /// Интерфейс контроллера игры компьютера.
    /// </summary>
    public interface IComputerController
    {
        /// <summary>
        /// Отображение кораблей на поле компьютера.
        /// </summary>
        void ShowShips();

        /// <summary>
        /// Генерация выстрела компьютера по полю игрока.
        /// </summary>
        /// <returns></returns>
        bool ShootByComputer();

        /// <summary>
        /// Выстрел по точке.
        /// </summary>
        /// <param name="parPoint">Точка</param>
        /// <returns></returns>
        bool Shoot(Point parPoint);

        int ComputerSteps { get; }
    }
}
