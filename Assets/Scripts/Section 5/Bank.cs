using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int StartingBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }
    // Start is called before the first frame update
    void Awake()
    {
        currentBalance = StartingBalance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deposit(int inBalance)
    {
         currentBalance += Mathf.Abs(inBalance);
    }

    public void Withdraw(int inBalance)
    {
        currentBalance -= Mathf.Abs(inBalance);

        if (currentBalance < 0) 
        {
            ResetLevel();
        }
        if (currentBalance > 1000) 
        {
            LoadaNextLevel();
        }
    }

    private void LoadaNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);// should load next level
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
