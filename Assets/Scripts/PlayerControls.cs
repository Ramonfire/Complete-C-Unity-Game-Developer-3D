using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction Fire;
    [SerializeField] float HorizontalThrustFactor=2;
    [SerializeField] float VerticalThrustFactor=2;
    [SerializeField] float pitchFactor = -15f;
    [SerializeField] float yawFactor = 25f;
    [SerializeField] float rollFactor = 5f;
                     float xAxis;
                     float yAxis;
    [SerializeField] float RotationHorizontalFactor = -5f;
    [SerializeField] float RotationVerticalFactor = -5f;
 

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
