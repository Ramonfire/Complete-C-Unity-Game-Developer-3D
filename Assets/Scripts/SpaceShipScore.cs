using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipScore : MonoBehaviour
{
    private long _score;
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
    }

    public void AddToScore(long inScoreValue) { _score+=inScoreValue; }

    public long getScore() { return _score; }

}
