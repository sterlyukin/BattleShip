using System;

namespace SeaBattle
{
    /// <summary>
    /// Интерфейс поведения контроллера игры.
    /// </summary>
    public interface IGameController
    {
        /// <summary>
        /// Установка корабля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetShip(object sender, EventArgs e);

        /// <summary>
        /// Проверка готовности игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckPlayerReady(object sender, EventArgs e);

        /// <summary>
        /// Выстрел компьютера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ComputerShot(object sender, EventArgs e);

        /// <summary>
        /// Генерация точки выстрела игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GenerateShootPoint(object sender, EventArgs e);

        /// <summary>
        /// Выстрел игрока.
        /// </summary>
        /// <param name="parPoint">Точка выстрела.</param>
        /// <returns></returns>
        bool Shoot(Point parPoint);

        /// <summary>
        /// Отображение кораблей игрока.
        /// </summary>
        void ShowShips();
        /// <summary>
        /// Действия при появлении курсора в клетке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseMove(object sender, EventArgs e);

        /// <summary>
        /// Действия при убирании курсора. из клетки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseLeave(object sender, EventArgs e);

        /// <summary>
        /// Окончание игры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GameOver(object sender, EventArgs e);
    }
}
