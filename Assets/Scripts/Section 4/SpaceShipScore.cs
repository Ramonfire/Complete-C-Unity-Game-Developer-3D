using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceShipScore : MonoBehaviour
{
    private long _score;
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        GetComponent<TMP_Text>().SetText("Score: \n " + _score);
    }

    public void AddToScore(long inScoreValue) 
    { 
        _score+=inScoreValue;
        GetComponent<TMP_Text>().SetText("Score: \n "+_score);
    }

    public long getScore() { return _score; }

}
