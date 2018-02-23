using System;
using System.Drawing;

namespace SeaBattle
{
    /// <summary>
    /// Абстрактный класс представления игры.
    /// </summary>
    public abstract class GameView
    {
        /// <summary>
        /// Событие постановки корабля.
        /// </summary>
        public abstract event EventHandler OnSetShip;
        /// <summary>
        /// Событие перемещения корабля.
        /// </summary>
        public abstract event EventHandler OnMoveShip;
        /// <summary>
        /// Событие снятия корабля.
        /// </summary>
        public abstract event EventHandler OnLeaveShip;

        /// <summary>
        /// Событие выбора однопалубного корабля.
        /// </summary>
        public abstract event EventHandler OnSelectOneDeckShip;
        /// <summary>
        /// Событие выбора двухпалубного корабля.
        /// </summary>
        public abstract event EventHandler OnSelectTwoDeckShip;
        /// <summary>
        /// Событие выбора трёхпалубного корабля.
        /// </summary>
        public abstract event EventHandler OnSelectThreeDeckShip;
        /// <summary>
        /// Событие выбора четырёхпалубного корабля.
        /// </summary>
        public abstract event EventHandler OnSelectFourDeckShip;

        /// <summary>
        /// Событие изменения ориентации корабля.
        /// </summary>
        public abstract event EventHandler OnChangeShipOrientation;
        /// <summary>
        /// Событие начала игры.
        /// </summary>
        public abstract event EventHandler OnStartGame;
        /// <summary>
        /// Событие выстрела игрока.
        /// </summary>
        public abstract event EventHandler OnShoot;
        /// <summary>
        /// Событие окончания игры.
        /// </summary>
        public abstract event EventHandler OnGameOver;

        /// <summary>
        /// Показать пробитые игроком палубы компьютера.
        /// </summary>
        /// <param name="parDeck">Палуба.</param>
        public abstract void ShowShootedComputerDeck(Deck parDeck);
        /// <summary>
        /// Показать выстрел игрока мимо.
        /// </summary>
        /// <param name="parPoint">Точка</param>
        public abstract void ShowMissedUserShoot(Point parPoint);
        /// <summary>
        /// Показать пробитые компьютером палубы игрока.
        /// </summary>
        /// <param name="parDeck">Палуба</param>
        public abstract void ShowShootedUserDeck(Deck parDeck);
        /// <summary>
        /// Показать выстрел компьютера мимо.
        /// </summary>
        /// <param name="parPoint">Точка</param>
        public abstract void ShowMissedComputerShoot(Point parPoint);
        /// <summary>
        /// Показать корабль игрока.
        /// </summary>
        /// <param name="parShip">Корабль</param>
        public abstract void ShowUserShip(Ship parShip);
        /// <summary>
        /// Показать корабль компьютера.
        /// </summary>
        /// <param name="parShip">Корабль</param>
        public abstract void ShowComputerShip(Ship parShip);

        /// <summary>
        /// Установить представление корабля.
        /// </summary>
        /// <param name="parX">Координата по x</param>
        /// <param name="parY">Координата по y</param>
        /// <param name="parColor"></param>
        public abstract void SetShipView(int parX, int parY, Color parColor);
        /// <summary>
        /// Инициализировать представление игры.
        /// </summary>
        public abstract void InitGameView();
        /// <summary>
        /// Показать всплывающее сообщение.
        /// </summary>
        /// <param name="parCaption">Заголовок</param>
        /// <param name="parMessage">Текст</param>
        public abstract void ShowMessage(string parCaption, string parMessage);
        /// <summary>
        /// Добавить текст в поле сообщений.
        /// </summary>
        /// <param name="parMessage">Текст</param>
        public abstract void AddMessageToMessageBox(string parMessage);

        /// <summary>
        /// Начать игру.
        /// </summary>
        public abstract void StartGame();
        /// <summary>
        /// Закончить игру.
        /// </summary>
        /// <param name="parPlayer">Игрок</param>
        /// <param name="parPlayerWon">Выиграл ли игрок</param>
        public abstract void GameOver(Player parPlayer, bool parPlayerWon);
        /// <summary>
        /// Обновить кол-во очков игрока.
        /// </summary>
        /// <param name="parUpdateValue">Величина обновления</param>
        public abstract void UpdateScore(int parUpdateValue);


        public abstract void Close();
        public abstract void TestGameOver(int time, int steps);
    }
}
