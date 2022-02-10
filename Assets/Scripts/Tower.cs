using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;
    [SerializeField] private float range = 0;
    [SerializeField] private ParticleSystem particleSystem;

    private EnemyMovement[] enemies;
    private EnemyMovement target;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
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
        target = enemies[0];

        for (int i = 1; i < enemies.Length; i++)
        {
            var distanceFromTarget = Vector3.Distance(transform.position, target.transform.position);
            if (Vector3.Distance(transform.position, enemies[i].transform.position) < distanceFromTarget)
            {
                target = enemies[i];
            }
        }
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
        var emission = particleSystem.emission;
        emission.enabled = active;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,range);
    }
}
