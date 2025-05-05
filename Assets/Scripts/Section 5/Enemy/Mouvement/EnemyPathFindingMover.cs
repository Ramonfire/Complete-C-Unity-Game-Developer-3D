using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyPathFindingMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new();//simplified for new List<Waypoint>()
    [SerializeField][Range(0f, 5f)] float Speed = 0.5f;
    Enemy enemy;


    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindPath()
    {
        path.Clear();
     

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

        foreach (Tile waypoint in path)
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
        enemy.Penalize();
        gameObject.SetActive(false);
    }
}
