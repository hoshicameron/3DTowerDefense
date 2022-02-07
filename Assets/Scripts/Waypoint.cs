using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int gridSize = 10;
    private Vector2Int gridPosition;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x * 0.1f) * gridSize,
            Mathf.RoundToInt(transform.position.z * 0.1f) * gridSize);
    }
}
