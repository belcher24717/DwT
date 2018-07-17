using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PounderScript : Tower
{
    public StatusEffect StatusEffect;

    public override bool Attack()
    {
        Animator.Play("Shoot");

        foreach (Enemy enemy in _targetedEnemies)
        {
            var effect = enemy.GetComponents<StatusEffect>().Where(e => e.Id == StatusEffect.Id).FirstOrDefault();
            if (effect == null)
                Instantiate(StatusEffect.gameObject, enemy.transform);
            else
                effect.Refresh();
        }
        return true;
    }

    public override List<Enemy> PickEnemies()
    {
        return PickEnemyFactory.PickEnemiesInRange(RangeUpgrades[_rangeUpgradeIndex].Value, transform.position);
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
