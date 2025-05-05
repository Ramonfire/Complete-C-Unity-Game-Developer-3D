using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable  =false;//modify it from the edit mode
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField]  public bool isValidPath=false;
    bool isOccupied;
    public bool IsOccupied { get { return isOccupied; } }

    [SerializeField] Tower BallistaPrefab;
    [SerializeField] GameObject BallistaObject;
    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coords=new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
        if (gridManager != null) 
        {
            coords = gridManager.GetCoordsFromPosition(transform.position);
            if (!isValidPath)
                gridManager.BlockNode(coords);
        }
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
            if (gridManager != null && pathFinder!=null)
            { 
                if(gridManager.GetNode(coords).isValidPath && !pathFinder.WillBlockPath(coords)) 
                {
                    PlaceBallista();
                }
                    
            }
            else 
            {
                PlaceBallista();
            }
              
        }


    }

    private void PlaceBallista()
    {
        BallistaObject = BallistaPrefab.CreateBallista(BallistaPrefab,transform.position);
        isOccupied = (BallistaObject!=null);//if the object isnt null then we are occupied nd we should block this grid
        if (isOccupied)
            gridManager.BlockNode(coords);

    }

  

    private void UpgradeBallista()
    {
            return;
    }

}
