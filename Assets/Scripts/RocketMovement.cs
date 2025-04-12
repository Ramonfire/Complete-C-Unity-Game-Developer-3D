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
    [SerializeField] private ParticleSystem _BoosterEffect;
    [SerializeField] private ParticleSystem _LeftBooster;
    [SerializeField] private ParticleSystem _RightBooster;
    // Start is called before the first frame update
    void Start()
    {
        _thrustFactor = 300;
        _rotationFactor = 100;
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _BoosterEffect.Stop();
        _LeftBooster.Stop();
        _RightBooster.Stop();
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
            if (!_RightBooster.isPlaying)
            {
                _LeftBooster.Stop();
                _RightBooster.Play();
            }
            RotateObject(_rotationFactor);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!_LeftBooster.isPlaying)
            {
                _RightBooster.Stop();
                _LeftBooster.Play();
            }
                
            RotateObject(-_rotationFactor);
        }
        else
        {
            if (!_BoosterEffect.isPlaying) //stop the side booster when we arent steering or Climbing
            {
                _RightBooster.Stop();
                _LeftBooster.Stop();
            }
           
        }
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
            _rigidbody.AddRelativeForce(Vector3.up * _thrustFactor * Time.deltaTime);
            if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(EngineSound);

            if (!_BoosterEffect.isPlaying)
                _BoosterEffect.Play();

            if (!_LeftBooster.isPlaying && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
                _LeftBooster.Play();

            if (!_RightBooster.isPlaying && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
                _RightBooster.Play();
        }
        else
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();

            if (_BoosterEffect.isPlaying)
                _BoosterEffect.Stop();
        }
    }
}
