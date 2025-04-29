using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravity : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rbbody;
    private float creationTime;
    [SerializeField] 
    private float  timeToWait;
    private MeshRenderer rend;
    void Start()
    {
        rbbody = GetComponent<Rigidbody>();
        rbbody.useGravity = false;
        creationTime = Time.time;
        rend=GetComponent<MeshRenderer>();
        rend.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - creationTime >= timeToWait)
        {
            rend.enabled = true;
            rbbody.useGravity = true;
        }
    }
}
