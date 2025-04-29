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
    private bool isTransitioning;
    [SerializeField] private bool isCollisionDisabeled;
    private void Start()
    {
        _playerMovement = GetComponent<RocketMovement>();
        _CrashEffect.Stop();
        _SuccessEffect.Stop();
        isTransitioning = false;
        isCollisionDisabeled = false;
    }


    private void Update()
    {
        ProcessDebugKeys();
    }

    private void ProcessDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabeled = !isCollisionDisabeled;//disbale or enable the collision
            Debug.Log("Collision is" + (isCollisionDisabeled ? "Disbaled" : "enabled"));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCrashSequence();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || isCollisionDisabeled)// dont proccess collisions if we are transitioning to an other scene
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

    private void PrepareBeforeLoad()
    {
        isTransitioning = true;
        GetComponent<AudioSource>().Stop();
        _playerMovement.enabled = false;
    }

    private void LoadNextScene()
    {
        PrepareBeforeLoad();
        _CrashEffect.Stop();
        _SuccessEffect.Play();
        IEnumerator coroutine = WaitThenLoadNextScene();
        StartCoroutine(coroutine);
    }

    private void StartCrashSequence() 
    {
        PrepareBeforeLoad();
        IEnumerator coroutine = ReloadScene();
        _SuccessEffect.Stop();
        _CrashEffect.Play();
        GetComponent<AudioSource>().PlayOneShot(ExplosionSound);
        StartCoroutine(coroutine);

    }

    private IEnumerator WaitThenLoadNextScene() 
    {
        yield return new WaitForSeconds(_delay);
        LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        isTransitioning = false;
    }
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_delay);
       LoadScene(SceneManager.GetActiveScene().buildIndex);
        isTransitioning = false;
    }
}
