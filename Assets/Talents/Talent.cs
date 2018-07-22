using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Talent : MonoBehaviour
{
    public int UnlockCost;
    public bool Unlocked;
    public int TalentCost;
    public string Name;
    public string Description;
    public Talent[] NextTalents;
    abstract protected void ApplyTalent();

    public void UnlockTalent()
    {
        PlayPurseScript.Instance.ChangeBalance(-UnlockCost);
        Unlocked = true;
    }

    public int NumUnlocked()
    {
        if (!Unlocked)
            return 0;

        Talent nextTalent = NextTalents.Where(t => t.Unlocked).FirstOrDefault();
        if (nextTalent != null)
            return 1 + nextTalent.NumUnlocked();
        else return 1;
    }

    public Talent GetNextTalent(int numUpgrade)
    {
        if (Unlocked && numUpgrade == 0)
        {
            return this;
        }
        else
        {
            Talent nextTalent = NextTalents.Where(t => t.Unlocked).FirstOrDefault();
            if (nextTalent != null)
                nextTalent.UpgradeTalent(numUpgrade - 1);
        }
        return null;
    }

    public void UpgradeTalent(int numUpgrade)
    {
        if (Unlocked && numUpgrade == 0)
        {
            PlayPurseScript.Instance.ChangeBalance(-TalentCost);
            ApplyTalent();
            Tower.PlayerSelectedTower.NumTalents++;
        }
        else
        {
            Talent nextTalent = NextTalents.Where(t => t.Unlocked).FirstOrDefault();
            if (nextTalent != null)
                nextTalent.UpgradeTalent(numUpgrade - 1);
        }
    }
}

