using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.PlayerLoop;

[ExecuteAlways]//executes in edit mode and play mode
public class CoordinateLaberer : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int Coordinates = new Vector2Int();
    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
        RenameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Application.isPlaying) 
        {
            DisplayCoordinates();
            RenameObject();
        }
    }

    private void UpdateCoordinates() 
    {
        Coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        Coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
    }

    private void DisplayCoordinates()
    {

        UpdateCoordinates();
        label.text = Coordinates.x+","+Coordinates.y;
    }

    private void RenameObject() 
    {
        UpdateCoordinates();
        transform.parent.name = Coordinates.ToString();
    
    }
}
