using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offset;
    [SerializeField] bool _verticalMovement;
    Vector3 _cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        _cameraPosition = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, transform.position.z);
        _cameraPosition.y = _verticalMovement ? _cameraPosition.y : 0;
        transform.position = _cameraPosition;
    }
}
