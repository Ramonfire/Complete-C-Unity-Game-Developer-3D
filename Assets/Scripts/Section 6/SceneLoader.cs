using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  PlayAgain() 
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void QuitApplication() 
    {
        Application.Quit();    
    }
    private void LoadScene(int SceneIndex)
    {
        if (SceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneIndex);
    }

}
