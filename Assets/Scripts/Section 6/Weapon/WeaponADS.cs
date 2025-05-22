using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponADS : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;

    [SerializeField] float adsFOV = 20f;
    [SerializeField] float defaultFOV = 40f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (mainCamera != null)
        {
            if (Input.GetButton("Fire2"))
            {
                mainCamera.m_Lens.FieldOfView = adsFOV;
            }
            else
            {
                mainCamera.m_Lens.FieldOfView = defaultFOV;
            }
        }
    }
}
