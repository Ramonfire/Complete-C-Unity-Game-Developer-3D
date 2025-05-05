using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int StartCoords;
    [SerializeField] Vector2Int EndCoords;

    Node StartNode;
    Node EndNode;
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

        StartNode = gridManager.GetNode(StartCoords);
        EndNode = gridManager.GetNode(EndCoords);
        GetNewPath();

    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
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


    void BreadthFirstSearch() 
    {
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(StartNode);
        reached.Add(StartCoords,StartNode);

        while (frontier.Count > 0 && isRunning) 
        {
            CurrentSearchNode = frontier.Dequeue();
            CurrentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (CurrentSearchNode.coordinates == EndCoords)
                isRunning = false;  
        }
    }


    public List<Node> BuildPath() 
    {
        List<Node> path = new();
        Node currentNode = EndNode;

        path.Add(EndNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {

            currentNode = currentNode.connectedTo;

            path.Add(EndNode);
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
}
