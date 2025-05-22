using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    mm9,//9 mm ammo for the pistol
    mm10,//10 mm ammo for the uzi
    R762,//7.62 ammo for the ak
    guage12//12 guage shells for the shoty
}

public class Ammo : MonoBehaviour// i prefer that each weapon manages it s own ammo.
{
    [SerializeField] int ammoCount;
    [SerializeField] AmmoType ammoType;

    public int GetAmmoCount() 
    {
        return ammoCount;
    }
    public AmmoType GetAmmoType() 
    {
        return ammoType;
    }
    public void AddAmmo(int inAmmo) 
    {
        ammoCount += inAmmo;
    }

    public void ReduceAmmo()
    {
        ammoCount --;
    }
}
