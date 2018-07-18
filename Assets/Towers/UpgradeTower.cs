using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    public TextMeshProUGUI DamageUpgrade;
    public TextMeshProUGUI RangeUpgrade;
    public TextMeshProUGUI FireRateUpgrade;
    public TextMeshProUGUI MaxTargetEnemiesUpgrade;

    public static UpgradeTower Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        HideUpgrades();
    }

    public void UpgradeRange()
    {
        Tower.PlayerSelectedTower.UpgradeRange();
        ShowRange();
    }

    public void UpgradeFireRate()
    {
        Tower.PlayerSelectedTower.UpgradeFireRate();
        ShowFireRate();
    }

    public void UpgradeDamage()
    {
        Tower.PlayerSelectedTower.UpgradeDamage();
        ShowDamage();
    }

    public void UpgradeMaxTargetEnemies()
    {
        Tower.PlayerSelectedTower.UpgradeMaxTargetEnemies();
        ShowMaxTargetEnemies();
    }

    public void ShowUpgrades()
    {
        ShowDamage();
        ShowRange();
        ShowMaxTargetEnemies();
        ShowFireRate();
    }

    private void ShowDamage()
    {
        DamageUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.DamageUpgrades.Count - 1 == Tower.PlayerSelectedTower._damageUpgradeIndex)
        {
            DamageUpgrade.SetText($"Damage:\n" +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower._damageUpgradeIndex].Value}");
        }
        else
        {
            DamageUpgrade.SetText($"Damage:\n" +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower._damageUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower._damageUpgradeIndex + 1].Value}\n" +
                $"${Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower._damageUpgradeIndex + 1].Cost}");
        }
    }

    private void ShowRange()
    {
        RangeUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.RangeUpgrades.Count - 1 == Tower.PlayerSelectedTower._rangeUpgradeIndex)
        {
            RangeUpgrade.SetText($"Range:\n" +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower._rangeUpgradeIndex].Value}");
        }
        else
        {
            RangeUpgrade.SetText($"Range:\n" +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower._rangeUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower._rangeUpgradeIndex + 1].Value}\n" +
                $"${Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower._rangeUpgradeIndex + 1].Cost}");
        }
    }

    private void ShowFireRate()
    {
        FireRateUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.FireRateUpgrades.Count - 1 == Tower.PlayerSelectedTower._fireRateUpgradeIndex)
        {
            FireRateUpgrade.SetText($"Fire Rate:\n" +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower._fireRateUpgradeIndex].Value}");
        }
        else
        {
            FireRateUpgrade.SetText($"Fire Rate:\n" +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower._fireRateUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower._fireRateUpgradeIndex + 1].Value}\n" +
                $"${Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower._fireRateUpgradeIndex + 1].Cost}");
        }
    }

    private void ShowMaxTargetEnemies()
    {
        MaxTargetEnemiesUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades.Count - 1 == Tower.PlayerSelectedTower._maxTargetEnemiesUpgradeIndex)
        {
            MaxTargetEnemiesUpgrade.SetText($"Max Targets:\n" +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower._maxTargetEnemiesUpgradeIndex].Value}");
        }
        else
        {
            MaxTargetEnemiesUpgrade.SetText($"Max Targets:\n" +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower._maxTargetEnemiesUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower._maxTargetEnemiesUpgradeIndex + 1].Value}\n" +
                $"${Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower._maxTargetEnemiesUpgradeIndex + 1].Cost}");
        }
    }

    public void HideUpgrades()
    {
        DamageUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        FireRateUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        MaxTargetEnemiesUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        RangeUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
