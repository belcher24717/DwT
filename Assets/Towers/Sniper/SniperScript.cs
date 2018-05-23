using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : Tower
{
    public override bool Attack()
    {
        Animator.Play("Shoot");

        if (_targetedEnemies.Count > 0)
            _targetedEnemies[0]?.TakeDamage(Damage, AttackType);

        return true;
    }

    public override List<Enemy> PickEnemies()
    {
        return PickEnemyFactory.PickClosestEnemy(Range, transform.position);
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
