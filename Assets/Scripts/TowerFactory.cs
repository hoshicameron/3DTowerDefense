using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : SingletonMonoBehaviour<TowerFactory>
{
    [SerializeField] private int towerLimit = 5;
    [SerializeField] private Tower towerPrefab;

    private Queue<Tower>towerQueue=new Queue<Tower>();
    public void AddTower(Waypoint baseWaypoint)
    {
        int towerCount = towerQueue.Count;

        if (towerCount < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        } else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        Tower tower=Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity,transform);
        baseWaypoint.IsPlaceable = false;

        tower.BaseWaypoint = baseWaypoint;

        towerQueue.Enqueue(tower);
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        Tower tower = towerQueue.Dequeue();

        tower.transform.position = baseWaypoint.transform.position;
        tower.transform.rotation=Quaternion.identity;

        // free up the tile
        tower.BaseWaypoint.IsPlaceable = true;

        // change tower base and set it to not placeable
        baseWaypoint.IsPlaceable = false;
        tower.BaseWaypoint = baseWaypoint;

        towerQueue.Enqueue(tower);
    }
}
