using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlacable =false;//modify it from the edit mode
    [SerializeField] GameObject BallistaPrefab;
    [SerializeField] GameObject BallistaObject;
    bool isOccupied;
    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void  OnMouseOver()
    {
        if (isOccupied && Input.GetMouseButtonDown(0)) //upgrade the ballista if the tile is alreayd occupied
        {
            UpgradeBallista();
        }
        else if (isPlacable && !isOccupied && Input.GetMouseButtonDown(0)) // if i can place objects into an unoccupied tile and a click action is triggered
        {
            BallistaObject = Instantiate(BallistaPrefab, transform.position, Quaternion.identity);
            isOccupied = true;
        }


    }

    private void UpgradeBallista()
    {
        Debug.Log("Upgrading Ballista");
    }
}
