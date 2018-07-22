using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class PlayerTalentsScript : MonoBehaviour
{
    public static PlayerTalentsScript Instance;
    public TalentTree[] TalentTrees;
    public TextMeshProUGUI TalentButton;

    // Use this for initialization
    void Start ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ShowTalentButton()
    {
        TalentButton.gameObject.transform.parent.gameObject.SetActive(true);
        TalentTree tree = GetCurrentTree();
        int numUnlocked = tree.Root.NumUnlocked();
        Talent talent = tree.Root.GetNextTalent(numUnlocked - 1);

        if (talent?.Unlocked != true || Tower.PlayerSelectedTower.NumTalents == numUnlocked)
            TalentButton.SetText($"Unlock Tier {numUnlocked+1}");
        else if(Tower.PlayerSelectedTower.NumTalents <= numUnlocked)
        {
            TalentButton.SetText($"{talent.Name}\n" +
                                 $"{talent.TalentCost} TB");
        }
    }

    private TalentTree GetCurrentTree()
    {
        return TalentTrees.Where(t => t.TowerType == Tower.PlayerSelectedTower?.TowerType).FirstOrDefault();
    }

    public void HideTalentButton()
    {
        TalentButton.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void UpgradeTalent()
    {
        TalentTree tree = GetCurrentTree();
        tree?.Root.UpgradeTalent(Tower.PlayerSelectedTower.NumTalents);
        ShowTalentButton();
    }
}

[System.Serializable]
public class TalentTree
{
    public Talent Root;
    public TowerType TowerType;
}