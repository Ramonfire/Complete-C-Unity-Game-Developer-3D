using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyPathFindingMover : MonoBehaviour
{
   
    [SerializeField][Range(0f, 5f)] float Speed = 0.5f;
    Enemy enemy;
    List<Node> path = new();//simplified for new List<Waypoint>()
    PathFinder pathFinder;
    GridManager gridManager;
    private void OnEnable()
    {
        FindPath();
        JumpToStartingTile();
        FollowPath();
    }

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        pathFinder = FindObjectOfType<PathFinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindPath()
    {
        path.Clear();
        if(pathFinder!=null)
            path = pathFinder.GetNewPath();
    }

    private void JumpToStartingTile()
    {
        if(gridManager!=null)
            transform.position = gridManager.GetPositionFromCoords(pathFinder.StartingCoords);
    }

    private void FollowPath()
    {
        StartCoroutine(MoveToNextTile());
    }


    private IEnumerator MoveToNextTile()
    {
        if (gridManager == null)
            yield return null;

      for(int i = 0; i < path.Count;i++)
        {
            Vector3 StartPosition = transform.position;
            Vector3 EndPosition = gridManager.GetPositionFromCoords(path[i].coordinates);
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
