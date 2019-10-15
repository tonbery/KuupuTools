using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraProfile
{
    [SerializeField] bool _snapToTarget;

    [SerializeField] float _height = 2;
    [SerializeField] float _distance = 4;

    [SerializeField] float _xSpeed = 100;
    [SerializeField] float _ySpeed = 100;
    [SerializeField] bool _invertY = true;

    [SerializeField] float _minY = -89;
    [SerializeField] float _maxY =  89;


    public float Height => _height; 
    public float Distance => _distance;

    public float XSpeed => _xSpeed; 
    public float YSpeed => _ySpeed; 
    public bool InvertY => _invertY;

    public float MinY => _minY;
    public float MaxY => _maxY;
}
