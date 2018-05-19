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
            if (_targetedEnemies.Count == 1)
                _targetedEnemies[0]?.TakeDamage(Damage, AttackType);
            return true;
        }

        return false;
    }

    public override List<Enemy> PickEnemies()
    {
        List<Enemy> pickList = PickEnemyFactory.PickClosestEnemy(Range, transform.position);
        Enemy pick = (pickList.Count == 1) ? pickList[0] : null;

        if((pick == null || !pick.isActiveAndEnabled) && Spinning())
            Animator.Play("Spin Down");

        return pickList;
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
