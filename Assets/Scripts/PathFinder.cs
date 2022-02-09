using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Waypoint startWaypoin;
    [SerializeField] private Waypoint endWaypoin;


    private Dictionary<Vector2Int, Waypoint> gridDictionary;
    private Queue<Waypoint> waypointQueue;
    private Vector2Int[] directions ={Vector2Int.up,Vector2Int.right, Vector2Int.down, Vector2Int.left};
    private List<Waypoint> path;

    public List<Waypoint> Path => path;

    private bool isPathFinded = false;
    private void Awake()
    {
        gridDictionary=new Dictionary<Vector2Int, Waypoint>();
        waypointQueue = new Queue<Waypoint>();
        path = new List<Waypoint>();

        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    private void BreadthFirstSearch()
    {
        waypointQueue.Enqueue(startWaypoin);
        while (waypointQueue.Count>0 &&!isPathFinded)
        {
            Waypoint waypoint = waypointQueue.Dequeue();
            waypoint.IsExplored = true;

            if (CheckForGoal(waypoint))
            {
                isPathFinded = true;
                break;
            }

            ExploreNeighbours(waypoint);
        }
    }

    private bool CheckForGoal(Waypoint waypoint)
    {
        return waypoint == endWaypoin;
    }

    private void ExploreNeighbours(Waypoint from)
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinate = direction + from.GetGridPosition();
            if (gridDictionary.ContainsKey(neighbourCoordinate))
            {
                Waypoint neighbour = gridDictionary[neighbourCoordinate];
                if (neighbour.IsExplored == false && !waypointQueue.Contains(neighbour))
                {
                    waypointQueue.Enqueue(neighbour);
                    neighbour.ExploredFrom = from;
                }
            }
        }
    }


    private void CreatePath()
    {
        path.Add(endWaypoin);
        Waypoint previousWaypoint = endWaypoin.ExploredFrom;
        if (previousWaypoint != null)
        {
            while (previousWaypoint!=null)
            {
                path.Add(previousWaypoint);
                previousWaypoint = previousWaypoint.ExploredFrom;
            }
        }

        PrintPath();
    }

    private void PrintPath()
    {
        path.Reverse();
        foreach (Waypoint waypoint in path)
        {
            print(waypoint);
        }
    }

    private void LoadBlocks()
    {

        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            if(gridDictionary.ContainsKey(waypoint.GetGridPosition()))    continue;

            gridDictionary.Add(waypoint.GetGridPosition(),waypoint);
        }
    }
}
