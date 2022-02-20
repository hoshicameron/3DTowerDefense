using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerFactory : SingletonMonoBehaviour<TowerFactory>
{
    private GameObject towerPrefab;

    public GameObject TowerPrefab
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
        // No tower selected the return ...
        if(towerPrefab==null) return;

        // Get tower from pool and activate it
        Tower tower = PoolManager.Instance.
            ReuseObject(towerPrefab.gameObject, position, Quaternion.identity).GetComponent<Tower>();
            tower.gameObject.SetActive(true);

        // Prevent spawn tower in current waypoint
        baseWaypoint.IsPlaceable = false;

        // Set tower base waypoint to current waypoint
        tower.BaseWaypoint = baseWaypoint;

        // Add tower to queue
        towerQueue.Enqueue(tower);

        // Play deploy sound
        AudioManager.Instance.PlaySound(SoundName.Effect_TowerDeploy);
    }

    private void MoveExistingTower(Waypoint baseWaypoint,Vector3 position)
    {
        Tower tower = towerQueue.Dequeue();

        // free up the tile
        tower.BaseWaypoint.IsPlaceable = true;

        // If dequeued tower is same with selected tower then move it to new position
        // otherwise deactivate it and spawn correct tower type

        if (tower.gameObject.CompareTag(towerPrefab.tag))
        {
            tower.transform.position = position;
            tower.transform.rotation=Quaternion.identity;
        } else
        {
            // deactivate dequeued tower
            tower.gameObject.SetActive(false);

            // Get the right tower from pool and activate it
            tower = PoolManager.Instance.
                ReuseObject(towerPrefab, position, Quaternion.identity).GetComponent<Tower>();
            tower.gameObject.SetActive(true);
        }

        // change tower base and set it to not placeable
        baseWaypoint.IsPlaceable = false;
        tower.BaseWaypoint = baseWaypoint;

        towerQueue.Enqueue(tower);

        // Play deploy sound
        AudioManager.Instance.PlaySound(SoundName.Effect_TowerDeploy);
    }
}
