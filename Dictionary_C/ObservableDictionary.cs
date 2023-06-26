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
            DataSaved?.Invoke(this, EventArgs.Empty); // Вызов события DataSaved
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
                ItemAdded?.Invoke(this, new KeyValuePair<TKey, TValue>(key, value)); // вызвано событие ItemAdded и передан в него добавленный элемент
                Cache_ItemAdded(this, new KeyValuePair<TKey, TValue>(key, value)); // вызов метода Cache_ItemAdded для сохранения элемента в кэше
                SaveData(); // вызов метода SaveData для сохранения данных
            }
        }

        /// <summary>
        /// Удаляет элемент с указанным ключом из словаря.
        /// </summary>
        /// <param name="key">Ключ удаляемого элемента.</param>
        /// <returns>Значение true, если элемент был успешно удален из словаря; в противном случае — значение false.</returns>
        public new bool Remove(TKey key)
        {
            if (_cache.ContainsKey(key))
            {
                var result = base.Remove(key);
                SaveData(); // вызван метод SaveData после удаления элемента из словаря
                return result;
            }
            
            return false;
        }
        
        /// <summary>
        /// Метод-обработчик события ItemAdded.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void Cache_ItemAdded(object sender, KeyValuePair<TKey, TValue> e)
        {
            Console.WriteLine($"Добавлен элемент с ключом {e.Key} и значением {e.Value}");
        }
        
        /// <summary>
        /// Метод-обработчик события ItemRemoved.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void Cache_ItemRemoved(object sender, KeyValuePair<TKey, TValue> e)
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
