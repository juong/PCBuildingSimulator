using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    GameObject cam;
	
	void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = this.transform.GetChild(0).gameObject;
	}

    //Camera X and Y controls
    float camX, camY;
    float camSpeedX = 2.0f;
    float camSpeedY = 5.0f;
    //Player X and Z controls
    float plX, plZ;
    float speed;
    float speedDefault = 0.1f;

	void Update () {
        transform.Rotate(0, camY, 0);
        plZ = Input.GetAxis("Vertical") * speed;
        plX = Input.GetAxis("Horizontal") * speed;

        cam.transform.Rotate(camX, 0, 0);
        camY = Input.GetAxis("Mouse X") * camSpeedY;
        camX = -Input.GetAxis("Mouse Y") * camSpeedX;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = speedDefault / 2;
        }
        else
        {
            speed = speedDefault;
        }
	}

    private void FixedUpdate()
    {
        transform.Translate(plX, 0, plZ); 
    }
}
