using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField]AmmoType type;
    [SerializeField] int ammoAmount;
    Transform player;
    [SerializeField] float pickUpRange=3f;
    // Start is called before the first frame update

    private void Awake()
    {
        ammoAmount = UnityEngine.Random.Range(2, 5);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= pickUpRange)
            PickUp();
    }
    private void PickUp()
    {
       Transform weapons =  FindObjectOfType<WeaponSelector>().transform;

        foreach (Transform weapon in weapons)
        {
            Weapon wp = weapon.GetComponent<Weapon>();
            if (wp.AmmoSlot.GetAmmoType() == type) 
            {
                wp.AmmoSlot.AddAmmo(ammoAmount);
                break;
            }     
           
        }

        Destroy(gameObject);
    }
}
