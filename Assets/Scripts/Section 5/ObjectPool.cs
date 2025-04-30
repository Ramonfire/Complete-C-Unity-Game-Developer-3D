using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] float _spawnDelay = 3f;
    [SerializeField] GameObject RamPrefab;
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
            //we can instantiate as the ObjectPool child 
            Instantiate(RamPrefab, transform);
            yield return new WaitForSeconds(_spawnDelay);
           
        }
       
    }

}
