using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoCount;


    public int GetAmmoCount() 
    {
        return ammoCount;
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
