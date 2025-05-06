using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    [SerializeField] int cost = 25;
    [SerializeField] int UpgradeCost = 50;
    [SerializeField] float buildDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        //Disbale all gameobjects 
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        //  activate the gameobjects with a delay
        foreach (Transform child in transform) 
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject CreateBallista(Tower ballistaPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>(); ;

        if (WithDrawConstructionCost(bank))
            return Instantiate(ballistaPrefab.gameObject,position, Quaternion.identity);
        else
           return null;
    }
    public GameObject UpgradeBallista()
    {
        return null;
    }


    private bool WithDrawConstructionCost(Bank bank)
    {
        if (CheckBalance(cost,bank)) 
        {
            bank.Withdraw(cost);
            return true;
        }  
        else
            return false;
    }

    private bool WithDrawUpgradeCost(Bank bank)
    {
        if (CheckBalance(UpgradeCost,bank))
        {
            bank.Withdraw(UpgradeCost);
            return true;
        }
        else
            return false;
    }

    private bool CheckBalance(int inCost,Bank bank)
    {
        if (bank == null)
            return false;

        if (bank.CurrentBalance < inCost)
            return false;

        return true;
    }
}
