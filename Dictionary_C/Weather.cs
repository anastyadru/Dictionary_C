using System;

namespace Dictionary_C
{
    /// <summary>
    /// Представляет данные о погоде.
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// Получает или задает идентификатор местоположения.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Получает или задает главное.
        /// </summary>
        public string Main { get; set; }
        
        /// <summary>
        /// Получает или задает описание.
        /// </summary>
        public string Description { get; set; }
    }
}