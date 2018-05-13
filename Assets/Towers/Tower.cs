using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected int _damage;
    protected double _fireRate;
    protected int _range;
    protected int _attackType;
    protected GameObject _targetedEnemy;

    public ParticleSystem[] ShotEffects;

    abstract public void Attack(Enemy enemy);
    abstract public GameObject PickEnemy();

    public void FaceEnemy()
    {
        //TODO Tower facing logic goes here...
    }


    // Use this for initialization
    void Start()
    {
        _targetedEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetedEnemy != null)
            FaceEnemy();
    }
}
