using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PounderScript : Tower
{
    public StatusEffect StatusEffect;
    public float SlowPercent;
    public float SlowDuration;

    public override bool Attack()
    {
        Animator.Play("Shoot");

        foreach (Enemy enemy in _targetedEnemies)
        {
            var effect = Instantiate(StatusEffect, enemy.transform);
            effect.EffectPercent = SlowPercent;
            effect.Duration = SlowDuration;
        }
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
