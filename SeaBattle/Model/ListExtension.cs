using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle
{
    /// <summary>
    /// Класс расширения для списка.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Рандомное возвращение двух координат.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parList"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this IEnumerable<T> parList)
        {
            return parList.ElementAt(new Random(DateTime.Now.Millisecond).Next(parList.Count()));
        }
    }
}
