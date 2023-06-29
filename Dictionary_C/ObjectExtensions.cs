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
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Получает объект типа T из массива байтов.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="data">Массив байтов, представляющий сериализованные данные объекта.</param>
        /// <returns>Объект типа T.</returns>
        public static T GetObject<T>(this byte[] data)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(data))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}