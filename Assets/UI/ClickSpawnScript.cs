using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickSpawnScript : MonoBehaviour
{
    [SerializeField]
    private Grid grid;                                  // The grid for placing towers
    private RaycastHit hit;                             // The Ray from camera to point-of-click
    private GameObject placedCursorTower;               // a reference to the cursor tower that's actually placed
    private ArrayList placedTowers = new ArrayList();   // A list of all placed towers
    private static List<SelectTowerScript> _towers;     // a static list of all selectable towers (UI)

    protected bool gridPositionChanged;                 // true if the cursor has changed grid positions

    public Transform SpawnParent;

    private static GameObject selectedTower;            // a static reference to the currently selected tower
    public static GameObject SelectedTower              // property
    {
        get
        {
            if (selectedTower == null)
                selectedTower = _towers?.FirstOrDefault(t => t.Selected)?.Tower;
            return selectedTower;
        }
    }

    private static GameObject cursorTower;            // a static reference to the currently selected tower
    public static GameObject CursorTower              // property
    {
        get
        {
            if (cursorTower == null)
                cursorTower = _towers?.FirstOrDefault(t => t.Selected)?.CursorTower;
            return cursorTower;
        }
    }

    private Vector3 gridPosition;                       // the current grid position in relation to the cursor
    public Vector3 GridPosition                         // property
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
    void Awake()
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
        cursorTower = null;
    }

    void PlaceTowerOnGrid (Vector3 buildPoint, bool placeCursorTower)
    {
        GameObject towerToUse = placeCursorTower ? CursorTower : SelectedTower;
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

        GameObject placedTower = Instantiate(towerToUse, GridPosition, Quaternion.identity, SpawnParent);
        if (!placeCursorTower)  // place actual tower
            placedTowers.Add(placedTower);
        else                    // else place cursor tower 
            placedCursorTower = placedTower; 
    }
}
