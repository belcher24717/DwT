using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int Health;
    private int _currentHealth;
    public float Speed;
    public ArmorType ArmorType;
    public int SlowResistance;
    public int GoldValue;
    public int Damage;
    public Image HealthBar;

    // may need damage and death information here (particle effects, animations, etc)...

    public void TakeDamage(int damage, AttackType attack)
    {
        int damageDealt = DamageCalculator.CalculateDamage(damage, ArmorType, attack);
        DamageEffect();
        if ((_currentHealth - damageDealt) > 0)
        {
            _currentHealth -= damageDealt;
            HealthBar.fillAmount = _currentHealth / (float)Health;
        }
        else
        {
            _currentHealth = 0;
            HealthBar.fillAmount = 0;
            DeathEffect();
            this.gameObject.SetActive(false);
        }

    }

    // Use this for initialization
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = Speed;
        _currentHealth = Health;
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
            other.GetComponent<CrystalScript>().TakeDamage(Damage);
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        EnemyController.Instance.RemoveEnemy(this);
        Destroy(gameObject, 5f);
    }

}
