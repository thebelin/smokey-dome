using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjectPresence : MonoBehaviour {
    // The key code to activate / deactivate the object
    public KeyCode[] controlsKeys;

    // Whether the active/deactive switch is momentary
    public bool momentary = false;

    // The controlled object
    public GameObject controlled;

    // Whether the controlled object is on
    private bool controlOn = false;

	// Use this for initialization
	void Awake () {
        if (!controlled)
            controlled = gameObject;

        controlled.SetActive(controlOn);
	}

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode keyCode in controlsKeys)
        {
            if (Input.GetKeyDown(keyCode) && !controlOn)
            {
                Debug.Log("Control On");
                controlOn = true;
                if (!momentary)
                    break;
            }

            if (Input.GetKeyDown(keyCode) && controlOn)
            {
                Debug.Log("control off");
                controlOn = false;
            }
                
        }
    }

    private void LateUpdate()
    {
        // If it's supposed to be on and it's off
        if (controlOn && !controlled.activeSelf)
            controlled.SetActive(true);

        // If it's supposed to be off and it's on
        else if (!controlOn & controlled.activeSelf)
            controlled.SetActive(false);
    }
}
