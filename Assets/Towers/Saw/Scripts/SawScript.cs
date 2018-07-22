using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : Tower
{
    public override bool Attack()
    {
        foreach(Enemy enemy in _targetedEnemies)
            enemy?.TakeDamage((int)DamageUpgrades[DamageUpgradeIndex].Value, AttackType);
        return true;
    }

    public override List<Enemy> PickEnemies()
    {
        return PickEnemyFactory.PickEnemiesInRange(RangeUpgrades[RangeUpgradeIndex].Value, transform.position, (int)MaxTargetEnemiesUpgrades[MaxTargetEnemiesUpgradeIndex].Value);
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
