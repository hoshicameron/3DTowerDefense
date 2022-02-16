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


    private Dictionary<Vector2Int,Waypoint> tileDictionary=new Dictionary<Vector2Int, Waypoint>();
    private TileEditor[] tiles;
    private Waypoint[] waypoints;

    private readonly Vector2Int directionUp=Vector2Int.up;
    private readonly Vector2Int directionRight=Vector2Int.right;
    private readonly Vector2Int directionDown=Vector2Int.down;
    private readonly Vector2Int directionLeft=Vector2Int.left;

    private void OnEnable()
    {
        UpdateTileDictionary();
    }


    public void UpdateTileDictionary()
    {
        tileDictionary.Clear();
        waypoints = GetComponentsInChildren<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            if (tileDictionary.ContainsKey(waypoint.GetGridPosition())) continue;

            tileDictionary.Add(waypoint.GetGridPosition(),waypoint);

        }
    }

    public void CheckNeighbors(Vector2Int from,out bool up,out bool right,out bool down,out bool left)
    {
        print(from);
        var upNeighbor = from + directionUp;
        var rightNeighbor = from + directionRight;
        var downNeighbor = from + directionDown;
        var leftNeighbor = from + directionLeft;

        up    = tileDictionary.ContainsKey(upNeighbor);
        right = tileDictionary.ContainsKey(rightNeighbor);
        down  = tileDictionary.ContainsKey(downNeighbor);
        left  = tileDictionary.ContainsKey(leftNeighbor);
    }

    public GameObject GetTileModel(bool up, bool right, bool down, bool left)
    {
        print($"{up},{right},{down},{left}");

        if (up && down && right && left)           return middleModel;
        else if (up && down && !left && !right)    return upDownModel;
        else if (!up && !down && left && right)    return leftRightModel;
        else if (!up && down && !left && right)    return upLeftCornerModel;
        else if (!up && down && left && !right)    return upRightCornerModel;
        else if (up && !down && !left && right)    return downLeftCornerModel;
        else if (up && !down && left && !right)    return downRightCornerModel;
        else if (up && down && !left && right)     return branchUpDownRightModel;
        else if (up && down && left && !right)     return branchUpDownLeftModel;
        else if (up && !down && left && right)     return branchLeftRightUpModel;
        else if (!up && down && left && right)     return branchLeftRightDownModel;
        else if (!up && down && !left && !right)   return upStartModel;
        else if (up && !down && !left && !right)   return downStartModel;
        else if (!up && !down && !left && right)   return leftStartModel;
        else if (!up && !down && left && !right)   return rightStartModel;

        print("No Suitable Model Exists");
        return null;

    }
}
