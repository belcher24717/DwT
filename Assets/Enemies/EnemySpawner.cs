using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public const float spawnDelay = 1f;     // Delay before spawn starts (may not be necessary long term as there will probably be outside delay before activating spawner)

    public int enemyCount;                  // number of enemies to spawn
    public float spawnTimer;                // Delay between each enemy spawn (might be passed in?)
    public GameObject enemyPrefab;          // The enemy prefab (should be passed in)
    public Vector3 spawnPoint;              // The enemy spawn point (might be passed in?)

    private GameObject destination;

	// Use this for initialization
	void Start ()
    {
        spawnPoint = new Vector3(1f, 1f, 50f);

        // find the destination GameObject
        destination = GameObject.Find("Destination");

        // repeatedly spawn enemies after 'spawnDelay' time every 'spawnTimer'.
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnTimer);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Spawn enemy (prefab 'test')
    void SpawnEnemy()
    {
        // stop when all enemies have been spawned
        if (enemyCount-- <= 0)
            return;

        // instantiate the enemy prefab
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        // set enemy destination
        NavMesh navScript = newEnemy.GetComponent<NavMesh>();
        if (destination != null)
            navScript.Destination = destination;
    }

}
