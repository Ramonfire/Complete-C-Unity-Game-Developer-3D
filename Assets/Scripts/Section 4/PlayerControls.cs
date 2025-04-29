using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    [Header("Input settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction Fire;

    [Header("Movement Settings")]
    [Tooltip("how fast the ship moves left to right")][SerializeField] float HorizontalThrustFactor=3;
    [Tooltip("how fast the speed moves up and down")][SerializeField] float VerticalThrustFactor=3;
    [Tooltip("bring the nose of the ship up or down")][SerializeField] float pitchFactor = -15f;
    [Tooltip("bring the nose of the ship left or right")][SerializeField] float yawFactor = 25f;
    [Tooltip("roll the ship left of right")][SerializeField] float rollFactor = 5f;
                     float xAxis;
                     float yAxis;
    [Tooltip("how far will the ship pitch")][SerializeField] float RotationHorizontalFactor = -5f;
    [Tooltip("how will the ship roll")][SerializeField] float RotationVerticalFactor = -5f;

    [Header("Weapons")]
    [SerializeField] GameObject[] Lasers;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        movement.Enable();
        Fire.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        Fire.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (Fire.ReadValue<float>()>0f)
        {
            ShootLasers();
        }
        else
        {
            DisableLaser();
        }
    }

    private void ShootLasers()
    {
        foreach(GameObject laser in Lasers)
        {
            SetEmitionStatus(laser,true);
        }
    }
    private void DisableLaser()
    {
        foreach (GameObject laser in Lasers)
        {
            SetEmitionStatus(laser, false);
        }
    }

    private static void SetEmitionStatus(GameObject laser,bool status)
    {
        var emission = laser.GetComponent<ParticleSystem>().emission;
        emission.enabled = status;
    }


    private void ProcessRotation()
    {

        float pitchDueToPosition = (transform.localPosition.y * pitchFactor);
        float pitchDueToMovement = (yAxis * RotationVerticalFactor);


        float pitch = pitchDueToPosition + pitchDueToMovement;
        float yaw = (transform.localPosition.x *yawFactor); // yaw is due to position to keep pointing to the sides
        float roll = (xAxis*RotationHorizontalFactor)*rollFactor;// roll is due to our movement (roll left to go left/ right to go right)

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessTranslation()
    {
         xAxis = movement.ReadValue<Vector2>().x;
         yAxis = movement.ReadValue<Vector2>().y;

        float newXPosition = transform.localPosition.x + (xAxis * Time.deltaTime * HorizontalThrustFactor);
        float newYPosition = transform.localPosition.y + (yAxis * Time.deltaTime * VerticalThrustFactor);

        float clampedXPos = Mathf.Clamp(newXPosition, -1.5f, 1.5f);
        float clampedYPos = Mathf.Clamp(newYPosition, -0.5f, 1.2f);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
