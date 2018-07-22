using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : Tower
{
    public StatusEffect ConcussionStatusEffect;
    public bool ApplyConcussion { get; set; } = false;

    public override bool Attack()
    {
        Animator.Play("Shoot");

        if (_targetedEnemies.Count > 0)
        {
            _targetedEnemies[0]?.TakeDamage((int)DamageUpgrades[DamageUpgradeIndex].Value, AttackType);
            if (ApplyConcussion)
            {
                var effect = Instantiate(ConcussionStatusEffect, _targetedEnemies[0].gameObject.transform);
                effect.EffectPercent = -100;
                effect.Duration = 1;
            }
        }
        return true;
    }

    public override List<Enemy> PickEnemies()
    {
        return PickEnemyFactory.PickClosestEnemy(RangeUpgrades[RangeUpgradeIndex].Value, transform.position);
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
