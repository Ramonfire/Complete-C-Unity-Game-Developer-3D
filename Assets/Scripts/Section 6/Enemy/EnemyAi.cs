using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    NavMeshAgent navAgent;
    [SerializeField] Transform player;
    [SerializeField] float range = 5f;
    bool isProvoked;
 
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
            if (isProvoked)
                EngageThePlayer();
            else if (Vector3.Distance(player.position, transform.position) <= 20)
            {
                isProvoked = true;
            }
                
        }
        else
            Debug.Log("No Player Found");
    }

    private void EngageThePlayer()
    {
        //if we arent close enough to the enemy then track him. else attack
        if (Vector3.Distance(player.position, transform.position) >= navAgent.stoppingDistance ) 
        {
            HeadTowardsThePlayer(player.position);
        }
        else
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("hit player");
    }

    private void HeadTowardsThePlayer(Vector3 playerPos)
    {
        navAgent.SetDestination(playerPos);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void Provoke() { isProvoked = true; }
}
