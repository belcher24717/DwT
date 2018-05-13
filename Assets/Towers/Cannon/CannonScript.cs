using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CannonScript : Tower
{
    // Use this for initialization
    void Start()
    {
        _range = 30;
        _damage = 10;
                        //_attackType = Enums.AttackType.
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        gameObject.GetComponent<Animator>().Play("Shoot");
    }

    public override Enemy PickEnemy()
    {
        float distanceToEnemy = _range + 1;
        Enemy enemyToFace = null;

        ReadOnlyCollection<Enemy> activeEnemies = EnemyController.Instance.ActiveEnemies;

        if (activeEnemies.Count != 0)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                float distance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
                if (distance <= _range)
                {
                    distanceToEnemy = (distance < distanceToEnemy) ? distance : distanceToEnemy;
                    enemyToFace = enemy;
                }
            }
        }

        return enemyToFace;
    }
}
