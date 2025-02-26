using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Int32 _thrustFactor ;
    [SerializeField] private Int32 _rotationFactor ;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip EngineSound;
    // Start is called before the first frame update
    void Start()
    {
        _thrustFactor = 300;
        _rotationFactor = 100;
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
        _rigidbody.freezeRotation = true;// freeze rotation to avoid glitching due to the physics system and us trying to manually rotate the object
         transform.Rotate(Vector3.back * _rotationFactor * Time.deltaTime);
        _rigidbody.freezeRotation = false;//unfreeze the rotation
    }

    private void SpaceKeyDown()
    {
           
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up* _thrustFactor* Time.deltaTime);
            if(!_audioSource.isPlaying)
                 _audioSource.PlayOneShot(EngineSound);
        }
        else
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }
}
