using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    
    public AttackType AttackType;
    protected List<Enemy> _targetedEnemies;

    public ParticleSystem[] ShotEffects;
    public GameObject TurretSwivel;
    public Animator Animator;
    public int TurnSpeed = 200;
    public double FireRate = 1.0;
    public double Range = 30;
    public int Damage;
    public int MaxTargetEnemies = 1;

    private bool CanFace { get { return TurretSwivel != null; } }
    private bool CanAttack { get { return _isFacingTarget && _lastAttack.AddSeconds(FireRate) < DateTime.Now; } }
    private bool _isFacingTarget;
    private DateTime _lastAttack;

    abstract public bool Attack();
    abstract public List<Enemy> PickEnemies();

    // can be overriden in multi-target situations 
    public void FaceEnemy()
    {
        if (!CanFace)
            return;

        if (_targetedEnemies.Count < 1 || _targetedEnemies[0] == null || !_targetedEnemies[0].isActiveAndEnabled)
        {
            _isFacingTarget = false;
            return;
        }

        var lookPos =  transform.position - _targetedEnemies[0].transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos) * Quaternion.Euler(-90, 0, 0);
        TurretSwivel.transform.rotation = Quaternion.Slerp(TurretSwivel.transform.rotation, rotation, Time.deltaTime * TurnSpeed);

        _isFacingTarget = Quaternion.Angle(TurretSwivel.transform.rotation, rotation) < 30;
    }

    private void TowerAttack()
    {
        if (!_isFacingTarget || !CanAttack)
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

    private bool TargetEnemyInRange(Enemy enemy)
    {
        return Vector3.Distance(enemy.gameObject.transform.position, transform.position) <= Range;
    }

    // Use this for initialization
    protected void Start()
    {
        _targetedEnemies = new List<Enemy>();
        _isFacingTarget = true;
        _lastAttack = DateTime.Now - TimeSpan.FromSeconds(FireRate);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_targetedEnemies.Count < MaxTargetEnemies)
            _targetedEnemies = PickEnemies();
        else
        {
            for (int i = 0; i < _targetedEnemies.Count; i++)
            {
                Enemy enemy = _targetedEnemies[i];

                if (enemy == null || !enemy.isActiveAndEnabled || !TargetEnemyInRange(enemy))
                {
                    _targetedEnemies = PickEnemies();
                    break;
                }
            }
        }

        if (CanFace)
            FaceEnemy();

        if (_targetedEnemies.Count > 0)
            TowerAttack();
    }
}
