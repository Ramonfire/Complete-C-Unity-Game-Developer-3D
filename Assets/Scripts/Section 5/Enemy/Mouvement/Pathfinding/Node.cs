using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node 
{
    public Vector2Int coordinates;
    public bool isValidPath=false;
    public bool isExplored=false;
    public bool isPath=false;

    public Node ConnectedTo= null;


    public Node() { }
    public Node(Vector2Int coordinates, bool isValidPath)
    {
        this.coordinates = coordinates;
        this.isValidPath = isValidPath;
    }
}
