using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera _cameraSize;

    // Start is called before the first frame update
    void Start()
    {
        _cameraSize = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -20);
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _cameraSize.orthographicSize += 1;
        } 
        if (Input.GetKeyDown(KeyCode.E))
        {
            _cameraSize.orthographicSize -= 1;
        } 
    }
}
