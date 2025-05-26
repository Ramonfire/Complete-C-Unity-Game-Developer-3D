using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowEndGame : MonoBehaviour
{
    [SerializeField]Canvas EndCanvas;
    // Start is called before the first frame update
    void Start()
    {
        EndCanvas.enabled = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        ShowUI();
    }


    private void TurnOnCursor()
    {
        FindObjectOfType<PlayerInput>().enabled = false;//disable player input
        Time.timeScale = 0;//freeze time
        FindObjectOfType<WeaponSelector>().enabled = false;
        Cursor.lockState = CursorLockMode.None;// unlock the cursor
        Cursor.visible = true;//show the cursor
    }


    private void ShowUI()
    {
        EndCanvas.enabled = true;
        TurnOnCursor();
    }
}
