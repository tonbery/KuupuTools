using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _transform;    
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

    [Header("Collision options")]
    [SerializeField] float _collisionRadius = 0.3f;
    [SerializeField] LayerMask _collisionMask;


    Vector3 _cameraDirection;
    Vector3 _cameraRight;
    Vector3 _cameraUp = Vector3.up;
    Vector2 _input;

    //COLLISION
    Ray _collisionRay;
    RaycastHit _hit;    
    Vector3 _finalCameraPoint;    
    Vector3 _bestPosition;
    Vector3 _targetCenter;

    public Vector3 Forward => _cameraDirection;
    public Vector3 Right => _cameraRight;

    public Transform Transform => _transform;
    public Transform CameraTransform => _cameraTransform;

    private void LateUpdate() {
        UpdatePivotPosition();
        UpdatePivotRotation();
        MoveCameraTarget();
        Collision();
        ApplyCameraPosition();  
    }
    private void Update() {        
        _x += _input.x * Time.deltaTime * CurrentProfile.XSpeed;
        _y = Mathf.Clamp(_y + _input.y * Time.deltaTime * CurrentProfile.YSpeed * (CurrentProfile.InvertY ? -1 : 1), CurrentProfile.MinY, CurrentProfile.MaxY);
    }

    public void SetInput(Vector2 input)
    {
        _input = input;
    }

    void Collision()
    {
        _collisionRay.origin = _targetCenter;
        var d = _targetCenter.Direction(_bestPosition);
        _collisionRay.direction = d;
                
        if (Physics.Raycast(_collisionRay, out _hit, d.magnitude, _collisionMask))
        {
            _finalCameraPoint = _hit.point + (_cameraDirection * _collisionRadius);
            #if UNITY_EDITOR
            Debug.DrawRay(_collisionRay.origin, d, Color.red);
            #endif
        }
        else
        {
            _finalCameraPoint = _bestPosition;
            #if UNITY_EDITOR 
            Debug.DrawRay(_collisionRay.origin, d, Color.green);
            #endif
        }
    }
    void MoveCameraTarget()
    {
        var d = _y < 0 ? CurrentProfile.Distance : CurrentProfile.Distance + Mathf.Lerp(0, CurrentProfile.Height, _y / CurrentProfile.MaxY);
        var h = _y < 0 ? CurrentProfile.Height : Mathf.Lerp(CurrentProfile.Height, 0, _y / CurrentProfile.MaxY);        
       
        _bestPosition = (_transform.position - (_cameraDirection * d)) + (_cameraUp * h);
        _targetCenter = _transform.position + Vector3.up;
    }

    void ApplyCameraPosition()
    {       
        _cameraTransform.position = _finalCameraPoint;
    }
    void UpdatePivotRotation()
    {            
        _cameraDirection = Vector3.forward.RotateAroundY(_x);
        _cameraRight = -Vector3.Cross(_cameraDirection, Vector3.up);
        _cameraDirection = _cameraDirection.Rotate(_cameraRight, _y);
        _cameraTransform.forward = _cameraDirection;
    }

    void UpdatePivotPosition()
    {
        if (_target != null) _transform.position = _target.position;
    }


#if UNITY_EDITOR   
    [Button] public void SetupCameraObjects()
    {
        foreach (var item in transform.GetComponentsInChildren<Transform>())
        {
            if(item != null && item != transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }

        _transform = transform;

        _camera = new GameObject("Camera", typeof(Camera)).GetComponent<Camera>();
        _cameraTransform = _camera.transform;
        _cameraTransform.localPosition = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        var RP = transform.position + (Vector3.up * 2);
        Debug.DrawRay(RP, _cameraDirection, Color.blue);
        Debug.DrawRay(RP, _cameraRight, Color.green);
        
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(_finalCameraPoint, _collisionRadius);
        Gizmos.color = new Color(0, 1, 0, 0.7f);
        Gizmos.DrawSphere(_bestPosition, 0.3f);
        Gizmos.color = new Color(0, 0, 1, 0.7f);
        Gizmos.DrawSphere(_targetCenter, 0.3f);
        
        Gizmos.color = Color.white;
    }

#endif

}
