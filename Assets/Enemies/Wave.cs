using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveX", menuName = "Wave", order = 1)]
public class Wave : ScriptableObject
{
    public int NumberOfSpawns;
    public int SpawnDelay;
    public int SpawnTimer;
    public Enemy EnemyPrefab;

    private int _tempNumOfSpawns;

    public void Initialize()
    {
        _tempNumOfSpawns = NumberOfSpawns;
    }

    public Enemy NextEnemy()
    {
        if (_tempNumOfSpawns > 0)
        {
            _tempNumOfSpawns--;
            return EnemyPrefab;
        }
        return null;
    }
}
