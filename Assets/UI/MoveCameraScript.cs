using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCameraScript : MonoBehaviour
{

    private int screenWidth;                // the width of the screen on game start
    private int screenHeight;               // the height of the screen on game start
    private float speed = 15f;              // the speed of the camera movement
    private float zoomSpeed = 15f;          // the speed of the camera zoom
    private float rotateSpeed = 30f;        // the speed of the camera rotation
    private bool hasFocus;                  // true if application has focus
    private float maxCameraZoom = 15f;      // the max amount you can zoom the camera
    private float maxCameraRotation = 45f;  // the max amount you can rotate the camera
    private float defaultCameraZoom;        // the default camera zoom (on start)
    private float defaultCameraRotation;    // the default camera rotation (on start) 

    // Use this for initialization
    void Start ()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        defaultCameraZoom = transform.position.y;
        defaultCameraRotation = transform.rotation.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: Should not double speed when common-direction keys are pressed, fix this...
        // move camera on WASD and Arrow Keys
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            MoveCameraLeft();
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            MoveCameraRight();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            MoveCameraUp();
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            MoveCameraDown();

        // move camera on mouse hitting edge
        if (Input.mousePosition.x > screenWidth && hasFocus)
            MoveCameraRight();
        if (Input.mousePosition.x < 0 && hasFocus)
            MoveCameraLeft();
        if (Input.mousePosition.y > screenHeight && hasFocus)
            MoveCameraUp();
        if (Input.mousePosition.y < 0 && hasFocus)
            MoveCameraDown();

        // magnify and rotate camera towards gamespace w/ mousewheel backward
        float temp = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (transform.position.y > defaultCameraZoom - maxCameraZoom)
                ZoomCameraForward();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (transform.position.y < defaultCameraZoom)
                ZoomCameraBackward();
        }

        // separate camera zoom and rotation? Rotation would go here...
    }

    void OnApplicationFocus(bool focus)
    {
        hasFocus = focus;
    }

    void MoveCameraRight()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
    }

    void MoveCameraUp()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.World);
    }

    void MoveCameraLeft()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.World);
    }

    void MoveCameraDown()
    {
        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
    }

    void ZoomCameraForward()
    {
        transform.Translate(new Vector3(0, -zoomSpeed * Time.deltaTime, 0), Space.World);
        transform.Rotate(new Vector3(1, 0, 0), -rotateSpeed * Time.deltaTime, Space.World);
    }

    void ZoomCameraBackward()
    {
        transform.Translate(new Vector3(0, zoomSpeed * Time.deltaTime, 0), Space.World);
        transform.Rotate(new Vector3(1, 0, 0), rotateSpeed * Time.deltaTime, Space.World);
    }

    //void OnGUI()
    //{
    //    GUI.Box(Rect((Screen.width / 2) - 140, 5, 280, 25), "Mouse Position = " + Input.mousePosition);
    //    GUI.Box(Rect((Screen.width / 2) - 70, Screen.height - 30, 140, 25), "Mouse X = " + Input.mousePosition.x);
    //    GUI.Box(Rect(5, (Screen.height / 2) - 12, 140, 25), "Mouse Y = " + Input.mousePosition.y);
    //}

}
