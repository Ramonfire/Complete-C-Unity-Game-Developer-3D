using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bank!=null) 
        {
            GetComponent<TMP_Text>().SetText("Balance: " + bank.CurrentBalance);   
        }
        
    }
}
