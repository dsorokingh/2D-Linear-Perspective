using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();     
        
        _camera.transform.position = new Vector3(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize, _camera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
