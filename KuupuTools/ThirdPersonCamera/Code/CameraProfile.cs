using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera Profile", menuName = "Kuupu/Third Person Camera/Camera Profile", order = 1)]
public class CameraProfile : ScriptableObject
{
    [SerializeField] bool _snapToTarget;


    [SerializeField] float _height = 2;
    [SerializeField] float _distance = 4;

    public float Height => _height; 
    public float Distance => _distance;
}
