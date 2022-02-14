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



    private void Awake()
    {
        gridDictionary=new Dictionary<Vector2Int, Waypoint>();
        waypointQueue = new Queue<Waypoint>();
        path = new List<Waypoint>();
    }


    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            return CalculatePath();
        }

        return path;
    }

    private List<Waypoint> CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }

    private void BreadthFirstSearch()
    {
        waypointQueue.Enqueue(startWaypoin);
        while (waypointQueue.Count>0 && path.Count==0)
        {
            Waypoint waypoint = waypointQueue.Dequeue();
            waypoint.IsExplored = true;

            if (CheckForGoal(waypoint))
            {
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
        SetPath(endWaypoin);

        Waypoint previousWaypoint = endWaypoin.ExploredFrom;
        while (previousWaypoint!=null)
        {
            SetPath(previousWaypoint);
            previousWaypoint = previousWaypoint.ExploredFrom;

        }

        path.Reverse();

    }

    private void SetPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.IsPlaceable = false;
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
