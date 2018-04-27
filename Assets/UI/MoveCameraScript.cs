using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCameraScript : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // probably want this to also incorporate edge of screen mouse movement, also, should not double speed when common-direction keys are pressed...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-15f * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(15f * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 15f * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -15F * Time.deltaTime, 0));
        }

    }
}
