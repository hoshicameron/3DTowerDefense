using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class TileEditor : MonoBehaviour
{

    private GridEditor gridEditor;

    private Vector3 tilePosition;
    private Waypoint waypoint;
    private int gridSize;
    private Vector3 currentPosition = Vector3.zero;

    private bool up=false;
    private bool right=false;
    private bool down=false;
    private bool left=false;

    private void OnEnable()
    {
        gridEditor = FindObjectOfType<GridEditor>();
        waypoint = GetComponent<Waypoint>();
        gridSize = waypoint.GetGridSize();

        gridEditor.UpdateTileDictionary();
        UpdateTile();
    }



    void Update()
    {

        if (transform.position!=currentPosition)
        {
            currentPosition = transform.position;
            UpdateTile();
        }

    }

    private void UpdateTile()
    {
        SnapToGrid();
        UpdateLable();
        SetTileModel();
    }

    private void UpdateLable()
    {
        float xPos = waypoint.GetGridPosition().x;
        float zPos = waypoint.GetGridPosition().y;

        gameObject.name = $"Tile{xPos},{zPos}";
    }

    private void SnapToGrid()
    {
        tilePosition = new Vector3(
            waypoint.GetGridPosition().x*gridSize
            ,transform.position.y,
            waypoint.GetGridPosition().y*gridSize);
        transform.position = tilePosition;
    }

    public void SetTileModel()
    {
        gridEditor.CheckNeighbors(waypoint.GetGridPosition(),out up,out right,out down,out left);

        GameObject tilePrefab = gridEditor.GetTileModel(up, right, down, left);

        if (tilePrefab != null)
        {
            if (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            Instantiate(tilePrefab, transform.position,tilePrefab.transform.rotation, transform);


            /*if (tileModel != null)
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
            }*/
        }
    }
}
