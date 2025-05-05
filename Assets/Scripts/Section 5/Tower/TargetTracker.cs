using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] Transform turret;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;
    [SerializeField]float turretRange = 20f;
    [SerializeField] float TrackingRange = 30f;




    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        TrackTarget();
    }

    private void FindClosestTarget()
    {
        if (target != null)// if we alreaday have a traget withing the range then dont look for a new target
        {
            float targetDitance = Vector3.Distance(transform.position, target.position);
            if (targetDitance < TrackingRange)
                return;
            else
                target = null;
        }

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTaraget = null;
        float maxDistance = TrackingRange;

        foreach (Enemy enemy in enemies)
        {
            float targetDitance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDitance < maxDistance) 
            {
                closestTaraget = enemy.transform;
                maxDistance = targetDitance;
            }
        }

        target = closestTaraget;

    }

    private void TrackTarget()
    {
        if (target != null) 
        {
            turret.LookAt(target);
            float targetDitance = Vector3.Distance(transform.position, target.position);
            Attack(targetDitance < turretRange);
        }
        else { Attack(false); }
        
    }

    private void Attack(bool inRange)
    {
        var emission = projectileParticles.emission;
        emission.enabled = inRange;
    }
}
