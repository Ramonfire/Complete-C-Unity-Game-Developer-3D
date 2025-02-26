using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    private RocketMovement _playerMovement;
    [SerializeField] private AudioClip ExplosionSound;
    [SerializeField] private float _delay = 1;
    bool isAlive;
    private void Start()
    {
        _playerMovement = GetComponent<RocketMovement>();
        isAlive = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Friendly":
                Debug.Log("Hit a friendly");
                break;
            case "Fuel":
                Debug.Log("Refueled");
                break;
            case "Finish":
                LoadNextScene();
                break;
            case "Obstacle":
                StartCrashSequence();
                break;

            default:
                break;
        }


    }

    private void LoadScene(Int32 index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(0);
    }


    private void LoadNextScene()
    {

        IEnumerator coroutine = WaitThenLoadNextScene();
        _playerMovement.enabled = false;
        StartCoroutine(coroutine);
    }

    private void StartCrashSequence() 
    {
          IEnumerator coroutine = ReloadScene();
        _playerMovement.enabled = false;
        GetComponent<AudioSource>().PlayOneShot(ExplosionSound);
        StartCoroutine(coroutine);
        
    }

    private IEnumerator WaitThenLoadNextScene() 
    {
        yield return new WaitForSeconds(_delay);
        LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_delay);
       LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
