using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] float _spawnDelay = 3f;
    [SerializeField] GameObject RamPrefab;
    [SerializeField] int poolSize=5;
    private GameObject[] pool;




    private void Awake()
    {
        PopulatePool();
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
    void Start()
    {
        StartCoroutine(InstantiateEnemyRam());
    }

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
