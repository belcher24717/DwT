using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawnScript : MonoBehaviour
{

    public GameObject tower;                            // The tower prefab to place on click (will be passed in, will need checks for resources, etc)

    private Vector3 buildPoint;                         // the point to build the tower
    private RaycastHit hit;                             // The Ray from camera to point-of-click 
    private Grid grid;                                  // The grid for placing towers

    private ArrayList placedTowers = new ArrayList();   // A list of all placed towers

    void Awake ()
    {
        grid = FindObjectOfType<Grid>();
    }

    // Use this for initialization
    void Start ()
    {

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

    void PlaceTowerOnGrid (Vector3 buildPoint)
    {
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
        placedTowers.Add(Instantiate(tower, gridPosition, Quaternion.identity));
    }
}
