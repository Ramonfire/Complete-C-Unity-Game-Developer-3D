using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 5;
    int currentHitPoints=0;
    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = MaxHitPoints;
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
            Destroy(gameObject);
        }
    }
}
