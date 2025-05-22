using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    int weaponCount;
    int previousWeapon;
    // Update is called once per frame
    void Start()
    {
        weaponCount = ActivateWeapon();
        previousWeapon = currentWeapon;
    }

    private void Update()
    {
         ProcessInput();
        CheckIfValidWeaponSelection();
        ActivateWeapon();

    }

    private void CheckIfValidWeaponSelection()
    {
        if (currentWeapon < 0)
            currentWeapon = weaponCount - 1;
        else if (currentWeapon > weaponCount - 1)
            currentWeapon = 0;
    }

    private void ProcessInput()
    {
        previousWeapon = currentWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            currentWeapon++;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            currentWeapon--;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 3;
        }
    }

    private int ActivateWeapon()
    {
        int weaponIndex = 0;


        foreach (Transform weapon in transform)
        {
            if(previousWeapon != currentWeapon)
            { 
                if(weaponIndex == currentWeapon)
                    weapon.gameObject.SetActive(true);      
                else
                    weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
        return weaponIndex;
    }
}
