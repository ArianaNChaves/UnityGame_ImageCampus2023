using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentPosition.x -= 1;
            transform.position = currentPosition;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentPosition.x += 1;
            transform.position = currentPosition;
        } 
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentPosition.y += 1;
            transform.position = currentPosition;
        } 
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentPosition.y -= 1;
            transform.position = currentPosition;
        } 
    }
}
