using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMouseLook : MonoBehaviour
{
    [SerializeField]
    private float Sensitivity = 1f;
    
    private float _x;
    private float _y;
    const float MIN_X = 0.0f;
    const float MAX_X = 360.0f;
    const float MIN_Y = -90.0f;
    const float MAX_Y = 90.0f;
 
    
 
    void Awake()
    {
        var euler = transform.rotation.eulerAngles;
        _x = euler.x;
        _y = euler.y;
    }
 
    void Update()
    {
        
 
        _x -= Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);
        if (_x < MIN_X) _x += MAX_X;
        else if (_x > MAX_X) _x -= MAX_X;
        _y += Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);
        if (_y < MIN_Y) _y = MIN_Y;
        else if (_y > MAX_Y) _y = MAX_Y;
 
        transform.rotation = Quaternion.Euler(-_y, -_x, 0.0f);
    }
}
