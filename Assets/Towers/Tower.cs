using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected int _damage;
    protected int _fireRate;
    protected int _range;
    public Particle[] ShotEffects;
    protected Enemy _targetedEnemy;


    public abstract void Attack();
    public abstract GameObject PickEnemy();

    public void FaceEnemy()
    {

    }


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject g = PickEnemy();
    }
}
