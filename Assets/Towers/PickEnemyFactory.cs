using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class PickEnemyFactory
{

    public static Enemy PickClosestEnemy(double range, Vector3 position)
    {
        double distanceToEnemy = range + 1;
        Enemy enemyToFace = null;

        ReadOnlyCollection<Enemy> activeEnemies = EnemyController.Instance.ActiveEnemies;

        if (activeEnemies.Count != 0)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                float distance = Vector3.Distance(enemy.gameObject.transform.position, position);
                if (distance <= range)
                {
                    distanceToEnemy = (distance < distanceToEnemy) ? distance : distanceToEnemy;
                    enemyToFace = enemy;
                }
            }
        }

        return enemyToFace;
    }

}
