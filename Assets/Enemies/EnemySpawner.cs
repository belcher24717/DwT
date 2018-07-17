using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public float spawnDelay = 1f;     // Delay before spawn starts (may not be necessary long term as there will probably be outside delay before activating spawner)

    public int currentWave;            // number of enemies to spawn
    public Vector3 spawnPoint;        // The enemy spawn point (might be passed in?)
    public Transform SpawnParent;
    public GameObject Destination;
    public Wave[] Waves;
    private int _spawnCount;
    // Use this for initialization
    void Start()
    {
        currentWave = 0;
        spawnPoint = this.gameObject.transform.position;
        // repeatedly spawn enemies after 'spawnDelay' time every 'spawnTimer'.
        //Waves[currentWave].Initialize();
        //InvokeRepeating("SpawnEnemy", Waves[currentWave].SpawnDelay, Waves[currentWave].SpawnTimer);
    }

    public void StartWave()
    {
        _spawnCount = 0;
        InvokeRepeating("SpawnEnemy", 0, Waves[currentWave].SpawnTimer);
        currentWave = currentWave + 1 % Waves.Length;
    }

	// Update is called once per frame
	void Update ()
    {

	}

    // Spawn enemy (prefab 'test')
    void SpawnEnemy()
    {
        Enemy nextEnemy = _spawnCount < Waves[currentWave].Enemies.Count ? Waves[currentWave].Enemies[_spawnCount] : null;
        // stop when all enemies have been spawned
        if (nextEnemy == null)
            return;

        // instantiate the enemy prefab
        GameObject newEnemy = Instantiate(nextEnemy.gameObject, spawnPoint, Quaternion.LookRotation(Destination.transform.position - spawnPoint), SpawnParent);

        // set enemy destination
        CorrectNavMeshAgentScript navScript = newEnemy.GetComponent<CorrectNavMeshAgentScript>();
        if (Destination != null)
            navScript.Destination = Destination;

        EnemyController.Instance.AddEnemy(newEnemy.GetComponent<Enemy>());
        _spawnCount++;
    }

}
