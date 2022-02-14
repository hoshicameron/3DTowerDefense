using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;

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

    private void OnMouseOver()
    {

    }

    private void OnMouseDown()
    {
        if (IsPlaceable)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            IsPlaceable = false;
        }
    }
}
