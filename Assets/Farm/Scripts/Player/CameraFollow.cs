using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; 
    [SerializeField] private Vector3 _shiftPosition;
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _shiftPosition + _target.position;
        transform.LookAt(_target);

    }
}
