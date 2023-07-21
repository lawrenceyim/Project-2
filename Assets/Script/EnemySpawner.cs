using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    float nextSpawnIn;
    [SerializeField] float spawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnIn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnIn) {
            nextSpawnIn = Time.time + spawnCooldown;
            SpawnEnemy(0);
        }
    }

    void SpawnEnemy(int prefabIndex) {
        Instantiate(enemyPrefabs[prefabIndex % enemyPrefabs.Length], transform.position, Quaternion.identity);
    }
}
