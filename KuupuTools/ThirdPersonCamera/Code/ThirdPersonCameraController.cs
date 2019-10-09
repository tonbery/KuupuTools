using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _trasform;
    [SerializeField] Transform _pivot;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Camera _camera;

    [Header("Profiles")]
    [SerializeField] List<CameraProfile> _profiles = new List<CameraProfile>();
    CameraProfile CurrentProfile => _profiles[_currentProfileIndex];

    [Header("Runtime properties")]
    [SerializeField] Transform _target;

    [SerializeField] int _currentProfileIndex;

    [SerializeField] float _x;
    [SerializeField, Range(-90,90)] float _y;

    Vector3 _pivotRotation;
    Vector3 _cameraPosition;
    

    void UpdateCameraPosition()
    {
        _cameraPosition.y = CurrentProfile.Height;
        _cameraPosition.z = -CurrentProfile.Distance;
        _cameraTransform.localPosition = _cameraPosition; 
    }
    void UpdatePivotRotation()
    {
        _pivotRotation.y = _x;
        _pivotRotation.x = _y;
        _pivot.eulerAngles = _pivotRotation;
    }

    void UpdatePivotPosition()
    {
        if (_target != null) _trasform.position = _target.position;
    }




#if UNITY_EDITOR

    private void OnValidate()
    {
        if (_pivot == null || _camera == null) return;

        UpdateCameraPosition();
        UpdatePivotRotation();
        UpdatePivotPosition();
    }
    [Button] public void SetupCameraObjects()
    {
        foreach (var item in transform.GetComponentsInChildren<Transform>())
        {
            if(item != null && item != transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }

        _trasform = transform;

        _pivot = new GameObject("Camera Pivot").transform;
        _pivot.SetParent(_trasform);
        _pivot.localPosition = Vector3.zero;

        _camera = new GameObject("Camera", typeof(Camera)).GetComponent<Camera>();
        _cameraTransform = _camera.transform;
        _cameraTransform.SetParent(_pivot);
        _cameraTransform.localPosition = Vector3.zero;
    }

#endif

}
