using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] Transform turret;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;//find the enemy
    }

    // Update is called once per frame
    void Update()
    {
        Tracktarget();
    }

    private void Tracktarget()
    {
        if(target!=null)
         turret.LookAt(target);
    }
}
