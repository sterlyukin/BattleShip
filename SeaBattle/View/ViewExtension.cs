using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeaBattle
{
    /// <summary>
    /// Класс-расширение для отображения.
    /// </summary>
    public static class ViewExtension
    {
        /// <summary>
        /// Двойная буфферизация.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="setting"></param>
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
