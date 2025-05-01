using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new();//simplified for new List<Waypoint>()
    [SerializeField][Range(0f,5f)] float Speed = 0.5f;



    // OnEnable called everytime the object s enabled
    void OnEnable()
    {
        FindPath();
        JumpToStartingTile();//move the object to the starting tile
        FollowPath();//follow Path when you wake up
    }

    private void Update()
    {

    }

    private void FindPath() 
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");//get the parent Object with the path tag to ensure we keep the correct order of our objects

        foreach (Transform waypoint in parent.transform)// for each child in the transform(transform contains the children(sounds fucked up))
        {
            path.Add(waypoint.GetComponent<WayPoint>());//fill our list
        }
    
    }
 
    private void JumpToStartingTile() 
    {
        transform.position = path[0].transform.position;
    }

    private void FollowPath()
    {
            StartCoroutine(MoveToNextTile());
    }

    private IEnumerator MoveToNextTile()
    {

        foreach (WayPoint waypoint in path)
        {
            Vector3 StartPosition = transform.position;
            Vector3 EndPosition = waypoint.transform.position;
            float TravelPercent = 0f;
            transform.LookAt(EndPosition);//face where you are going
            while (TravelPercent < 1f)
            {
                TravelPercent += Time.deltaTime * Speed;
                transform.position = Vector3.Lerp(StartPosition, EndPosition, TravelPercent);
                yield return new WaitForEndOfFrame();
            }

        }
        ArrivedToTarget();
    }

    private void ArrivedToTarget()
    {
        gameObject.SetActive(false);
    }
}
