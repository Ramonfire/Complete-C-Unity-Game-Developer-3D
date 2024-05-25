using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Int32 _thrustFactor ;
    [SerializeField] private Int32 _rotationFactor ;
    // Start is called before the first frame update
    void Start()
    {
        _thrustFactor = 300;
        _rotationFactor = 100;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessUserInput();
      
    }

    private void ProcessUserInput()
    {
        SpaceKeyDown();
        DirectionalKeysDown();  
    }

    private void DirectionalKeysDown()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateObject(_rotationFactor);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateObject(-_rotationFactor);
        };
    }

    private void RotateObject(Int32 _rotationFactor)
    {
        transform.Rotate(Vector3.back * _rotationFactor * Time.deltaTime);
    }

    private void SpaceKeyDown()
    {
           
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up* _thrustFactor* Time.deltaTime); 
        }
    }
}
