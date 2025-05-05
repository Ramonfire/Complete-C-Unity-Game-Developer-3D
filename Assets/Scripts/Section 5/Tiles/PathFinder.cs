using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startingCoords;
    public Vector2Int StartingCoords { get { return startingCoords; } }
    [SerializeField] Vector2Int destinationCoords;
    public Vector2Int DestinationCoords { get { return destinationCoords; } }

    Node startingNode;
    Node destinationNode;
    Node CurrentSearchNode;

    Queue<Node> frontier = new();
    Dictionary<Vector2Int, Node> reached = new();


    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

    }

    private void Start()
    {

        startingNode = gridManager.GetNode(startingCoords);
        destinationNode = gridManager.GetNode(destinationCoords);


        GetNewPath();

    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startingCoords);
    }

    public List<Node> GetNewPath(Vector2Int coords)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coords);
        return BuildPath();
    }


    private void ExploreNeighbors()
    {
        List<Node> Neighbors = new();
        foreach (Vector2Int direction in directions)
        {
            Node node = gridManager.GetNode(CurrentSearchNode.coordinates + direction);
            if(node!=null)
                Neighbors.Add(node);
        }


        foreach (Node neighbor in Neighbors)
        {
            if(neighbor.isValidPath && !reached.ContainsKey(neighbor.coordinates)) 
            {
                neighbor.connectedTo=CurrentSearchNode;
                reached.Add(neighbor.coordinates,neighbor);
                frontier.Enqueue(neighbor);  
            }
        }
    }


    void BreadthFirstSearch(Vector2Int coords) 
    {

        startingNode = gridManager.GetNode(coords);
        destinationNode = gridManager.GetNode(destinationCoords);

        startingNode.isValidPath = true;
        destinationNode.isValidPath = true;


        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(startingNode);
        reached.Add(coords,startingNode);

        while (frontier.Count > 0 && isRunning) 
        {
            CurrentSearchNode = frontier.Dequeue();
            CurrentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (CurrentSearchNode.coordinates == destinationCoords)
                isRunning = false;  
        }
    }


    public List<Node> BuildPath() 
    {
        List<Node> path = new();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {

            currentNode = currentNode.connectedTo;

            path.Add(currentNode);
            currentNode.isPath = true;

        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coords) 
    {
        Node node = gridManager.GetNode(coords);
        if (node != null)
        {
            bool TileState = node.isValidPath;
            node.isValidPath = false;
            List<Node> newPath = GetNewPath();
            node.isValidPath = TileState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
            return false;
    }

    public void NotifyReceiver() 
    {
        BroadcastMessage("RecalculatePath",false, SendMessageOptions.DontRequireReceiver);//send the messge and dont care wether someone receives it or no(averge UDP enjoyer)
        
    }
}
