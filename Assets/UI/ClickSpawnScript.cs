using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawnScript : MonoBehaviour
{

    public GameObject tower;        // The tower prefab to place on click (will be passed in, will need checks for resources, etc)
    public LayerMask mask;          // Not sure what this is atm?

    private Vector3 buildPoint;     // the point to build the tower
    private RaycastHit hit;         // The Ray from camera to point-of-click 

    // Use this for initialization
    void Start ()
    {
        // eventually this will be removed. A tower will be selected and passed in...
        //TODO: REMOVE THIS
        tower = GameObject.Find("Tower");
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, mask))
            {
                // only spawn if the floor is clicked
                if (hit.transform.name == "Floor")
                {
                    buildPoint = hit.point;
                    Instantiate(tower, buildPoint, Quaternion.identity);
                }
            }
        }
	}
}
