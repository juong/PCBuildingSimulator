using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour {

    public bool fan;
    public bool tv;
    GameObject reserved;
    Color rColor;
	
	void FixedUpdate () {
        if (fan)
        {
            if (!reserved)
            {
                reserved = GameObject.Find("TEST OBJECTS");
                rColor = this.transform.parent.GetChild(2).GetComponent<Light>().color;
            }
            if (reserved.transform.childCount > 20)
                this.transform.parent.GetChild(2).GetComponent<Light>().color = Color.red;
            else
                this.transform.parent.GetChild(2).GetComponent<Light>().color = rColor;
            transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
        if (tv)
        {
            if (!reserved)
                reserved = GameObject.Find("TestButtonBase");
            else
            {
                if (reserved.transform.GetChild(0).GetComponent<Renderer>().material.color == Color.cyan)
                {
                    this.transform.GetChild(0).gameObject.SetActive(false);
                    this.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
	}
}
