using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyAi))]
public class EnemyHp : MonoBehaviour
{
    [SerializeField] int maxHp=20;
    int currentHp;

    // Start is called before the first frame update
    void Awake()
    {
        currentHp = maxHp;
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
    }

    public void ReceiveDamage(int inDamage)
    {
        gameObject.GetComponent<EnemyAi>().Provoke();
        currentHp -= inDamage;
    }
}
