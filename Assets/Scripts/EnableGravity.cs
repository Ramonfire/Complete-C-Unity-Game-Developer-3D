using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravity : MonoBehaviour
{
    // Start is called before the first frame update
    private  Rigidbody rigidbody;
    private float creationTime;
    [SerializeField] 
    private float  timeToWait;
    private MeshRenderer renderer;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        creationTime = Time.time;
        renderer=GetComponent<MeshRenderer>();
        renderer.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - creationTime >= timeToWait)
        {
            renderer.enabled = true;
            rigidbody.useGravity = true;
        }
    }
}
