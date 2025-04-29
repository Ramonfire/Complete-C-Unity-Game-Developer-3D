using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipCollisionHandler : MonoBehaviour
{
    PlayerControls movement;
    [SerializeField] ParticleSystem _CrashEffect;
    [SerializeField] AudioClip ExplosionSound;
    [SerializeField] float _delay = 1f;
    private bool isTransitioning;
    private void Start()
    {
        movement = GetComponent<PlayerControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning)
            return;
        switch (other.gameObject.tag)
        {
            case "Enemy":
                StartCrashSequence();
                break;
            default:
                break;
        }
    }

    private void StartCrashSequence()
    {
        PrepareBeforeLoad();
        _CrashEffect.Play();
        IEnumerator coroutine = ReloadScene();
        StartCoroutine(coroutine);
    }

    private void PrepareBeforeLoad()
    {
        isTransitioning = true;
        movement.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private IEnumerator WaitThenLoadNextScene()
    {
        yield return new WaitForSeconds(_delay);
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isTransitioning = false;
    }
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_delay);
        LoadScene(SceneManager.GetActiveScene().buildIndex);
        isTransitioning = false;
    }

    private void LoadScene(Int32 index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(0);
    }
}
