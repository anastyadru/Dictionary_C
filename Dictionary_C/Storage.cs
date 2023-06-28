using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Dictionary_C
{
    /// <summary>
    /// Класс для сохранения и загрузки объектов в файл.
    /// </summary>
    public class Storage
    {
        public ObservableDictionary<string, WeatherData> WeatherData { get; set; }
        
        /// <summary>
        /// Сохраняет объект в файл по указанному пути.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="data">Объект для сохранения.</param>
        /// <param name="filePath">Путь к файлу, в который нужно сохранить объект.</param>
        private static void Save<T>(T data, string filePath)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            
            using var fileStream = new FileStream(filePath, FileMode.Create);
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, data); 
        }

        /// <summary>
        /// Загружает объект из файла по указанному пути.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="filePath">Путь к файлу, из которого нужно загрузить объект.</param>
        /// <returns>Загруженный объект типа T.</returns>
        public static T Load<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not found");
            }

            using var fileStream = new FileStream(filePath, FileMode.Open);
            var binaryFormatter = new BinaryFormatter();
            
            try
            {
                return (T)binaryFormatter.Deserialize(fileStream);
            }
            catch (SerializationException ex)
            {
                throw new SerializationException($"Error deserializing data from file {filePath}", ex);
            }
        }

        /// <summary>
        /// Событие, возникающее после сохранения данных.
        /// </summary>
        public event EventHandler DataSaved;
        
        /// <summary>
        /// Событие, возникающее при удалении данных.
        /// </summary>
        public event EventHandler DataRemoved;
        
        /// <summary>
        /// Метод для сохранения данных в файл.
        /// </summary>
        public void SaveData()
        {
            var json = JsonConvert.SerializeObject(WeatherData);
            File.WriteAllText("data.json", json);
            DataSaved?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Метод для удаления данных из файла.
        /// </summary>
        public void RemoveData(string cityName)
        {
            WeatherData.Remove(cityName);
            var json = JsonConvert.SerializeObject(WeatherData);
            File.WriteAllText("data.json", json);
            DataRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}