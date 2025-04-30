using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new();//simplified for new List<Waypoint>()
    [SerializeField][Range(0f,5f)] float Speed = 0.5f;
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
            Vector3 StartPosition = transform.position;
            Vector3 EndPosition = waypoint.transform.position;
            float TravelPercent = 0f;
            transform.LookAt(EndPosition);//face where you are going
            while (TravelPercent < 1f)
            {
                TravelPercent += Time.deltaTime*Speed;
                transform.position = Vector3.Lerp(StartPosition, EndPosition, TravelPercent);
                yield return new WaitForEndOfFrame();
            }
          
        }
    }
}
