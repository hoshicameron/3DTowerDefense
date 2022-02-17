using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
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
            int random = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy =
                PoolManager.Instance.ReuseObject(enemyPrefabs[random], transform.position, Quaternion.identity);
            enemy.SetActive(true);
            enemySpawnCount--;
            yield return new WaitForSeconds(delayBetweenSpawns);
            if(GameManager.Instance.IsGameEnded)    break;
        }
    }
}
