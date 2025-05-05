using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static log4net.Appender.ColoredConsoleAppender;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("Should match the unity editor grid size")]
    [SerializeField] int unityGridSize =10;
    public int UnityGridSize { get { return unityGridSize; } }
    Dictionary<Vector2Int, Node> grid=new();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }


    public Node GetNode(Vector2Int coords) 
    {
        return grid.ContainsKey(coords) ? grid[coords] : null;
    }
    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                grid.Add(coords, new Node(coords, true));
            }
        }

    }

    public void BlockNode(Vector2Int coords) 
    {
        if(grid.ContainsKey(coords))
        {
            grid[coords].isValidPath = false;
        }
    
    }


    public void ResetNodes() 
    {
        foreach (KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.ConnectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    
    }
    //turns World position to coordinates understood by our node system
    public Vector2Int GetCoordsFromPosition(Vector3 position)
    {
        Vector2Int Coordinates= new Vector2Int();

        Coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        Coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return Coordinates;

    }

    public Vector3 GetPositionFromCoords(Vector2Int coords)
    {
        Vector3 position = new Vector3();

        position.x = Mathf.RoundToInt(coords.x * unityGridSize);
        position.y = Mathf.RoundToInt(coords.y * unityGridSize);

        return position;
    }
}
