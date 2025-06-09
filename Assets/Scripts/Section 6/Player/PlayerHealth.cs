
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    bool levelCompleted = false;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas DamageReceivedCanvas;
    public bool LevelCompleted
    {
        get { return levelCompleted; }
        set { levelCompleted = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        gameOverCanvas.enabled = false;
        DamageReceivedCanvas.enabled = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
           
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerStatus();
        
    }

    private void CheckPlayerStatus()
    {
        if (currentHealth <= 0) 
        {
            ShowUI();
            return;
        }

    }

    private void ShowUI()
    {
        gameOverCanvas.enabled = true;
        TurnOnCursor();
    }

    private  void TurnOnCursor()
    {
        GetComponent<PlayerInput>().enabled = false;//disable player input
        Time.timeScale = 0;//freeze time
        FindObjectOfType<WeaponSelector>().enabled = false;
        Cursor.lockState = CursorLockMode.None;// unlock the cursor
        Cursor.visible = true;//show the cursor
    }


    public void DamagePlayer(int inDamage)
    {
        currentHealth -= inDamage;

        if (DamageReceivedCanvas == null)
            return;

        StartCoroutine(DisplayDamage());
    }
    IEnumerator DisplayDamage() 
    {
        DamageReceivedCanvas.enabled = true;
        yield return new WaitForSeconds(0.3f);
        DamageReceivedCanvas.enabled = false;
    }
}
