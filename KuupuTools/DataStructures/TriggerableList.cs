using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableList <T>
{
    List<T> _list;

    public delegate void TDelegate(T element);
    public event TDelegate OnElementAdded;
    public event TDelegate OnElementRemoved;

    public int Count => _list.Count;

    public T this[int id] { get { return _list[id]; } }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _list)
        {            
            yield return item;            
        }
    }

    public TriggerableList()
    {
        _list = new List<T>();
    }

    public void Add(T value)
    {
        _list.Add(value);
        OnElementAdded(value);        
    }

    public void Remove(T value)
    {        
        OnElementRemoved(value);
        _list.Remove(value);        
    }
}
