using System;
using System.Collections.Generic;

namespace Dictionary_C
{
    /// <summary>
    /// Класс, вызывающий события при добавлении и удалении объектов из коллекции.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа словаря.</typeparam>
    /// <typeparam name="TValue">Тип значения словаря.</typeparam>
    public abstract class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        ///<summary>
        /// Кэш, хранящий пары ключ-значение.
        ///</summary>
        ///<typeparam name="TKey">Тип ключа.</typeparam>
        ///<typeparam name="TValue">Тип значения.</typeparam>
        private readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Событие, возникающее при добавлении элемента в словарь.
        /// </summary>
        public event EventHandler<KeyValuePair<TKey, TValue>> ItemAdded; 
        
        /// <summary>
        /// Событие, возникающее при удалении элемента из словаря.
        /// </summary>
        public event EventHandler<KeyValuePair<TKey, TValue>> ItemRemoved;
        
        /// <summary>
        /// Событие, которое вызывается при сохранении данных.
        /// </summary>
        public event EventHandler DataSaved;
        
        /// <summary>
        /// Сохраняет данные и вызывает событие DataSaved.
        /// </summary>
        private void SaveData()
        {
            DataSaved?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// Добавляет элемент с указанным ключом и значением в словарь.
        /// </summary>
        /// <param name="key">Ключ добавляемого элемента.</param>
        /// <param name="value">Значение добавляемого элемента.</param>
        public new void Add(TKey key, TValue value)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, value);
                base.Add(key, value);
                ItemAdded?.Invoke(this, new KeyValuePair<TKey, TValue>(key, value));
                Cache_ItemAdded(this, new KeyValuePair<TKey, TValue>(key, value));
                SaveData();
            }
        }

        /// <summary>
        /// Удаляет элемент с указанным ключом из словаря и вызывает метод SaveData для сохранения изменений.
        /// </summary>
        /// <param name="key">Ключ элемента, который нужно удалить.</param>
        /// <returns>Значение true, если элемент был успешно удален; в противном случае — значение false.</returns>
        public new bool Remove(TKey key)
        {
            var result = base.Remove(key);
            SaveData();
            return result;
        }
        
        /// <summary>
        /// Метод-обработчик события ItemAdded.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private static void Cache_ItemAdded(object sender, KeyValuePair<TKey, TValue> e)
        {
            Console.WriteLine($"Добавлен элемент с ключом {e.Key} и значением {e.Value}");
        }
        
        /// <summary>
        /// Метод-обработчик события ItemRemoved.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private static void Cache_ItemRemoved(object sender, KeyValuePair<TKey, TValue> e)
        {
            Console.WriteLine($"Удален элемент с ключом {e.Key} и значением {e.Value}");
        }
        
        /// <summary>
        /// Конструктор класса ObservableDictionary.
        /// </summary>
        protected ObservableDictionary()
        {
            ItemAdded += Cache_ItemAdded;
            ItemRemoved += Cache_ItemRemoved;
        }

    }
}
