using System.Collections.Generic;

public class TriggerableDictionary <K, T>
{
    Dictionary<K, T> _dictionary;

    public delegate void TDelegate(T element);
    public event TDelegate OnAddElement;
    public event TDelegate OnRemoveElement;

    public int Count => _dictionary.Count;

    T this[K id] { get { return _dictionary[id]; } }

    public TriggerableDictionary()
    {
        _dictionary = new Dictionary<K, T>();
    }

    public void Add(K key, T value)
    {
        if (!_dictionary.ContainsKey(key))
        {
            _dictionary.Add(key, value);
            OnAddElement(value);
        }        
    }

    public void Remove(K key)
    {
        if (_dictionary.ContainsKey(key))
        {
            OnRemoveElement(_dictionary[key]);
            _dictionary.Remove(key);
        }
    }
}
