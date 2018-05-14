using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    private List<Enemy> _activeEnemies;

    public ReadOnlyCollection<Enemy> ActiveEnemies { get { return _activeEnemies.AsReadOnly(); } }

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _activeEnemies = new List<Enemy>();
        }
        else if (Instance != this)
            GameObject.Destroy(this);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }

    public void AddEnemy(Enemy enemy)
    {
        if(enemy != null)
            _activeEnemies.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
