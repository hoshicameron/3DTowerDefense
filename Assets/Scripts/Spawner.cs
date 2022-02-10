using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float delayBetweenSpawns = 0f;
    [SerializeField] private int enemySpawnCount=0;


    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (enemySpawnCount > 0)
        {
            GameObject enemy=Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.name = "Enemy" + enemySpawnCount;
            enemySpawnCount--;
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }
}
