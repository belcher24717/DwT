using System;
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

    public Camera UICamera;
    public GameObject EnemySpawns;
    public Transform SpawnParent;
    [HideInInspector]
    public static List<GameObject> Spawners = new List<GameObject>();
    [HideInInspector]
    public GameObject Destination;

    private static Tower selectedTower;            // a static reference to the currently selected tower
    public static Tower SelectedTower              // property
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

        if (Spawners.Count == 0)
        {
            var spawns = GameObject.FindGameObjectsWithTag("EnemySpawner");
            foreach (GameObject spawn in spawns)
                Spawners.Add(spawn);
        }
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (IsWaveActive())
            return;

        if (Input.GetMouseButton(1))
            ClearTowerSelection();

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

    private void ClearTowerSelection()
    {
        ResetSelection();
        DestroyPlacedCursorTower();
        Tower.DeselectTower();
    }

    private bool IsWaveActive()
    {
        bool spawnersActive = Spawners.Any(x => x.GetComponent<EnemySpawner>() != null && x.GetComponent<EnemySpawner>().WaveActive);
        bool enemiesAlive = EnemySpawns.transform.childCount > 0;

        bool waveActive = (spawnersActive || enemiesAlive);
        if (waveActive)
        {
            if (UICamera.isActiveAndEnabled)
                InvokeRepeating("ShrinkUICamera", 0, 0.1f); //UICamera.enabled = false;
            ClearTowerSelection();
        }
        else
        {
            if (!UICamera.isActiveAndEnabled)
            {
                UICamera.enabled = true;
                InvokeRepeating("GrowUICamera", 0, 0.1f); //UICamera.enabled = true;
            }
        }

        // if any spawners are active or any enmies are alive, return true
        return waveActive;
    }

    private void ShrinkUICamera()
    {
        Rect rect = new Rect(UICamera.rect);
        rect.height -= 0.01f;
        if (rect.height < 0)
            rect.height = 0;

        UICamera.rect = rect;

        if (rect.height <= 0)
        {
            UICamera.enabled = false;
            CancelInvoke();
        }
    }

    private void GrowUICamera()
    {
        Rect rect = new Rect(UICamera.rect);
        rect.height += 0.01f;
        UICamera.rect = rect;

        if (rect.height >= 0.1f)
            CancelInvoke();
    }

    protected void ChangeSpawnParentLayer(int layer)
    {
        foreach (Transform trans in SpawnParent.GetComponentsInChildren<Transform>(true))
            trans.gameObject.layer = layer;
    }

    protected void ResetSelection()
    {
        _towers.ForEach(t => t.Selected = false);
        selectedTower = null;
        cursorTower = null;
        ChangeSpawnParentLayer(0);
    }

    private bool PathExists()
    {
        NavMeshPath path = new NavMeshPath();
        foreach (GameObject spawner in Spawners)
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
        bool canPlace = !placedTowers.Any(t => t.transform.position == GridPosition) && (SelectedTower != null && SelectedTower.BaseCost <= PlayPurseScript.Instance.Balance);
        
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
            placedTowers.Add(Instantiate(SelectedTower.gameObject, GridPosition, SelectedTower.gameObject.transform.rotation, SpawnParent));
            PlayPurseScript.Instance.ChangeBalance(-SelectedTower.BaseCost);
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
