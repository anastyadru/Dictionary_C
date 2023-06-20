using System;

namespace Dictionary_C
{
    /// <summary>
    /// Класс, представляющий прогноз погоды.
    /// </summary>
    public class Forecast
    {
        /// <summary>
        /// Дата прогноза.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Температура воздуха.
        /// </summary>
        public double Temp { get; set; }
        
        /// <summary>
        /// Ощущаемая температура.
        /// </summary>
        public double FeelsLike { get; set; }
        
        /// <summary>
        /// Давление воздуха.
        /// </summary>
        public double Pressure { get; set; }
        
        /// <summary>
        /// Влажность воздуха.
        /// </summary>
        public double Humidity { get; set; }
    }
}