using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]//executes in edit mode and play mode
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabererPathFinding : MonoBehaviour
{
    TextMeshPro label;
    Tile wayPoint;
    Vector2Int Coordinates = new Vector2Int();
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color occupiedColor = Color.blue;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color pathColor = Color.yellow;
    GridManager gridManager;
    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        wayPoint = GetComponentInParent<Tile>();
        gridManager = FindObjectOfType<GridManager>();
        label.enabled = false;
        DisplayCoordinates();
        RenameObject();
        ColorLabel();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Application.isPlaying)
        {
            if (!label.enabled)
                label.enabled = true;
            DisplayCoordinates();
            RenameObject();
        }

        ColorLabel();
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
        if (gridManager == null)
            return;
        Coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        Coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
    }

    private void DisplayCoordinates()
    {

        UpdateCoordinates();
        label.text = Coordinates.x + "," + Coordinates.y;
    }

    private void RenameObject()
    {
        UpdateCoordinates();
        transform.parent.name = Coordinates.ToString();

    }
    private void ColorLabel()
    {
        if (gridManager == null)
            return;

        Node node = gridManager.GetNode(Coordinates);
        if (node!=null)
        {
            if (node.isValidPath)
            {
                SetLabelColor(defaultColor);
                if (node.isPath)
                    SetLabelColor(occupiedColor);
                else if (node.isExplored)
                    SetLabelColor(pathColor);
            }
            else
                SetLabelColor(blockedColor);

        }
    }
    private void SetLabelColor(Color inColor)
    {
        if (label.color != inColor)
            label.color = inColor;
    }
}
