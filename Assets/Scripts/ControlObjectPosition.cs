using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjectPosition : MonoBehaviour {

    // the key(s) to use to control this object
    public KeyCode[] controlsKeys;

    // the key(s) to use to switch to rotate mode
    public KeyCode[] rotateKeys;

    // the key(s) to use to switch vertical axis
    public KeyCode[] vertSwitch;

    // The speed to move
    public float speed = 1;

    // the speed to rotate
    public float rotationSpeed = 1;

    // The object to control
    public GameObject controlled;

    // the object to rotate if it's different from the controlled object
    public GameObject rotationCaddy;

    // whether this object is being controlled
    private bool controlOn = false;

    // whether the rotate switch is on or off
    private bool rotateOn = false;

    // Whether to use the alternate vertical axis control
    private bool switchVert = false;

    // The Horizontal control axis *
    //* note that you can set this up in Project->Input Settings
    public string horizontalAxis = "Horizontal";

    // The Vertical axis
    //* note that you can set this up in Project->Input Settings
    public string verticalAxis = "Vertical";

	// Update is called once per frame
	void Update () {

        // Monitor for each type of control keys
        foreach (KeyCode keyCode in controlsKeys)
        {
            if (Input.GetKeyDown(keyCode) && !controlOn)
            {
                controlOn = true;
                break;
            }
            if (Input.GetKeyDown(keyCode) && controlOn)
                controlOn = false;
        }

        foreach (KeyCode keyCode in rotateKeys)
        {
            if (Input.GetKeyDown(keyCode))
            {
                rotateOn = true;
                break;
            }
            if (Input.GetKeyUp(keyCode))
                rotateOn = false;
        }
        foreach (KeyCode keyCode in vertSwitch)
        {
            if (Input.GetKeyDown(keyCode))
            {
                switchVert = true;
                break;
            }
            if (Input.GetKeyUp(keyCode))
                switchVert = false;
        }
    }

    private void LateUpdate()
    {
        // if control is On, then apply any joystick movements accordingly
        if (!controlOn)
            return;

        // If the object to be controlled doesn't exist then stop here
        if (!controlled)
            return;

        // get the input from the horizontal & vertical axis
        Vector2 stickInput = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));

        // if the rotate button is down, rotate
        if (rotateOn)
            if (rotationCaddy != null)
            {
                if (switchVert)
                    rotationCaddy.transform.Rotate(new Vector3(
                        0,
                        stickInput.x * rotationSpeed * Time.deltaTime,
                        stickInput.y * rotationSpeed * Time.deltaTime
                        ));
                else
                    rotationCaddy.transform.Rotate(new Vector3(
                        stickInput.y * rotationSpeed * Time.deltaTime,
                        stickInput.x * rotationSpeed * Time.deltaTime,
                        0));
            }
            else
            {
                if (switchVert)
                    controlled.transform.Rotate(new Vector3(
                        0,
                        stickInput.x * rotationSpeed * Time.deltaTime,
                        stickInput.y * rotationSpeed * Time.deltaTime));
                else
                    controlled.transform.Rotate(new Vector3(
                        stickInput.y * rotationSpeed * Time.deltaTime,
                        stickInput.x * rotationSpeed * Time.deltaTime,
                        0));
            }
        else
        {
            // otherwise, translate according to input
            if (switchVert)
                controlled.transform.Translate(new Vector3(
                    stickInput.x * speed * Time.deltaTime,
                    stickInput.y * speed * Time.deltaTime,
                    0
                    ));
            else
                controlled.transform.Translate(new Vector3(
                    stickInput.x * speed * Time.deltaTime,
                    0,
                    stickInput.y * speed * Time.deltaTime
                    ));
        }
    }
}
