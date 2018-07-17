using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveX", menuName = "Wave", order = 1)]
public class Wave : ScriptableObject
{
    public float SpawnTimer;
    public List<Enemy> Enemies;
}
