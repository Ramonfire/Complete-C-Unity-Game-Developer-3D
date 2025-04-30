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
    WayPoint wayPoint;
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color occupiedColor = Color.blue;
    [SerializeField] Color blockedColor = Color.red;
    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        wayPoint = GetComponentInParent<WayPoint>();
        label.enabled = false;
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

        ColorCoords();
        ToggleLabels();
    }

    void ToggleLabels() 
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            label.enabled = !label.IsActive();
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
    private void ColorCoords()
    {
        if (!wayPoint.IsPlaceable())
        {
            SetLabelColor(blockedColor);
            return;
        }

        if (wayPoint.IsOccupied())
        {
            SetLabelColor(occupiedColor);
        }
        else
        {
            SetLabelColor(defaultColor);
        }
    }
    private void SetLabelColor(Color inColor)
    {
        if(label.color!=inColor)
         label.color = inColor;
    }

}
