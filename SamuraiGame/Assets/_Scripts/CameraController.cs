using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Ken Miller
/// Controls the camera by mouse or with arrow keys 
/// horizontally on the X axis.
/// </summary>
public class CameraController : MonoBehaviour {
    //Speed for mouse camera movement
    public float speed = 20;
    //Speed for arrow buttons camera movement
    public float arrowButtonSpeed = 1;
    //Values to clamp the camera in world space
    public float minCamPos, maxCamPos;
    //Variable to hold the previous frame mouse location
    private Vector3 _prevMousePosition;
    //Variable to hold the current frame mouse location
    private Vector3 _newMousePosition;
    //The amount of distance between last frame and current frame
    private float _deltaMouseDistance;

    //Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Set the current and previous mouse position to be the same for correct distance calculations
            _newMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _prevMousePosition = _newMousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            //Find the delta mouse position and apply to the camera. The X position is clamped so the camera will only move a certain distance in the + or - X axis
            _prevMousePosition = _newMousePosition;
            _newMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _deltaMouseDistance = _newMousePosition.x - _prevMousePosition.x;
            Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x - _deltaMouseDistance * speed, minCamPos, maxCamPos), Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        
        //Get left and right arrow keyboard input and apply the amount on the horizontal axis to the camera position
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x + Input.GetAxis("Horizontal") * arrowButtonSpeed, minCamPos, maxCamPos), Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
