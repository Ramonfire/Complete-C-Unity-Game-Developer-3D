using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable  =false;//modify it from the edit mode
    public bool IsPlaceable { get { return isPlaceable; } }


    bool isOccupied;
    public bool IsOccupied { get { return isOccupied; } }

    [SerializeField] Tower BallistaPrefab;
    [SerializeField] GameObject BallistaObject;
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
        else if (isPlaceable && !isOccupied && Input.GetMouseButtonDown(0)) // if i can place objects into an unoccupied tile and a click action is triggered
        {
            PlaceBallista();
        }


    }

    private void PlaceBallista()
    {
        BallistaObject = BallistaPrefab.CreateBallista(BallistaPrefab,transform.position);
        isOccupied = BallistaObject!=null;//if the object isnt null then we are occupied
    }

  

    private void UpgradeBallista()
    {
            return;
    }

}
