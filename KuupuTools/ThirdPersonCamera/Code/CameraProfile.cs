using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    float _trasitionTime = 1f;

    public CameraProfile(CameraProfile profile)
    {
        _height = profile.Height;
        _distance = profile.Distance;
        _xSpeed = profile.XSpeed;
        _ySpeed = profile.YSpeed;
        _invertY = profile.InvertY;
        _minY = profile.MinY;
        _maxY = profile.MaxY;
    }

    public void ChangeProfile(CameraProfile profile)
    {
        DOTween.To(() => _height, x => _height = x, profile.Height, _trasitionTime);
        DOTween.To(() => _distance, x => _distance = x, profile.Distance, _trasitionTime);
        DOTween.To(() => _xSpeed, x => _xSpeed = x, profile.XSpeed, _trasitionTime);
        DOTween.To(() => _ySpeed, x => _ySpeed = x, profile.YSpeed, _trasitionTime);
        DOTween.To(() => _minY, x => _minY = x, profile.MinY, _trasitionTime);
        DOTween.To(() => _maxY, x => _maxY = x, profile.MaxY, _trasitionTime);
        _invertY = profile.InvertY;
    }

    public float Height => _height; 
    public float Distance => _distance;

    public float XSpeed => _xSpeed; 
    public float YSpeed => _ySpeed; 
    public bool InvertY => _invertY;

    public float MinY => _minY;
    public float MaxY => _maxY;
}
