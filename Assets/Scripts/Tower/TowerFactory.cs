using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : SingletonMonoBehaviour<TowerFactory>
{
    private Tower towerPrefab;

    public Tower TowerPrefab
    {
        get { return towerPrefab; }
        set { towerPrefab = value; }
    }

    private Queue<Tower>towerQueue=new Queue<Tower>();
    public void AddTower(Waypoint baseWaypoint, Vector3 position)
    {
        int towerCount = towerQueue.Count;


        if (towerCount < GameManager.Instance.MaxAllowedTower)
        {
            InstantiateNewTower(baseWaypoint,position);
        } else
        {
            MoveExistingTower(baseWaypoint,position);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint, Vector3 position)
    {
        if(towerPrefab==null) return;

        Tower tower=Instantiate(towerPrefab,position , Quaternion.identity,transform);
        baseWaypoint.IsPlaceable = false;

        tower.BaseWaypoint = baseWaypoint;

        towerQueue.Enqueue(tower);
    }

    private void MoveExistingTower(Waypoint baseWaypoint,Vector3 position)
    {
        Tower tower = towerQueue.Dequeue();

        tower.transform.position = position;
        tower.transform.rotation=Quaternion.identity;

        // free up the tile
        tower.BaseWaypoint.IsPlaceable = true;

        // change tower base and set it to not placeable
        baseWaypoint.IsPlaceable = false;
        tower.BaseWaypoint = baseWaypoint;

        towerQueue.Enqueue(tower);
    }
}
