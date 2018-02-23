namespace SeaBattle
{
    /// <summary>
    /// Класс палуба.
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// Позиция палубы на поле.
        /// </summary>
        private Point _position;

        /// <summary>
        /// Позиция.
        /// </summary>
        public Point Position
        {
            get { return _position; }
        }
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parPosition"></param>
        public Deck(Point parPosition)
        {
            _position = parPosition;
        }
    }
}
