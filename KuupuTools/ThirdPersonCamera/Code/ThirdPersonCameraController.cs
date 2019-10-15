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

    [Header("Extra Options")]
    [SerializeField] bool _useSimpleInput;
    [SerializeField] string _horizontalAxys = "Horizontal";
    [SerializeField] string _verticalAxys = "Vertical";



    //INTERNAL
    Vector3 _pivotRotation;
    Vector3 _cameraPosition;
    
    private void LateUpdate() {
        UpdatePivotPosition();
        UpdatePivotRotation(); 
        UpdateCameraPosition();  
    }
    private void Update() {
        if(_useSimpleInput){                       
            
            _x += Input.GetAxis(_horizontalAxys) * Time.deltaTime * CurrentProfile.XSpeed;
            _y = Mathf.Clamp(_y + Input.GetAxis(_verticalAxys) * Time.deltaTime * CurrentProfile.YSpeed * (CurrentProfile.InvertY ? -1 : 1), CurrentProfile.MinY, CurrentProfile.MaxY);
            
        }
    }

    void UpdateCameraPosition()
    {
        _cameraPosition.y = _y < 0 ? CurrentProfile.Height : Mathf.Lerp(CurrentProfile.Height, 0, _y /CurrentProfile.MaxY);
        _cameraPosition.z =  -(_y < 0 ? CurrentProfile.Distance : CurrentProfile.Distance + Mathf.Lerp(0, CurrentProfile.Height, _y / CurrentProfile.MaxY));
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
        if (_pivot == null || _camera == null || Application.isPlaying) return;

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
