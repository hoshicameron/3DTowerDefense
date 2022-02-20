using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;
    [SerializeField] private float range = 0;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private GameObject towerLight;
    [SerializeField] private AudioSource audioSource;


    private EnemyMovement[] enemies;
    private EnemyMovement target;
    public Waypoint BaseWaypoint { get; set; } = null;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameEnded)    return;

        FindAndDestroy();
    }

    private void FindAndDestroy()
    {
        if (target == null || !target.gameObject.activeSelf ||
            Vector3.Distance(transform.position, target.transform.position) > range)
        {
            Shoot(false);

            FindNewTarget();
        } else
        {
            FireAtEnemy();
        }
    }

    private void FindNewTarget()
    {
        enemies = FindObjectsOfType<EnemyMovement>();

        if (enemies.Length==0) return;
        EnemyMovement closestEnemy = enemies[0];

        for (int i = 1; i < enemies.Length; i++)
        {
            closestEnemy=GetClosestEnemy(closestEnemy,enemies[i]);
        }

        target = closestEnemy;
    }

    private EnemyMovement GetClosestEnemy(EnemyMovement targetA,EnemyMovement targetB)
    {
        var distanceA = Vector3.Distance(transform.position, targetA.transform.position);
        var distanceB = Vector3.Distance(transform.position, targetB.transform.position);

        if (distanceA < distanceB)
        {
            return targetA;
        }

        return targetB;
    }

    public void FireAtEnemy()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < range)
        {
            objectToPan.LookAt(target.transform);
            Shoot(true);
        } else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool active)
    {
        if (towerLight)    towerLight.SetActive(active);

        if (audioSource != null)
        {
            if(active && !audioSource.isPlaying)    audioSource.Play();
            else if(!active && audioSource.isPlaying)          audioSource.Stop();
        }
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var emission = particleSystem.emission;
            emission.enabled = active;
        }

    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,range);
    }*/
}
