using System;

namespace Dictionary_C
{
    /// <summary>
    /// Представляет данные о ветре.
    /// </summary>
    public class Wind
    {
        /// <summary>
        /// Получает или задает скорость.
        /// </summary>
        public double Speed { get; set; }
        
        /// <summary>
        /// Получает или задает градус.
        /// </summary>
        public double Deg { get; set; }
        
        /// <summary>
        /// Получает или задает порыв.
        /// </summary>
        public double Gust { get; set; }
    }
}