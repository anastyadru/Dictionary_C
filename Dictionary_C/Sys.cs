using System;

namespace Dictionary_C
{
    /// <summary>
    /// Представляет параметры.
    /// </summary>
    public class Sys
    {
        /// <summary>
        /// Получает или задает время восхода.
        /// </summary>
        public int Sunrise { get; set; }
        
        /// <summary>
        /// Получает или задает время заката.
        /// </summary>
        public int Sunset { get; set; }
    }
}