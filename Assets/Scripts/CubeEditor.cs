using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    [SerializeField] private TextMeshPro positionText;

    private Vector3 gridPosition;
    private Vector3 currentPosition;
    private Waypoint waypoint;
    private int gridSize;

    private void Start()
    {
        currentPosition = transform.position;
        waypoint = GetComponent<Waypoint>();
        gridSize = waypoint.GetGridSize();
    }

    void Update()
    {
        if(transform.position==currentPosition)    return;

        SnapToGrid();

        UpdateLable();
    }

    private void UpdateLable()
    {
        float xPos = gridPosition.x / gridSize;
        float yPos = gridPosition.z / gridSize;

        positionText.SetText($"{xPos},{yPos}");
        gameObject.name = $"Tile{xPos},{yPos}";
    }

    private void SnapToGrid()
    {
        gridPosition = new Vector3(waypoint.GetGridPosition().x,transform.position.y,waypoint.GetGridPosition().y);
        transform.position = gridPosition;
    }
}
