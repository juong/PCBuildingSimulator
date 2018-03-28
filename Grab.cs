using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public bool handFree = true;
    public GameObject item;
    public Transform guide;
    public GameObject MB;
    GameObject parts;

    public float distance;
    float maxdist;
    public bool mbEmpty;
    bool removable = true;

    private GameObject placedItem; //only used if there we are pulling an item off

    void OnTriggerEnter(Collider col)
    {
        if(MB)
            scanForParts();
        //if ((col.name.StartsWith("p") || col.name.StartsWith("h")) && handFree)
        //    checkPartForScrews();
        if (col.gameObject.tag == "item" || (col.gameObject.tag == "MB" && mbEmpty))
            if (!item)
                item = col.gameObject;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "item")
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

    void Start()
    {

    }

    void Update()
    {
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


        if (!handFree && item)
        {
            item.transform.position = guide.position;
            if (!item.GetComponent<PartProperties>().isMB)
                distance = Vector3.Distance(item.transform.position, item.GetComponent<PartProperties>().target.transform.position);
        }
    }

    private void pickup()
    {
        if(item && item.GetComponent<PartProperties>().placed && (item.name.StartsWith("p") || item.name.StartsWith("h")))
        {
            checkPartForScrews();
        }
        if (item && ((removable && item.tag == "item") || (mbEmpty && item.name.StartsWith("mb"))))
        {
            if (item.GetComponent<PartProperties>().placed)
            {
                placedItem = item;
                item = Instantiate(item.GetComponent<PartProperties>().dupe);
                if (item.name.StartsWith("ram") || item.name.StartsWith("video_card"))
                {
                    sort(item.name);
                    placedItem.SetActive(false);
                }
                item.GetComponent<PartProperties>().target.SetActive(false);
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
            //Unparent our ball
            guide.GetChild(0).parent = null;
            handFree = true;
        }
    }

    private void place()
    {
        if (item && (item.name.StartsWith("mb") || MB || (item.name.StartsWith("p") || (item.name.StartsWith("h")))))
        {

            if (item.name.StartsWith("ram") || item.name.StartsWith("video_card"))
            {
                sort(item.name);
            }
            item.GetComponent<PartProperties>().target.SetActive(true);
            if (item.name.StartsWith("mb") && !MB)
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
            for (int i = 0; i < 4; i++)
            {
                if (!MB.transform.GetChild(1).GetChild(i + 4).gameObject.activeSelf && !targetFound)
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

    private void scanForParts()
    {
        mbEmpty = true;
        for (int i = 0; i < MB.transform.GetChild(1).childCount; i++)
        {
            if (MB.transform.GetChild(1).GetChild(i).gameObject.activeSelf && MB.transform.GetChild(1).GetChild(i).gameObject.tag == "item")
            {
                mbEmpty = false;
            }
        }
        if (mbEmpty)
        {
            for (int i = 0; i < MB.transform.GetChild(2).childCount; i++)
            {
                if (!MB.transform.GetChild(2).GetChild(i).GetChild(1).gameObject.activeSelf)
                {
                    mbEmpty = false;
                }
            }
        }
    }

    private void checkPartForScrews()
    {
        removable = true;
        for (int i = 0; i < item.transform.GetChild(0).childCount; i++)
        {
            if (!item.transform.GetChild(0).GetChild(i).GetChild(1).gameObject.activeSelf)
            {
                removable = false;
            }
        }
    }

    /* NOTES
    * any boards that arent preceded by "mini" in their fitment can be assumed to have 4 RAM and 4 vc, making 12(0-11)children in all
    * any mini boards will have only one vc and 2 RAM, making 7(0-6)children
    */
}