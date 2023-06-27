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
            
            var formatter = new BinaryFormatter(); // cоздан объект BinaryFormatter для сериализации данных
            using var stream = new MemoryStream();
            formatter.Serialize(stream, data); 
                
            // сериализован объект data в поток stream с помощью метода Serialize объекта formatter
                
            return stream.ToArray(); // возвращен массив байт из потока stream с помощью метода ToArray()
        }

        /// <summary>
        /// Получает объект типа T из массива байтов.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="data">Массив байтов, представляющий сериализованные данные объекта.</param>
        /// <returns>Объект типа T.</returns>
        public static T GetObject<T>(this byte[] data)
        {
            var formatter = new BinaryFormatter(); // cоздан объект BinaryFormatter для десериализации данных
            using var stream = new MemoryStream(data);
            return (T)formatter.Deserialize(stream); 
            // десериализован объект из потока stream с помощью метода Deserialize объекта formatter и приведен к типу T
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