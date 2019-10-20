using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleBag<T> {
    List<T> _bag;
    public bool CanDraw => _bag.Count > 0;
    public ShuffleBag(List<T> Elements)
    {
        _bag = new List<T>(Elements);
    }
    public ShuffleBag(T[] Elements)
    {
        _bag = new List<T>(Elements);
    }

    /// <summary>
    /// Draws a value from the suffle bag, please check CanDraw before drawing.
    /// </summary>
    public T Draw()
    {
        var random = Random.Range(0, _bag.Count);
        var element = _bag[random];
        _bag.Remove(element);       

        return element;
    }
}
