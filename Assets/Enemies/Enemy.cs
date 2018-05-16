﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public int Speed;
    public ArmorType ArmorType;
    public int SlowResistance;
    public int GoldValue;
    public int Damage;

    // may need damage and death information here (particle effects, animations, etc)...

    public void TakeDamage(int damage, AttackType attack)
    {
        int damageDealt = DamageCalculator.CalculateDamage(damage, ArmorType, attack);
        if ((Health - damageDealt) > 0)
        {
            DamageEffect();
            Health -= damageDealt;
        }
        else
        {
            Health = 0;
            DeathEffect();
            this.gameObject.SetActive(false);
            Destroy(this, 10f);
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // handles display of damage effects / particles
    void DamageEffect()
    {

    }

    void DeathEffect()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            //apply damage to crystal...
            // theCrystal.Damage(_damage);
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        EnemyController.Instance.RemoveEnemy(this);
    }

}
