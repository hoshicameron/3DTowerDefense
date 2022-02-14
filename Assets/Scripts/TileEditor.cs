using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class TileEditor : MonoBehaviour
{

    [SerializeField] private GridEditor gridEditor;

    private Vector3 gridPosition;
    private Vector3 currentPosition;
    private Waypoint waypoint;
    private int gridSize;
    private int currentPrefabID;

    private GameObject tileModel=null;


    private void Start()
    {
        currentPosition = transform.position;
        waypoint = GetComponent<Waypoint>();
        gridSize = waypoint.GetGridSize();

        CleanGameObject();
        UpdateLable();
    }

    private void CleanGameObject()
    {
        // Create Fresh clone
        for (int i = 1; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentPosition) > Mathf.Epsilon)
        {
            SnapToGrid();
            UpdateLable();
            UpdateTile();
        }


    }

    private void UpdateLable()
    {
        float xPos = waypoint.GetGridPosition().x;
        float zPos = waypoint.GetGridPosition().y;

        gameObject.name = $"Tile{xPos},{zPos}";
    }

    private void SnapToGrid()
    {
        gridPosition = new Vector3(
            waypoint.GetGridPosition().x*gridSize
            ,transform.position.y,
            waypoint.GetGridPosition().y*gridSize);
        transform.position = gridPosition;
    }

    public void UpdateTile()
    {
        GameObject tilePrefab = gridEditor.GetTileModel(waypoint);
        if (tilePrefab != null)
        {
            if (tileModel != null)
            {
                if (currentPrefabID != tilePrefab.GetInstanceID())
                {
                    DestroyImmediate(tileModel);
                    tileModel = Instantiate(tilePrefab, transform.position,Quaternion.identity, transform);
                    currentPrefabID = tilePrefab.GetInstanceID();
                }
            } else
            {
                tileModel = Instantiate(tilePrefab, transform.position,tilePrefab.transform.rotation, transform);
                currentPrefabID = tilePrefab.GetInstanceID();
            }
        }
    }
}
