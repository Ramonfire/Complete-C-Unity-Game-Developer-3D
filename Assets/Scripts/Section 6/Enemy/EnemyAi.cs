using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    NavMeshAgent navAgent;
    [SerializeField] Transform player;
    [SerializeField] float range = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.position, transform.position) <= 20)
                SetDestination(player.position);
            else
                SetDestination(transform.position);
        }
        else
            Debug.Log("No Player Found");
    }

    private void SetDestination(Vector3 playerPos)
    {
        navAgent.SetDestination(playerPos);
    }
}
