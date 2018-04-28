using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawnScript : MonoBehaviour
{

    public GameObject tower;        // The tower prefab to place on click (will be passed in, will need checks for resources, etc)

    private Vector3 buildPoint;     // the point to build the tower
    private RaycastHit hit;         // The Ray from camera to point-of-click 

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
                    buildPoint = hit.point;
                    Instantiate(tower, buildPoint, Quaternion.identity);
                }
            }
        }
	}
}
