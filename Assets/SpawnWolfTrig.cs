using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWolfTrig : MonoBehaviour
{
    public List<GameObject> spawn;
    public bool spawnMoment = true;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Hero") && spawnMoment)
        {
            spawnMoment = false;
            for (int i = 0; i < spawn.Count; i++)
            {
                StartCoroutine(Spawner(spawn[i].transform));
            }
        }
    }

    [SerializeField] private float spawRate = 1f;

    [SerializeField] private GameObject enemyPrefabs;

    private IEnumerator Spawner(Transform pos)
    {
        WaitForSeconds wait = new WaitForSeconds(spawRate);

        yield return wait;

        Instantiate(enemyPrefabs, new Vector3(pos.position.x, pos.position.y, 0), Quaternion.identity);
    }
}
