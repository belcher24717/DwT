using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DamageOverTime : StatusOverTime
{
    public AttackType AttackType;

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

    protected override void Tick()
    {
        Enemy scriptToAffect = this.gameObject.transform.parent.GetComponent<Enemy>();
        scriptToAffect.TakeDamage(TickValue, AttackType);
    }
}
