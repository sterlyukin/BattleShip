namespace SeaBattle
{
    /// <summary>
    /// Игрок.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Имя.
        /// </summary>
        private string _name;
        /// <summary>
        /// Кол-во очков.
        /// </summary>
        private int _scores = 0;

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// Кол-во очков.
        /// </summary>
        public int Scores
        {
            get { return _scores; }
            set { _scores = value; }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parName"></param>
        public Player(string parName)
        {
            _name = parName;
        }
    }
}
