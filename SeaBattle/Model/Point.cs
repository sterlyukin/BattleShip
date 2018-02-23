using System;

namespace SeaBattle
{
    /// <summary>
    /// Класс точки.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Координата по x.
        /// </summary>
        private int _x;
        /// <summary>
        /// Координата по y.
        /// </summary>
        private int _y;

        /// <summary>
        /// Координата по x.
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Value must be positive or zero");
                }
                else
                {
                    _x = value;
                }
            }
        }
        /// <summary>
        /// Координата по y.
        /// </summary>
        public int Y
        {
            get { return _y; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value must be positive or zero");
                }
                else
                {
                    _y = value;
                }
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parX">X</param>
        /// <param name="parY">Y</param>
        public Point(int parX, int parY)
        {
            X = parX;
            Y = parY;
        }

        /// <summary>
        /// Перегрузка оператора сравнения двух точек.
        /// </summary>
        /// <param name="parPointA"></param>
        /// <param name="parPointB"></param>
        /// <returns></returns>
        public static bool operator ==(Point parPointA, Point parPointB)
        {
            if((parPointA.X == parPointB.X) && (parPointA.Y == parPointB.Y))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Перегрузка оператора сравнения двух точек.
        /// </summary>
        /// <param name="parPointA"></param>
        /// <param name="parPointB"></param>
        /// <returns></returns>
        public static bool operator !=(Point parPointA, Point parPointB)
        {
            if ((parPointA.X == parPointB.X) && (parPointA.Y == parPointB.Y))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Переопределённый метод Equals для сравнения двух точек.
        /// </summary>
        /// <param name="obj">Объект, с которым идёт сравнение.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this == (Point)obj;
        }

        /// <summary>
        /// Переопределённый метод для получения хэш-кода объекта.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _x.GetHashCode() * 17 + _y.GetHashCode() * 19;
        }
    }
}
