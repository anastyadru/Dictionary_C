using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dictionary_C
{
    
    /// <summary>
    /// Класс, содержащий точку входа в программу.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Асинхронно получает данные о погоде по указанному URL и возвращает объект WeatherData.
        /// </summary>
        /// <param name="url">URL для получения данных о погоде.</param>
        /// <returns>Объект WeatherData, содержащий данные о погоде.</returns>
        private static async Task<WeatherData> GetWeatherDataAsync(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                return weatherData;
            }
            return null;
        }

        /// <summary>
        /// Получает название города от пользователя.
        /// </summary>
        /// <returns>Название города.</returns>
        private static string GetCityName()
        {
            string cityName;

            try
            {
                Console.WriteLine("Введите, для какого города прогноз погоды: Minsk, London, Paris, NewYork, Warsaw");
                cityName = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Такого города не существует в списке. Введите город вручную: ");
                cityName = Console.ReadLine();
            }

            return cityName;
        }

        /// <summary>
        /// Получает тип прогноза погоды от пользователя.
        /// </summary>
        /// <returns>Тип прогноза погоды (1 - на 1 день, 5 - на 5 дней).</returns>
        private static int GetWeatherType()
        {
            Console.WriteLine("На сколько дней Вы хотите знать прогноз погоды: на 1 день, на 5 дней?");
            var weatherType = int.Parse(Console.ReadLine());
            return weatherType;
        }

        /// <summary>
        /// Выводит на консоль данные о текущей погоде в указанном городе.
        /// </summary>
        /// <param name="weatherData">Объект WeatherData, содержащий данные о погоде.</param>
        /// <param name="cityName">Название города.</param>
        private static void PrintCurrentWeather(WeatherData weatherData, string cityName)
        {
            var result = "";

            if (weatherData != null)
            {
                result += $"Прогноз погоды в городе {cityName} на сегодня: \n";
                result += $"Температура: {weatherData.Data.Temp}°C\n";
                result += $"Температура ощущается на: {weatherData.Data.FeelsLike}°C\n";
                result += $"Давление: {weatherData.Data.Pressure}Pa\n";
                result += $"Влажность: {weatherData.Data.Humidity}%\n";
                
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Ошибка получения данных о погоде в городе {cityName}\n");
            }
        }

        /// <summary>
        /// Выводит на консоль данные о прогнозе погоды на 5 дней в указанном городе.
        /// </summary>
        /// <param name="weatherData">Объект WeatherData, содержащий данные о погоде.</param>
        /// <param name="cityName">Название города.</param>
        private static void PrintWeatherForecast(WeatherData weatherData, string cityName)
        {
            var result = "";

            if (weatherData != null)
            {
                result += $"Прогноз погоды в городе {cityName} на 5 дней: \n";
                for (int i = 0; i < weatherData.ForecastList.Count; i++)
                {
                    Forecast forecast = weatherData.ForecastList[i];
                    result += $"День {i + 1}: \n";
                    result += $"Дата: {forecast.Date}\n";
                    result += $"Температура: {forecast.Temp}°C\n";
                    result += $"Температура ощущается на: {forecast.FeelsLike}°C\n";
                    result += $"Давление: {forecast.Pressure}Pa\n";
                    result += $"Влажность: {forecast.Humidity}%\n";
                }
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Ошибка получения данных о погоде в городе {cityName}\n");
            }
        }
        
        /// <summary>
        /// Метод, вызываемый при сохранении данных в хранилище.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private static void OnDataSaved(object sender, EventArgs e)
        {
            var storage = (Storage)sender;
            var json = JsonConvert.SerializeObject(storage.WeatherData, Formatting.Indented);
            File.WriteAllText("data.json", json);
        }
        
        /// <summary>
        /// Обработчик события удаления данных из хранилища.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private static void OnDataRemoved(object sender, EventArgs e)
        {
            var storage = (Storage)sender;
            storage.WeatherData.Clear();
            var json = JsonConvert.SerializeObject(storage.WeatherData, Formatting.Indented);
            File.WriteAllText("data.json", json);
        }

        /// <summary>
        /// Главный метод программы.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        private static async Task Main(string[] args)
        {
            var cityName = GetCityName();
            var weatherType = GetWeatherType();
            var storage = new Storage();

            if (File.Exists("data.json"))
            {
                var json = File.ReadAllText("data.json");
                storage.WeatherData = JsonConvert.DeserializeObject<ObservableDictionary<string, WeatherData>>(json);
            }
            
            if (weatherType == 1)
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
                var weatherData = await GetWeatherDataAsync(url);
                PrintCurrentWeather(weatherData, cityName);
                
                storage.WeatherData.Add(cityName, weatherData);
                storage.DataSaved += OnDataSaved;
                storage.DataRemoved += OnDataRemoved;
                storage.SaveData();
            }
            else if (weatherType == 5)
            {
                var url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
                var weatherData = await GetWeatherDataAsync(url);
                PrintWeatherForecast(weatherData, cityName);
                
                storage.WeatherData.Add(cityName, weatherData);
                storage.DataSaved += OnDataSaved;
                storage.DataRemoved += OnDataRemoved;
                storage.SaveData();
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, укажите, на сколько дней Вы хотите знать прогноз погоды: на 1 день или на 5 дней.");
            }

            Console.ReadLine();
        }
    }
}