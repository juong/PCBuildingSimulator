using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public bool handFree = true;
    public GameObject item;
    public Transform guide;
    public GameObject MB;
    GameObject parts;

    public float distance;
    float maxdist;
    
    private GameObject placedItem; //only used if there we are pulling an item off

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "item" || col.gameObject.tag == "MB")
            if (!item)
                item = col.gameObject;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "item" || col.gameObject.tag == "MB")
        {
            if (handFree)
                item = null;
        }
    }

    void Start () {
        
    }
	
	void Update () {
        if (item && (item.name.StartsWith("mb") || item.name.StartsWith("hdd") || item.name.StartsWith("per") || item.name.StartsWith("psu")))
            maxdist = 0.6f;
        else
            maxdist = 0.3f;
        if (Input.GetMouseButtonDown(0))
        {
            if (!handFree)
                if (distance < maxdist && (!item.GetComponent<PartProperties>().isMB))
                    place();
                else
                    drop();
            else
                pickup();
        }

        if (!handFree && item) {
            item.transform.position = guide.position;
            if(!item.GetComponent<PartProperties>().isMB)
                distance = Vector3.Distance(item.transform.position, item.GetComponent<PartProperties>().target.transform.position);
        }
    }

    private void pickup()
    {
        if (item)
        {
            if (item.GetComponent<PartProperties>().placed)
            {
                placedItem = item;
                item.GetComponent<PartProperties>().duplicate();
                item = item.GetComponent<PartProperties>().dupe;
            }
            item.GetComponent<Collider>().isTrigger = true;
            //Set the object parent to the guide empty object.
            item.transform.SetParent(guide);
            //Set gravity to false while holding item
            item.GetComponent<Rigidbody>().useGravity = false;
            //Re-position the ball on the guide object 
            item.transform.position = guide.position;
            handFree = false;
        }
    }

    private void drop()
    {
        if (item)
        {
            item.GetComponent<Collider>().isTrigger = false;
            //Set the Gravity to true again.
            item.GetComponent<Rigidbody>().useGravity = true;
            //Item is now no longer useful
            //item = null;
            //Unparent our ball
            guide.GetChild(0).parent = null;
            handFree = true;
        }
    }

    private void place()
    {
        if (item && (item.name.StartsWith("mb") || MB))
        {

            if (item.name.StartsWith("ram") || item.name.StartsWith("video_card"))
            {
                sort(item.name);
            }
            item.GetComponent<PartProperties>().target.SetActive(true);
            if (item.name.StartsWith("mb"))
            {
                MB = item.GetComponent<PartProperties>().target.transform.GetChild(0).gameObject;
            }
            Destroy(item);
            item = null;
            handFree = true;
        }
    }

    private void sort(string type)
    {
        bool targetFound = false;
        if (type.StartsWith("r"))
        {
            for(int i = 0; i < 4; i++)
            {
                if(!MB.transform.GetChild(1).GetChild(i + 4).gameObject.activeSelf && !targetFound)
                {
                    item.GetComponent<PartProperties>().target = MB.transform.GetChild(1).GetChild(i + 4).gameObject;
                    targetFound = true;
                }
            }
        }
        if (type.StartsWith("v"))
        {
            for (int i = 0; i < 4; i++)
            {
                if (!MB.transform.GetChild(1).GetChild(i + 8).gameObject.activeSelf && !targetFound)
                {
                    item.GetComponent<PartProperties>().target = MB.transform.GetChild(1).GetChild(i + 8).gameObject;
                    targetFound = true;
                }
            }
        }
    }

    /*TBA
     * add ref to mb
     * query when special parts are added
     */
}
