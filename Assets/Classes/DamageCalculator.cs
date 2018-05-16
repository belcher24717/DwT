using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DamageCalculator {

    // potentially make the .8, 1.0, and 1.2 return values changeable via talents? 

    public static int CalculateDamage(int damage, ArmorType armor, AttackType attack)
    {
        // if chaos (true damage) don't take armor into account
        if (attack == AttackType.Chaos)
        {
            if (armor == ArmorType.None)
                return (int)Math.Round(damage * 1.2);
            else
                return damage;
        }

        // else calculate proper damage for armor v attack types
        switch (armor)
        {
            case ArmorType.None:
                return (int)Math.Round(damage * 1.2);
            case ArmorType.Light:
                if (attack == AttackType.HighImpact || attack == AttackType.Explosive)
                    return (int)Math.Round(damage * 0.8);
                else
                    return (int)Math.Round(damage * 1.2);
            case ArmorType.Medium:
                if (attack == AttackType.Shrapnel)
                    return (int)Math.Round(damage * 0.8);
                else if (attack == AttackType.Piercing)
                    return damage;
                else
                    return (int)Math.Round(damage * 1.2);
            case ArmorType.Heavy:
                if (attack == AttackType.Shrapnel || attack == AttackType.Piercing)
                    return (int)Math.Round(damage * 0.8);
                else if (attack == AttackType.HighImpact)
                    return damage;
                else
                    return (int)Math.Round(damage * 1.2);
            default:
                return damage;
        }
           
    }
}
