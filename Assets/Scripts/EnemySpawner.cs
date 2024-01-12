using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawRate = 4f;

    private float spawRateBegin = 1f;

    [SerializeField] private GameObject[] enemyPrefabs;

    public bool canSpawn = true;

    private void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawRateBegin);

        spawRateBegin = spawRate;

        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemytoSpawn = enemyPrefabs[rand];

        Instantiate(enemytoSpawn, transform.position, Quaternion.identity);
        canSpawn = true;
    }
}
