using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int gridSize = 10;
    private Vector2Int gridPosition;

    public bool IsExplored { get; set; } = false;

    public Waypoint ExploredFrom { get; set; } = null;
    public bool IsPlaceable { get; set; } = true;


    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x /gridSize),
            Mathf.RoundToInt(transform.position.z /gridSize));
    }

    private void OnMouseDown()
    {
        if (IsPlaceable)
        {
            TowerFactory.Instance.AddTower(this);
        }
    }
}
