using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int _health;
    protected int _speed;
    protected int _armorValue;
    protected ArmorType[] _armorType;
    protected int _slowResistance;
    protected int _goldValue;
    protected int _damage;

    public void Damage(int value)
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
