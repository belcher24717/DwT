using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerScript : MonoBehaviour {

    public GameObject tower;                                // The Tower that this script will spawn on click
    public bool isSelected;

    private ArrayList towerSelectors = new ArrayList();     // A list of all tower selectors

    void Awake()
    {
        // every Tower Selector has a list of every existing Tower Selector in the game
        towerSelectors.Add(GameObject.Find("Tower Selector 1"));
        towerSelectors.Add(GameObject.Find("Tower Selector 2"));
        // ...
    }

    // Use this for initialization
    void Start ()
    {
        isSelected = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnMouseDown()
    {

        if (!isSelected)
        {
            // deselect current selection
            foreach (GameObject selector in towerSelectors)
            {
                SelectTowerScript script = selector.GetComponent<SelectTowerScript>(); 
                if (script.isSelected)
                {
                    script.isSelected = false;
                    selector.GetComponent<Renderer>().material.color = Color.grey;
                }
            }

            // set the tower to spawn, set selected Tower Selector color to red
            isSelected = true;
            ClickSpawnScript.selectedTower = tower;
            GetComponent<Renderer>().material.color = Color.red;

        }

    }

}
