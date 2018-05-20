using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : Tower
{
    public override bool Attack()
    {
        foreach(Enemy enemy in _targetedEnemies)
            enemy?.TakeDamage(Damage, AttackType);
        return true;
    }

    public override List<Enemy> PickEnemies()
    {
        return PickEnemyFactory.PickEnemiesInRange(Range, transform.position);
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
