using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_ConcussionTalent : Talent
{

    protected override void ApplyTalent()
    {
        if (Tower.PlayerSelectedTower.TowerType != TowerType.Sniper)
            return;

        SniperScript sniper = Tower.PlayerSelectedTower as SniperScript;
        sniper.ApplyConcussion = true;


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
