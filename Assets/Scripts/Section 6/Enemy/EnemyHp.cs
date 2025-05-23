using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyAi))]
public class EnemyHp : MonoBehaviour
{
    [SerializeField] int maxHp=20;
    [SerializeField] GameObject[] ammo;
    [SerializeField]int ammoIndex ;
    int currentHp;

    // Start is called before the first frame update
    void Awake()
    {
        currentHp = maxHp;
        ammoIndex = UnityEngine.Random.Range(0, 4);
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
        gameObject.SetActive(false);
        if (ammo[ammoIndex] != null)
            Instantiate(ammo[ammoIndex], transform.position, ammo[ammoIndex].transform.rotation);
    }

    public void ReceiveDamage(int inDamage)
    {
        BroadcastMessage("Provoke");
      //  gameObject.GetComponent<EnemyAi>().Provoke();
        currentHp -= inDamage;
    }
}
