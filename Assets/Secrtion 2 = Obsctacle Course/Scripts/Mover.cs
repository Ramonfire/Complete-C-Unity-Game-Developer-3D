using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float xSpeed = 7f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float zSpeed = 7f;

    
    void Start()
    {
        PrintInstructions();
    }

    void Update()
    {
        MovePlayer();
    }


    public void PrintInstructions()
    {
        Debug.Log("WASD  or arrows to move \n get to the end without hitting the obstacles");
        Debug.Log(" good luck");

    }

    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime;
        transform.Translate(xValue * xSpeed, 0, zValue * zSpeed);
    }

}
