using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(EnemyAi))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyHp : MonoBehaviour
{
    [SerializeField] int maxHp=20;
    [SerializeField] GameObject[] ammo;
    [SerializeField]int ammoIndex ;
    int currentHp;
    Animator animator;
    bool isDead=false;

    // Start is called before the first frame update
    void Awake()
    {
        currentHp = maxHp;
        ammoIndex = UnityEngine.Random.Range(0, 4);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (currentHp <= 0 ) 
        {
            Die();
        }
    }

    private void Die()
    {

        if (isDead)
            return;
        GetComponent<EnemyAi>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<NavMeshAgent>().SetDestination(transform.position);//stop the ennemie from moving
        animator.SetTrigger("die");
        Vector3 ammoSpawn = transform.position + (Vector3.up*0.5f);
        if (ammo[ammoIndex] != null)
            Instantiate(ammo[ammoIndex], ammoSpawn, ammo[ammoIndex].transform.rotation);
        transform.name = "dead";
        isDead = true;
    }

    public void ReceiveDamage(int inDamage)
    {
        BroadcastMessage("Provoke");
      //  gameObject.GetComponent<EnemyAi>().Provoke();
        currentHp -= inDamage;
    }
}
