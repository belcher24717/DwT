using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickSpawnScript : MonoBehaviour
{
    private RaycastHit hit;                             // The Ray from camera to point-of-click
    [SerializeField]
    private Grid grid;                                  // The grid for placing towers
    private static GameObject cursorTower;
    private GameObject placedCursorTower;
    protected bool gridPositionChanged;
    private ArrayList placedTowers = new ArrayList();   // A list of all placed towers

    private static List<SelectTowerScript> _towers;

    private static GameObject selectedTower;
    public static GameObject SelectedTower
    {
        get
        {
            if (selectedTower == null)
                cursorTower = selectedTower = _towers.FirstOrDefault(t => t.Selected)?.Tower;
            return selectedTower;
        }
    }

    private Vector3 gridPosition;
    public Vector3 GridPosition
    {
        get
        {
            return gridPosition;
        }
        private set
        {
            if (gridPosition != value)
            {
                gridPositionChanged = true;
                gridPosition = value;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        _towers = new List<SelectTowerScript>(transform.GetComponentsInChildren<SelectTowerScript>());
    }

    // Update is called once per frame
    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool rayHit = Physics.Raycast(ray, out hit);

        // only place towers on floor
        if (rayHit && hit.transform.name == "Floor")
        {
            GridPosition = grid.GetNearestPointOnGrid(hit.point);

            // if girdPosition changed, place new cursorTower and cleanup old
            if (gridPositionChanged && SelectedTower != null)
            {
                gridPositionChanged = false;
                PlaceTowerOnGrid(hit.point, true);
            }

            // if left mouse click, attempt to place tower
            if (Input.GetMouseButton(0))
                 PlaceTowerOnGrid(hit.point, false);
        }
        else
        {
            if (placedCursorTower != null)
                GameObject.Destroy(placedCursorTower);
        }
	}

    protected void ResetSelection()
    {
        _towers.ForEach(t => t.Selected = false);
        selectedTower = null;
    }

    void PlaceTowerOnGrid (Vector3 buildPoint, bool placeCursorTower)
    {

        GameObject towerToUse = placeCursorTower ? cursorTower : SelectedTower;
        if (towerToUse == null)
            return;

        // if a tower already exists in this location, don't place it
        if (!placeCursorTower)
        {
            foreach (GameObject tower in placedTowers)
            {
                if (GridPosition == tower.transform.position)
                {
                    return;
                }
            }
        }

        // delete old placed cursor tower
        if (placedCursorTower != null)
            GameObject.Destroy(placedCursorTower);

        // place the tower
        GameObject placedTower = Instantiate(towerToUse, GridPosition, Quaternion.identity);
        if (!placeCursorTower)
            placedTowers.Add(placedTower);
        else
            placedCursorTower = placedTower; 
    }
}
