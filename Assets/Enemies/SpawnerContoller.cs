using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnerContoller : MonoBehaviour
{
    private static List<EnemySpawner> _spawners;

    void Awake()
    {
        _spawners = new List<EnemySpawner>(transform.GetComponentsInChildren<EnemySpawner>());
    }

    public void NextWave()
    {
        if (_spawners != null)
            foreach (EnemySpawner spawner in _spawners)
                spawner.StartWave();

        EventSystem.current.SetSelectedGameObject(null);
    }

}
