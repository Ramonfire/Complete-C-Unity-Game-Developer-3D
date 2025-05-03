using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField][Range(0.1f, 20f)] float _spawnDelay = 3f;
    [SerializeField] GameObject RamPrefab;
    [SerializeField][Range(0, 50)] int poolSize = 5;
    private GameObject[] pool;




    private void Awake()
    {
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(InstantiateEnemyRam());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(RamPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
   

    private void Update()
    {
        
    }


    IEnumerator InstantiateEnemyRam()
    {
        while (true)
        {
            ActivateRams();
            yield return new WaitForSeconds(_spawnDelay);
        }

    }

    private void ActivateRams()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }

        }
    }
}
