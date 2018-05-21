using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalScript : MonoBehaviour
{
    public int Health;
    private int _currentHealth;
    public Image HealthBar;
    // Use this for initialization
    void Start()
    {
        _currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DamageEffect();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            HealthBar.fillAmount = 0;
            GameOver();
        }
        else
        {
            HealthBar.fillAmount = _currentHealth / (float)Health;
        }
    }

    public void GameOver()
    {

    }

    void DamageEffect()
    {

    }

}
