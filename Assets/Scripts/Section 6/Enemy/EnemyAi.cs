using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    NavMeshAgent navAgent;
    [SerializeField] Transform player;
    [SerializeField] float range = 10f;
    bool isProvoked;
    [SerializeField] float lookSpeed = 5f;

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
            if (Vector3.Distance(player.position, transform.position) <= range)
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
        FaceTarget();
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
        GetComponent<Animator>().SetBool("attack",true);
    }

    private void HeadTowardsThePlayer(Vector3 playerPos)
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navAgent.SetDestination(playerPos);
    }

    private void FaceTarget() 
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*lookSpeed);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void Provoke() { isProvoked = true; }
}
