using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// it means that the engine will automatically add the component to the object it is attached to , since this componenet is neccessary for this program to run
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 5;
    [Tooltip("add to the max health when the nemy dies making it harder to kill on the next respawn")]
    [SerializeField] int DifficultyLevel = 1;
    int currentHitPoints=0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = MaxHitPoints;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            enemy.RewardGold();
            MaxHitPoints += DifficultyLevel;
            gameObject.SetActive(false);

        }
    }
}
