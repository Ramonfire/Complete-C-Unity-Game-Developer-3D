using Cinemachine;
using UnityEngine;
using StarterAssets;

public class WeaponADS : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;

    [SerializeField] float adsFOV = 20f;
    [SerializeField] float defaultFOV = 40f;

    FirstPersonController controller;
    [SerializeField] float zoomoutSens = 2f;
    [SerializeField] float zoominSens = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if (mainCamera != null)
        {
            if (Input.GetButton("Fire2"))
            {
                mainCamera.m_Lens.FieldOfView = adsFOV;
                controller.RotationSpeed = zoominSens;
            }
            else
            {
                mainCamera.m_Lens.FieldOfView = defaultFOV;
                controller.RotationSpeed = zoomoutSens;
            }
        }
    }
}
