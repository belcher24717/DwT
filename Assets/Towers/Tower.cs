using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    
    public AttackType AttackType;
    protected Enemy _targetedEnemy;

    public ParticleSystem[] ShotEffects;
    public GameObject TurretSwivel;
    public Animator Animator;
    public int TurnSpeed = 200;
    public double FireRate = 1.0;
    public double Range = 30;
    public int Damage;

    private bool _canAttack;
    private DateTime _lastAttack;

    abstract public bool Attack();
    abstract public Enemy PickEnemy();

    public void FaceEnemy()
    {
        if (TurretSwivel == null || _targetedEnemy == null || !_targetedEnemy.isActiveAndEnabled)
        {
            _canAttack = false;
            return;
        }

        var lookPos =  transform.position - _targetedEnemy.transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos) * Quaternion.Euler(-90, 0, 0);
        TurretSwivel.transform.rotation = Quaternion.Slerp(TurretSwivel.transform.rotation, rotation, Time.deltaTime * TurnSpeed);

        _canAttack = Quaternion.Angle(TurretSwivel.transform.rotation, rotation) < 30 && _lastAttack.AddSeconds(FireRate) < DateTime.Now;
    }

    private void TowerAttack()
    {
        if (!_canAttack)
            return;

        //try to attack
        if (Attack())
        {
            //we attacked, so play the effects
            if(ShotEffects != null)
                foreach (ParticleSystem p in ShotEffects)
                    p?.Play();

            _lastAttack = DateTime.Now;
        }
    }

    private bool TargetEnemyInRange()
    {
        return Vector3.Distance(_targetedEnemy.gameObject.transform.position, transform.position) <= Range;
    }

    // Use this for initialization
    void Start()
    {
        _targetedEnemy = null;
        _canAttack = false;
        _lastAttack = DateTime.Now - TimeSpan.FromSeconds(FireRate);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_targetedEnemy == null || !_targetedEnemy.isActiveAndEnabled || !TargetEnemyInRange())
            _targetedEnemy = PickEnemy();
        else
        {
            FaceEnemy();
            TowerAttack();
        }
    }
}
