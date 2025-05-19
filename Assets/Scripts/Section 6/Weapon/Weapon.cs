using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FirstPersonCamera;
    [SerializeField] float maxDistance= 100f;
    [Tooltip("round per seconds")]
    [SerializeField] float fireRate = 1f;
   [SerializeField] LayerMask layerMask;
    [SerializeField] int Damage = 5;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem enemyHitEffect;
    [SerializeField] ParticleSystem worldHitEffect;
    float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        float ShootDelta = (Time.time - lastShot);
        if (ShootDelta < 1 / fireRate)
            return;
        MuzzleFlash();
        Fire();
    }

    private void MuzzleFlash()
    {
        if (muzzleFlash)
            muzzleFlash.Play();
    }

    private void Fire()
    {
        RaycastHit hit;
        bool didHit = Physics.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.forward, out hit, maxDistance, layerMask);

        if (didHit)
        {
            RayHitSomething(hit);

        }
        lastShot = Time.time;
    }

    private void RayHitSomething(RaycastHit hit)
    {
        EnemyHp target = hit.transform.GetComponent<EnemyHp>();
        if (target != null)
        {
            target.ReceiveDamage(Damage);
            CreateEffect(hit, enemyHitEffect);
        }
        else
            CreateEffect(hit, worldHitEffect);
    }

    private void CreateEffect(RaycastHit hit, ParticleSystem enemyHitEffect)
    {
        ParticleSystem test= Instantiate(enemyHitEffect,hit.point,Quaternion.LookRotation(hit.normal));

    }
}
