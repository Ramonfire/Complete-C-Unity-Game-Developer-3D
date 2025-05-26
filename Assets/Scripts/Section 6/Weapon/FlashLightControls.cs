using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControls : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject flashLight;

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            flashLight.SetActive(!flashLight.activeSelf);
    }
}
