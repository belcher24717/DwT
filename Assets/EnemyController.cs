using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    private List<Enemy> _enemiesActive;

    public ReadOnlyCollection<Enemy> EnemiesActive { get { return _enemiesActive.AsReadOnly(); } }

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _enemiesActive = new List<Enemy>();
        }
        else if (Instance != this)
            GameObject.Destroy(this);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemiesActive.Add(enemy);
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemiesActive.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
