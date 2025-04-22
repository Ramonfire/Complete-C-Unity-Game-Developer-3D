using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] ParticleSystem HitEffect;
    bool isTransitioning;
    [SerializeField] float _delay=0.5f;
    [SerializeField] SpaceShipScore scoreBoard;
    [SerializeField] long reward=1;
    [SerializeField] float Hp=1;



    private void Start()
    {
        scoreBoard = FindObjectOfType<SpaceShipScore>();// only one exist per scene therefore we can fetch it this way
        if (!GetComponent<Rigidbody>()) //if there is no rigid body make one
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        if (isTransitioning)
            return;
        if (other.CompareTag("Laser"))
        {
                ProcessHit();
            
        }
    }

    private void ProcessHit()
    {
        Hp--;
        if (HitEffect != null)
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);
        }
        if (Hp == 0)
        { 
            isTransitioning = true;
            scoreBoard.AddToScore(reward);//increase the score if the enemy is killed
            IEnumerator destructionSequence = DestoryObjectSequence();
            StartCoroutine(destructionSequence);
        }

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
            explosionObject = Instantiate(explosionEffect, transform.position, Quaternion.identity);

    }
}
