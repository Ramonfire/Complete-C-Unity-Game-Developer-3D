using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionEffect;
    bool isTransitioning;
    [SerializeField] float _delay=0.5f;
    [SerializeField] SpaceShipScore scoreBoard;
    [SerializeField] long reward=1;



    private void Start()
    {
        scoreBoard = FindObjectOfType<SpaceShipScore>();// only one exist per scene therefore we can fetch it this way
    }


    private void OnParticleCollision(GameObject other)
    {
        if (isTransitioning)
            return;
        if (other.CompareTag("Laser"))
        {
            isTransitioning = true;
            StartDestructionSequence();
            
        }
    }

    private void StartDestructionSequence()
    {
        IEnumerator destructionSequence = DestoryObjectSequence();
        scoreBoard.AddToScore(reward);//increase the score
        StartCoroutine(destructionSequence);
    
    }

    private IEnumerator DestoryObjectSequence()
    {
        CreateExplosion();
        yield return new WaitForSeconds(_delay);
        KillSelf();
    }

    private void KillSelf()
    {
        isTransitioning = false;
        Destroy(this.gameObject);
    }

    private void CreateExplosion()
    {
        ParticleSystem explosionObject = null;
        if (explosionEffect != null)
            explosionObject = Instantiate(explosionEffect, transform.position, Quaternion.identity);//Create a new instance of the object then play it

    }
}
