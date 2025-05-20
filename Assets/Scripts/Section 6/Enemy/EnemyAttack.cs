using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float damage = 40f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void DamagePlayerEvent()
    {
        if (player == null)
            return;

        Debug.Log("Attacking the player");
    }
}
