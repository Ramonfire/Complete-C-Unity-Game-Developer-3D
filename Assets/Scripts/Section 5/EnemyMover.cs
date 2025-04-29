using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new();//simplified for new List<Waypoint>()
    [SerializeField] float waitTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        FollowPath();//follow Path when you wake up
    }

    private void Update()
    {
        
    }

    private void FollowPath()
    {
            StartCoroutine(MoveToNextTile());
    }

    private IEnumerator MoveToNextTile()
    {
        foreach (WayPoint waypoint in path)
        {
            transform.position = waypoint.transform.position;// move the enemy to the next tile
            yield return new WaitForSeconds(waitTime);
        }
    }
}
