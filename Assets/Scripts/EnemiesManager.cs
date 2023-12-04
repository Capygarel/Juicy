using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;

    public Transform spawnPoint;

    public static EnemiesManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void Die(GameObject enemy)
    {
        SpawnNewEnemy();

    }

    private void SpawnNewEnemy()
    {
        StartCoroutine(SpawnNewEnemyInSeconds(1f));
    }

    private IEnumerator SpawnNewEnemyInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject enemy = Instantiate(enemiesPrefabs[0]);
        enemy.transform.position = spawnPoint.position;
    }
}
