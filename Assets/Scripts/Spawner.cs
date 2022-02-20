using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float delayBetweenSpawns = 0f;
    [SerializeField] private int enemySpawnCount=0;
    [SerializeField] private float startDelay = 0;


    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (enemySpawnCount > 0)
        {
            if(GameManager.Instance.IsGameEnded)    break;

            // Generate random number for Instantiate different enemies
            int random = Random.Range(0, enemyPrefabs.Length);

            // spawn enemy from pool manager and activate it
            GameObject enemy =
                PoolManager.Instance.ReuseObject(enemyPrefabs[random], transform.position, Quaternion.identity);
            enemy.SetActive(true);

            // play spawn sound
            AudioManager.Instance.PlaySound(SoundName.Effect_EnemySpawn);

            enemySpawnCount--;

            // Delay between spawns
            yield return new WaitForSeconds(delayBetweenSpawns);

        }
    }
}
