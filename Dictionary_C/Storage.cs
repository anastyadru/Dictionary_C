using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
            
            var bytes = data.GetBytes(); // сериализован объект в массив байтов с помощью метода GetBytes()
            using var fileStream = new FileStream(filePath, FileMode.Create);
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, bytes); // вызван метод Serialize() для записи данных в файл 
        }

        /// <summary>
        /// Загружает объект из файла по указанному пути.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="filePath">Путь к файлу, из которого нужно загрузить объект.</param>
        /// <returns>Загруженный объект типа T.</returns>
        public static T Load<T>(string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            var binaryFormatter = new BinaryFormatter();

            // создан экземпляр класса BinaryFormatter для десериализации данных из потока FileStream

            var bytes = (byte[])binaryFormatter.Deserialize(fileStream); // вызван метод Deserialize() для получения объекта типа T из файла
                
            if (bytes == null)
            {
                throw new SerializationException("Failed to deserialize object from file.");
            }
                
            return bytes.GetObject<T>();
        }

        /// <summary>
        /// Метод для получения объекта из массива байтов.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="bytes">Массив байтов.</param>
        /// <returns>Объект заданного типа.</returns>
        private static T GetObject<T>(this byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            var binaryFormatter = new BinaryFormatter();
            try
            {
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
            catch (SerializationException ex)
            {
                throw new SerializationException("Failed to deserialize object", ex);
            }
        }

        /// <summary>
        /// Событие, возникающее после сохранения данных.
        /// </summary>
        public event EventHandler DataSaved;
        
        /// <summary>
        /// Метод для сохранения данных в файл.
        /// </summary>
        public void SaveData()
        {
            var data = ObjectExtensions.ToByteArray(WeatherData);
            File.WriteAllBytes("data.bin", data); // сохранение данных
            DataSaved?.Invoke(this, EventArgs.Empty); // вызов события
        }
    }
}