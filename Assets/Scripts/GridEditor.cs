using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridEditor : MonoBehaviour
{

    [SerializeField] private GameObject upLeftCornerModel;
    [SerializeField] private GameObject upRightCornerModel;
    [SerializeField] private GameObject downLeftCornerModel;
    [SerializeField] private GameObject downRightCornerModel;
    [SerializeField] private GameObject leftRightModel;
    [SerializeField] private GameObject upDownModel;
    [SerializeField] private GameObject middleModel;
    [SerializeField] private GameObject branchUpDownRightModel;
    [SerializeField] private GameObject branchUpDownLeftModel;
    [SerializeField] private GameObject branchLeftRightDownModel;
    [SerializeField] private GameObject branchLeftRightUpModel;
    [SerializeField] private GameObject leftStartModel;
    [SerializeField] private GameObject rightStartModel;
    [SerializeField] private GameObject upStartModel;
    [SerializeField] private GameObject downStartModel;


    private Dictionary<Vector2Int,Waypoint> tileDictionary;

    private Vector2Int directionUp=Vector2Int.up;
    private Vector2Int directionRight=Vector2Int.right;
    private Vector2Int directionDown=Vector2Int.down;
    private Vector2Int directionLeft=Vector2Int.left;

    private bool up, right, down, left;

    private GameObject tileModel=null;

    private void Start()
    {
        UpdateAllTiles();
    }

    public void UpdateAllTiles()
    {
        TileEditor[] tiles = FindObjectsOfType<TileEditor>();

        foreach (TileEditor cube in tiles)
        {
            cube.UpdateTile();
        }
    }


    public GameObject GetTileModel(Waypoint tile)
    {
        LoadTiles();
        CheckNeighbours(tile);
        SetTileModel();

        return tileModel;
    }

    public void LoadTiles()
    {
        tileDictionary=new Dictionary<Vector2Int, Waypoint>();
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            if (tileDictionary.ContainsKey(waypoint.GetGridPosition())) continue;

            tileDictionary.Add(waypoint.GetGridPosition(),waypoint);

        }
    }

    public void CheckNeighbours(Waypoint tile)
    {
        up = right = down = left = false;

        print(tileDictionary.Count);
        Vector2Int upNeighbourCoordinate = directionUp + tile.GetGridPosition();
        Vector2Int rightNeighbourCoordinate = directionRight + tile.GetGridPosition();
        Vector2Int downNeighbourCoordinate = directionDown + tile.GetGridPosition();
        Vector2Int leftNeighbourCoordinate = directionLeft + tile.GetGridPosition();

        if (tileDictionary.ContainsKey(upNeighbourCoordinate))
        {
            up = true;
        }

        if (tileDictionary.ContainsKey(rightNeighbourCoordinate))
        {
            right = true;
        }

        if (tileDictionary.ContainsKey(downNeighbourCoordinate))
        {
            down = true;
        }

        if (tileDictionary.ContainsKey(leftNeighbourCoordinate))
        {
            left = true;
        }

    }

    public void SetTileModel()
    {
        if (up && down && right && left)
        {
            tileModel= middleModel;
        }
        else if (up && down && !left && !right)
        {
            tileModel= upDownModel;
        }
        else if (!up && !down && left && right)
        {
            tileModel= leftRightModel;
        }
        else if (!up && down && !left && right)
        {
            tileModel= upLeftCornerModel;
        }
        else if (!up && down && left && !right)
        {
            tileModel= upRightCornerModel;
        }
        else if (up && !down && !left && right)
        {
            tileModel= downLeftCornerModel;
        }
        else if (up && !down && left && !right)
        {
            tileModel= downRightCornerModel;
        }
        else if (up && down && !left && right)
        {
            tileModel= branchUpDownRightModel;
        }
        else if (up && down && left && !right)
        {
            tileModel= branchUpDownLeftModel;
        }
        else if (up && !down && left && right)
        {
            tileModel= branchLeftRightUpModel;
        }
        else if (!up && down && left && right)
        {
            tileModel= branchLeftRightDownModel;
        }
        else if (!up && down && !left && !right)
        {
            tileModel= upStartModel;
        }
        else if (up && !down && !left && !right)
        {
            tileModel= downStartModel;
        }
        else if (!up && !down && !left && right)
        {
            tileModel= leftStartModel;
        }
        else if (!up && !down && left && !right)
        {
            tileModel= rightStartModel;
        }
        else
        {
            tileModel = middleModel;
            print("No Suitable Model Exists");
        }
    }
}