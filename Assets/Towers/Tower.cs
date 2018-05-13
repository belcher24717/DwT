using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected int _damage;
    public double FireRate = 1.0;
    protected int _range;
    protected int _attackType;
    protected Enemy _targetedEnemy;

    public ParticleSystem[] ShotEffects;
    public GameObject TurretSwivel;
    public float TurnSpeed = 100;
    abstract public void Attack();
    abstract public Enemy PickEnemy();
    private bool _canAttack;
    private DateTime _lastAttack;
    public void FaceEnemy()
    {
        if (TurretSwivel == null || _targetedEnemy == null)
            return;

        var lookPos =  transform.position - _targetedEnemy.transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TurnSpeed);

        _canAttack = Quaternion.Angle(transform.rotation, rotation) < 30 && _lastAttack.AddSeconds(FireRate) < DateTime.Now;
    }

    private void TowerAttack()
    {
        //if you're not facing close enough, don't attack
        if (!_canAttack)
            return;

        _lastAttack = DateTime.Now;

        //we're going to attack now
        foreach (ParticleSystem p in ShotEffects)
            p.Play();

        Attack();
    }

    // Use this for initialization
    void Start()
    {
        _targetedEnemy = null;
        _canAttack = false;
        _lastAttack = DateTime.Now - TimeSpan.FromSeconds(-FireRate);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_targetedEnemy == null)
            _targetedEnemy = PickEnemy();
        else
        {
            FaceEnemy();
            TowerAttack();
        }
    }
}
