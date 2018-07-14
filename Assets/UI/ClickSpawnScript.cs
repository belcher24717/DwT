using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ClickSpawnScript : MonoBehaviour
{
    [SerializeField]
    private Grid grid;                                  // The grid for placing towers
    private RaycastHit hit;                             // The Ray from camera to point-of-click
    private GameObject placedCursorTower;               // a reference to the cursor tower that's actually placed
    private List<GameObject> placedTowers = new List<GameObject>();   // A list of all placed towers
    private static List<SelectTowerScript> _towers;     // a static list of all selectable towers (UI)

    protected bool gridPositionChanged;                 // true if the cursor has changed grid positions

    public Transform SpawnParent;
    public List<EnemySpawner> Spawners = new List<EnemySpawner>();
    public GameObject Destination;

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

    private static TowerCursorScript cursorTower;       // a static reference to the currently selected tower
    public static TowerCursorScript CursorTower         // property
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
        if (rayHit && hit.transform.tag == "Floor")
        {
            GridPosition = grid.GetNearestPointOnGrid(hit.point);

            // if girdPosition changed, place new cursorTower and cleanup old
            if (gridPositionChanged && SelectedTower != null && !Input.GetMouseButton(0))
            {
                gridPositionChanged = false;
                PlaceTowerOnGrid(hit.point, true);
            }

            // if left mouse click, attempt to place tower
            if (Input.GetMouseButtonDown(0))
                 PlaceTowerOnGrid(hit.point, false);
        }
        else
        {
            if (placedCursorTower != null)
                DestroyPlacedCursorTower();
            gridPositionChanged = true;
        }
	}

    protected void ResetSelection()
    {
        _towers.ForEach(t => t.Selected = false);
        selectedTower = null;
        cursorTower = null;
    }

    private bool PathExists()
    {
        NavMeshPath path = new NavMeshPath();
        foreach (EnemySpawner spawner in Spawners)
        {
            spawner?.GetComponent<NavMeshAgent>().CalculatePath(Destination.transform.position, path);
            if (path.status != NavMeshPathStatus.PathComplete)
                return false;
        }
        return true;
    }

    void PlaceTowerOnGrid(Vector3 buildPoint, bool placeCursorTower)
    {
        //first check for canPlace, is the place taken?
        bool canPlace = !placedTowers.Any(t => t.transform.position == GridPosition);

        //if there is no cursor tower, don't even place anything
        if (CursorTower == null)
            return;

        //set up a default to remember the previous cursors value
        bool cursorDefaultCanPlace = true;

        // delete old placed cursor tower, extract old canPlace value
        if (placedCursorTower != null)
            cursorDefaultCanPlace = DestroyPlacedCursorTower();

        //always place cursor tower first, so it can be used to know if there is a path
        placedCursorTower = Instantiate(CursorTower.gameObject, GridPosition, Quaternion.identity, SpawnParent);

        //default set the CanPlace to: if we canPlace at this time && the old value from the previous cursor && if we are a cursor tower
        placedCursorTower?.GetComponent<TowerCursorScript>()?.CanPlace(canPlace && cursorDefaultCanPlace && placeCursorTower);

        //if you can't place, and you aren't a cursor tower, return
        if (!canPlace && !placeCursorTower)
            return;

        //if we got here, evaluate the navmesh next frame
        StartCoroutine(FinishPlaceTowerOnGrid(canPlace, placeCursorTower));
    }

    private IEnumerator FinishPlaceTowerOnGrid(bool canPlace, bool placeCursorTower)
    {
        //wait for 1 frame
        yield return 0;

        //if we can place at this point check the path
        if (canPlace)
            canPlace = PathExists();

        //if we're placing a real tower, and the path was ok
        if (placedCursorTower?.transform.position == GridPosition && !placeCursorTower && canPlace)
        {
            placedTowers.Add(Instantiate(SelectedTower, GridPosition, SelectedTower.transform.rotation, SpawnParent));
            DestroyPlacedCursorTower();
        }
        
        //always correct the cursor towers color. if null, this will do nothing
        placedCursorTower?.GetComponent<TowerCursorScript>()?.CanPlace(canPlace);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>previous can place value</returns>
    private bool DestroyPlacedCursorTower()
    {
        bool? couldPlace = placedCursorTower?.GetComponent<TowerCursorScript>()?.GoodPlace;
        GameObject.Destroy(placedCursorTower);
        placedCursorTower = null;

        return couldPlace == true;
    }
}
