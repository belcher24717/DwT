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
    public PlayerTalentsScript PlayerTalents;
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
        ShowTalent();
    }

    private void ShowDamage()
    {
        DamageUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.DamageUpgrades.Count - 1 == Tower.PlayerSelectedTower.DamageUpgradeIndex)
        {
            DamageUpgrade.SetText($"Damage:\n" +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower.DamageUpgradeIndex].Value}");
        }
        else
        {
            DamageUpgrade.SetText($"Damage:\n" +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower.DamageUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower.DamageUpgradeIndex + 1].Value}\n" +
                $"{Tower.PlayerSelectedTower.DamageUpgrades[Tower.PlayerSelectedTower.DamageUpgradeIndex + 1].Cost} TB");
        }
    }

    private void ShowRange()
    {
        RangeUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.RangeUpgrades.Count - 1 == Tower.PlayerSelectedTower.RangeUpgradeIndex)
        {
            RangeUpgrade.SetText($"Range:\n" +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower.RangeUpgradeIndex].Value}");
        }
        else
        {
            RangeUpgrade.SetText($"Range:\n" +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower.RangeUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower.RangeUpgradeIndex + 1].Value}\n" +
                $"{Tower.PlayerSelectedTower.RangeUpgrades[Tower.PlayerSelectedTower.RangeUpgradeIndex + 1].Cost} TB");
        }
    }

    private void ShowFireRate()
    {
        FireRateUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.FireRateUpgrades.Count - 1 == Tower.PlayerSelectedTower.FireRateUpgradeIndex)
        {
            FireRateUpgrade.SetText($"Fire Rate:\n" +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower.FireRateUpgradeIndex].Value}");
        }
        else
        {
            FireRateUpgrade.SetText($"Fire Rate:\n" +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower.FireRateUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower.FireRateUpgradeIndex + 1].Value}\n" +
                $"{Tower.PlayerSelectedTower.FireRateUpgrades[Tower.PlayerSelectedTower.FireRateUpgradeIndex + 1].Cost} TB");
        }
    }

    private void ShowMaxTargetEnemies()
    {
        MaxTargetEnemiesUpgrade.gameObject.transform.parent.gameObject.SetActive(true);
        if (Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades.Count - 1 == Tower.PlayerSelectedTower.MaxTargetEnemiesUpgradeIndex)
        {
            MaxTargetEnemiesUpgrade.SetText($"Max Targets:\n" +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower.MaxTargetEnemiesUpgradeIndex].Value}");
        }
        else
        {
            MaxTargetEnemiesUpgrade.SetText($"Max Targets:\n" +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower.MaxTargetEnemiesUpgradeIndex].Value} >> " +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower.MaxTargetEnemiesUpgradeIndex + 1].Value}\n" +
                $"{Tower.PlayerSelectedTower.MaxTargetEnemiesUpgrades[Tower.PlayerSelectedTower.MaxTargetEnemiesUpgradeIndex + 1].Cost} TB");
        }
    }

    private void ShowTalent()
    {
        PlayerTalents.ShowTalentButton();
    }

    public void HideUpgrades()
    {
        DamageUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        FireRateUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        MaxTargetEnemiesUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        RangeUpgrade.gameObject.transform.parent.gameObject.SetActive(false);
        PlayerTalents.HideTalentButton();
    }
}
