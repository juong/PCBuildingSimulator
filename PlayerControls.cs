using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    GameObject cam;
    GameObject rig;
    float camHeight;
    float crouchHeight;

    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rig = this.transform.GetChild(0).gameObject;
        cam = rig.transform.GetChild(0).gameObject;
        camHeight = cam.transform.position.y;
        crouchHeight = cam.transform.position.y - 1;
	}

    //debug vars
    //public float testCamRotX;
    //public float tcr;
    public float camPosy;

    //Camera X and Y controls
    float camX, camY;
    float camSpeedX = 2.0f;
    float camSpeedY = 5.0f;
    //Player X and Z controls
    float plX, plZ;
    float speed = 4;
    //float speedDefault = 0.1f;

    float crouchSpeed = 5.5f;

    void Update() {
        //debug
        //testCamRotX = Input.GetAxis("Mouse Y");
        //tcr = cam.transform.localRotation.x;
        //camPosy = cam.transform.localPosition.y;
        //debug

        transform.Rotate(0, camY, 0);
        plZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        plX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        camY = Input.GetAxis("Mouse X") * camSpeedY;
        camX = -Input.GetAxis("Mouse Y") * camSpeedX;

        if((cam.transform.localRotation.x < 0.6 && camX > 0) || (cam.transform.localRotation.x > -0.6 && camX < 0)){
            cam.transform.Rotate(camX, 0, 0);
        }

        
	}

    private void FixedUpdate()
    {
        transform.Translate(plX, 0, plZ);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(cam.transform.position.y > crouchHeight)
            {
                rig.transform.Translate(0, -crouchSpeed * Time.deltaTime, crouchSpeed/2*Time.deltaTime);
            }
        }
        else
        {
            if (cam.transform.position.y < camHeight)
            {
                rig.transform.Translate(0, crouchSpeed * Time.deltaTime, -crouchSpeed / 2 * Time.deltaTime);
            }
        }
    }
}
