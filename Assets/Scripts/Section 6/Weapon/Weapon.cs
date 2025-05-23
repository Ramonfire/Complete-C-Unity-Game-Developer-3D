using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Ammo))]
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
    [SerializeField] bool isAutomatic=true;
    Ammo ammoSlot;
    public Ammo AmmoSlot //property used to fetch ammoSlot to recharge it when ammo is picked up by the player
    { get { return ammoSlot; }
    }
    float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        ammoSlot = GetComponent<Ammo>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (isAutomatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))//should fire as long as the button is held
            {
                if (ammoSlot.GetAmmoCount() > 0)
                    Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))//should only fire once
                if (ammoSlot.GetAmmoCount() > 0)
                    Shoot();
        }
    }

    private void Shoot()
    {
        float ShootDelta = (Time.time - lastShot);
        if (ShootDelta < 1 / fireRate)// not using courotine here because they cost more than a simple return
            return;
        MuzzleFlash();
        Fire();
    }

    private void MuzzleFlash()
    {
        if (muzzleFlash!=null)
            muzzleFlash.Play();
    }

    private void Fire()
    {
        ammoSlot.ReduceAmmo();
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
