using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunScript : Tower
{
    public override bool Attack()
    {
        if(!Spinning())
            Animator.Play("Spin Up");

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Spin"))
        {
            _targetedEnemy.TakeDamage(Damage, AttackType);
            return true;
        }

        return false;
    }

    public override Enemy PickEnemy()
    {
        Enemy pick = PickEnemyFactory.PickClosestEnemy(Range, transform.position);

        if((pick == null || !pick.isActiveAndEnabled) && Spinning())
            Animator.Play("Spin Down");

        return pick;
    }

    private bool Spinning()
    {
        return Animator.GetCurrentAnimatorStateInfo(0).IsName("Spin Up") ||
               Animator.GetCurrentAnimatorStateInfo(0).IsName("Spin");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
