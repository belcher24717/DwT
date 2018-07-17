using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CannonScript : Tower
{
    public CannonShellScript Shell;
    public Transform ShellSpawnLocation;

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

    public override bool Attack()
    {
        CannonShellScript shell = Instantiate(Shell, ShellSpawnLocation.position, Quaternion.identity);
        if (_targetedEnemies.Count > 0)
        {
            shell.Damage = (int)DamageUpgrades[_damageUpgradeIndex].Value;
            shell.Target = _targetedEnemies[0]?.transform;

            Animator.Play("Shoot");
        }

        return true;
    }

    public override List<Enemy> PickEnemies()
    {
        return PickEnemyFactory.PickClosestEnemy(RangeUpgrades[_rangeUpgradeIndex].Value, transform.position);
    }
}
