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
        //_attackType = Enums.AttackType.
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override bool Attack()
    {
        CannonShellScript shell = Instantiate(Shell, ShellSpawnLocation.position, Quaternion.identity);
        shell.Damage = Damage;
        shell.Target = _targetedEnemy.transform;

        Animator.Play("Shoot");
        return true;
    }

    public override Enemy PickEnemy()
    {
        return PickEnemyFactory.PickClosestEnemy(Range, transform.position);
    }
}
