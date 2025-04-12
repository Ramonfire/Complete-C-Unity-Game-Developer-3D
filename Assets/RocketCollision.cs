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
    [SerializeField] private ParticleSystem _CrashEffect;
    [SerializeField] private ParticleSystem _SuccessEffect;
    bool isAlive;
    bool isTransitioning;
    private void Start()
    {
        _playerMovement = GetComponent<RocketMovement>();
        _CrashEffect.Stop();
        _SuccessEffect.Stop();
        isAlive = true;
        isTransitioning = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)// dont proccess collisions if we are transitioning to an other scene
            return;
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
                isAlive = false;
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
        isTransitioning = true;
        GetComponent<AudioSource>().Stop();
        _CrashEffect.Stop();
        _SuccessEffect.Play();
        IEnumerator coroutine = WaitThenLoadNextScene();
        _playerMovement.enabled = false;
        StartCoroutine(coroutine);
        isTransitioning = false;
    }

    private void StartCrashSequence() 
    {
        isTransitioning = true;
        GetComponent<AudioSource>().Stop();
        IEnumerator coroutine = ReloadScene();
        _playerMovement.enabled = false;
        _SuccessEffect.Stop();
        _CrashEffect.Play();
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
        isTransitioning = false;
    }
}
