using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    public void UpgradeRange()
    {
        Tower.PlayerSelectedTower.UpgradeRange();
    }

    public void UpgradeFireRate()
    {
        Tower.PlayerSelectedTower.UpgradeFireRate();
    }

    public void UpgradeDamage()
    {

        Tower.PlayerSelectedTower.UpgradeDamage();
    }

    public void UpgradeMaxTargetEnemies()
    {
        Tower.PlayerSelectedTower.UpgradeMaxTargetEnemies();
    }
}
