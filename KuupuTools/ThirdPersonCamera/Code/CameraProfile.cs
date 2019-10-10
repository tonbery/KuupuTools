using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraProfile
{
    [SerializeField] bool _snapToTarget;

    [SerializeField] float _height = 2;
    [SerializeField] float _distance = 4;

    public float Height => _height; 
    public float Distance => _distance;
}
