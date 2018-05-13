using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CannonScript : Tower {



	// Use this for initialization
	void Start () {
        _range = 30;
        _damage = 10;
        _fireRate = .5; // attacks per second
        //_attackType = Enums.AttackType.
	}
	
	// Update is called once per frame
	void Update () {
        _targetedEnemy = PickEnemy();
        if (_targetedEnemy != null)
            Attack(_targetedEnemy.GetComponent<Enemy>());
	}

    public override void Attack(Enemy enemy)
    {
        // gotta attack dat enemy!
    }

    public override GameObject PickEnemy()
    {
        float distanceToEnemy = _range + 1;
        GameObject enemyToFace = null;

        ReadOnlyCollection<Enemy> activeEnemies = EnemyController.Instance.ActiveEnemies;

        if (activeEnemies.Count != 0)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                float distance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
                if (distance <= _range)
                {
                    distanceToEnemy = (distance < distanceToEnemy) ? distance : distanceToEnemy;
                    enemyToFace = enemy.gameObject;
                }
            }
        }

        return enemyToFace;
    }

}
