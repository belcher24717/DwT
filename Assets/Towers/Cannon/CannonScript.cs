using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CannonScript : Tower
{
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
        Animator.Play("Shoot");
        return true;
    }

    public override void PerformAttack()
    {

    }

    public override Enemy PickEnemy()
    {
        return PickEnemyFactory.PickClosestEnemy(Range, transform.position);
    }
}
