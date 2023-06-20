using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dictionary_C
{
    /// <summary>
    /// Класс, представляющий данные о погоде.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Получает или задает координаты местоположения.
        /// </summary>
        public Coordinates Coord { get; set; }
        
        /// <summary>
        /// Получает или задает список данных о погоде.
        /// </summary>
        public List<Weather> Weather { get; set; }
        
        /// <summary>
        /// Получает или задает базовый параметр.
        /// </summary>
        [JsonProperty("base")]
        public string Base { get; set; }
        
        /// <summary>
        /// Получает или задает данные о погоде.
        /// </summary>
        public Data Data { get; set; }
        
        /// <summary>
        /// Список прогнозов погоды.
        /// </summary>
        public List<Forecast> ForecastList { get; set; }
        
        /// <summary>
        /// Получает или задает видимость.
        /// </summary>
        public int Visibility { get; set; }
        
        /// <summary>
        /// Получает или задает данные о ветре.
        /// </summary>
        public Wind Wind { get; set; }
        
        /// <summary>
        /// Получает или задает данные об облачности.
        /// </summary>
        public Clouds Clouds { get; set; }
        
        /// <summary>
        /// Получает или задает время получения данных.
        /// </summary>
        public int Dt { get; set; }
        
        /// <summary>
        /// Получает или задает данные о системе.
        /// </summary>
        public Sys Sys { get; set; }
        
        /// <summary>
        /// Получает или задает идентификатор местоположения.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Получает или задает название местоположения.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Получает или задает код ответа сервера.
        /// </summary>
        public int Cod { get; set; }
        
        /// <summary>
        /// Получает или задает данные о городе.
        /// </summary>
        public string City { get; set; }
    }
}