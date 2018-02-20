using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public bool handFree = true;
    public GameObject item;
    public Transform guide;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "item")
            if (!item)
                item = col.gameObject;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "item")
        {
            if (handFree)
                item = null;
        }
    }

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!handFree)
                drop();
            else
                pickup();
        }//mause If

        if (!handFree && item)
            item.transform.position = guide.position;
    }

    private void pickup()
    {
        if (!item)
            return;
        //Set the object parent to the guide empty object.
        item.transform.SetParent(guide);
        //Set gravity to false while holding item
        item.GetComponent<Rigidbody>().useGravity = false;
        //Apply the same rotation the main object has.
        item.transform.localRotation = transform.rotation;
        //Re-position the ball on the guide object 
        item.transform.position = guide.position;
        handFree = false;
    }

    private void drop()
    {
        if (!item)
            return;
        //Set the Gravity to true again.
        item.GetComponent<Rigidbody>().useGravity = true;
        //Item is now no longer useful
        item = null;
        //Unparent our ball
        guide.GetChild(0).parent = null;
        handFree = true;
    }
}
