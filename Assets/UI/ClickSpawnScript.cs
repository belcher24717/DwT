using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickSpawnScript : MonoBehaviour
{
    private Vector3 buildPoint;                         // the point to build the tower
    private RaycastHit hit;                             // The Ray from camera to point-of-click
    [SerializeField]
    private Grid grid;                                  // The grid for placing towers

    private ArrayList placedTowers = new ArrayList();   // A list of all placed towers

    private static List<SelectTowerScript> _towers;

    // Use this for initialization
    void Start()
    {
        _towers = new List<SelectTowerScript>(transform.GetComponentsInChildren<SelectTowerScript>());
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // only spawn if the floor is clicked
                if (hit.transform.name == "Floor")
                {
                    PlaceTowerOnGrid(hit.point);
                }
            }
        }
	}

    protected void ResetSelection()
    {
        _towers.ForEach(t => t.Selected = false);
    }

    void PlaceTowerOnGrid (Vector3 buildPoint)
    {
        GameObject selectedTower = _towers.FirstOrDefault(t => t.Selected)?.Tower;
        if (selectedTower == null)
            return;

        Vector3 gridPosition = grid.GetNearestPointOnGrid(buildPoint);

        // if a tower already exists in this location, don't place it
        foreach (GameObject tower in placedTowers)
        {
            if (gridPosition == tower.transform.position)
            {
                return;
            }
        }

        // place the tower
        placedTowers.Add(Instantiate(selectedTower, gridPosition, Quaternion.identity));
    }
}
