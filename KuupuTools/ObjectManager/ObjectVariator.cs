using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ObjectVariator : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0,360)] Vector2 _rotation = new Vector2(0,360);
    [SerializeField, MinMaxSlider(0.1f, 2)] Vector2 _size = new Vector2(0.95f, 1.05f);
     


    [Button]
    public void RandomRotate()
    {
        var objs = GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if(item.parent == transform)
            {

                var euler = item.localEulerAngles;
                euler.y = _rotation.RandomBetweenXY();
                item.localEulerAngles = euler;
            }
        }
    }

    [Button]
    public void RandomSize()
    {
        var objs = GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if (item.parent == transform)
            {
                var random = _size.RandomBetweenXY();
                var size = Vector3.one * random;                
                item.localScale = size;
            }
        }
    }

    [Button]
    public void ResetScale()
    {
        var objs = GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if (item.parent == transform)
            {
                item.localScale = Vector3.one;
            }
        }
    }
}
