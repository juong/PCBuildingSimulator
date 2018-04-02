using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour {

    public GameObject item;


    void OnTriggerEnter(Collider col)
    {
        if (!item && (col.tag != "Player" && col.tag != "NotPaintable") && col.GetComponent<Renderer>().material.color != this.transform.GetChild(0).GetComponent<Renderer>().material.color && (!col.name.StartsWith("mb") && !col.name.StartsWith("ram") && !col.name.StartsWith("v")))
        {
            item = col.gameObject;
            this.transform.GetChild(1).GetComponent<Renderer>().material.color = item.GetComponent<Renderer>().material.color;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!item && (col.tag != "Player" && col.tag != "NotPaintable") && col.GetComponent<Renderer>().material.color != this.transform.GetChild(0).GetComponent<Renderer>().material.color && (!col.name.StartsWith("mb") && !col.name.StartsWith("ram") && !col.name.StartsWith("v")))
        {
            item = col.gameObject;
            this.transform.GetChild(1).GetComponent<Renderer>().material.color = item.GetComponent<Renderer>().material.color;
        }
    }

    void OnTriggerExit(Collider col)
    {
        item = null;
    }
	
	
	void Update () {
        if (item)
        {
            if (Input.GetMouseButtonDown(0))
            {
                item.GetComponent<Renderer>().material.color = this.transform.GetChild(0).GetComponent<Renderer>().material.color;
                item = null;
            }else if (Input.GetMouseButtonDown(1))
            {
                this.transform.GetChild(0).GetComponent<Renderer>().material.color = item.GetComponent<Renderer>().material.color;
                item = null;
            }
        }
	}
}
