using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    private Vector3 _startingPosition;
    [SerializeField] private Vector3 _mouvementVector;
    private float _mouvementFactor;//Range define the limit of the variable 0<_mouvementFactor<1
    [SerializeField] float period = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        if (period <=Mathf.Epsilon)//for float the chance of  getting an absolute 0 is small , so we use the tiniest defined floating point value which is mathf.Epsilon 
            return;

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;//the area of a circle is 2PI*Radius if the radius is 1 then it is 2PI= Tau = 360 degrees
        float rawSinWave = Mathf.Sin(cycles * tau);

        _mouvementFactor = Mathf.Abs(rawSinWave);//absolute value of the wave to make it oscillate in the correct area
        Vector3 offset = _mouvementVector * _mouvementFactor;
        transform.position = _startingPosition + offset;
    }
}
