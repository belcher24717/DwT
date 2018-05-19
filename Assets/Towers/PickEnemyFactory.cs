using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class PickEnemyFactory
{

    public static List<Enemy> PickClosestEnemy(double range, Vector3 position)
    {
        double distanceToEnemy = range + 1;
        Enemy enemyToFace = null;
        List<Enemy> enemies = new List<Enemy>();

        ReadOnlyCollection<Enemy> activeEnemies = EnemyController.Instance.ActiveEnemies;

        if (activeEnemies.Count > 0)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                Vector3? enemyPosition = enemy?.gameObject?.transform?.position;
                if (!enemyPosition.HasValue)
                    continue;

                float distance = Vector3.Distance(enemyPosition.Value, position);
                if (distance <= range)
                {
                    //distanceToEnemy = (distance < distanceToEnemy) ? distance : distanceToEnemy;
                    //enemyToFace = enemy;
                    if (distance < distanceToEnemy)
                    {
                        distanceToEnemy = distance;
                        enemyToFace = enemy;
                    }
                }
            }
        }

        enemies.Add(enemyToFace);
        return enemies;
    }

    public static List<Enemy> PickAllEnemiesInRange(double range, Vector3 position)
    {
        float distanceToEnemy;
        List<Enemy> enemiesInRange = new List<Enemy>();

        ReadOnlyCollection<Enemy> activeEnemies = EnemyController.Instance.ActiveEnemies;

        if (activeEnemies.Count > 0)
        {
            foreach (Enemy enemy in activeEnemies)
            {
                Vector3? enemyPosition = enemy?.gameObject?.transform?.position;
                if (!enemyPosition.HasValue)
                    continue;

                distanceToEnemy = Vector3.Distance(enemyPosition.Value, position);
                if (distanceToEnemy <= range)
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }

        return enemiesInRange;
    }

}
