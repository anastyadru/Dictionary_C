using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dictionary_C
{
    /// <summary>
    /// Расширения для объектов.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Получает массив байтов, представляющий сериализованные данные объекта.
        /// </summary>
        /// <param name="data">Сериализуемый объект.</param>
        /// <returns>Массив байтов, представляющий сериализованные данные объекта.</returns>
        public static byte[] GetBytes(this object data)
        {
            
            BinaryFormatter formatter = new BinaryFormatter(); // cоздан объект BinaryFormatter для сериализации данных
            using MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, data); 
                
            // сериализован объект data в поток stream с помощью метода Serialize объекта formatter
                
            return stream.ToArray(); // возвращен массив байт из потока stream с помощью метода ToArray()
        }

        /// <summary>
        /// Метод для преобразования словаря погодных данных в массив байтов.
        /// </summary>
        /// <param name="weatherData">Словарь погодных данных.</param>
        /// <returns>Массив байтов.</returns>
        public static byte[] ToByteArray(ObservableDictionary<string, WeatherData> weatherData)
        {
            throw new System.NotImplementedException();
        }
    }
}