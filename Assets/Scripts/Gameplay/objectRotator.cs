using System;
using UnityEngine;

public class objectRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private bool _rotateClockwise = true;
    
    // Update is called once per frame
    void Update()
    {
        if (_rotateClockwise)
        {
            transform.Rotate(Vector3.back * (_rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime));
        }
    }
    
    public void SetRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
    }
    
    public void SetRotationDirection(bool clockwise)
    {
        _rotateClockwise = clockwise;
    }
}