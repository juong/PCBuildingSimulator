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
    GameObject caseParent;
    GameObject tempItem;
    Transform tO;

    public float distance;
    float maxdist;
    public bool mbEmpty;
    bool removable = true;

    private GameObject placedItem; //only used if we are pulling an item off

    void OnTriggerEnter(Collider col)
    {
        
        if(MB)
            scanForParts();
        if (col.gameObject.tag == "item" || (col.gameObject.tag == "MB" && mbEmpty))
            if (!item)
                item = col.gameObject;
        if(item && item.GetComponent<PartProperties>().placed)
            checkPartForScrews();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "item")
            if (!item)
                item = col.gameObject;
        if(item && item.GetComponent<PartProperties>().placed)
            checkPartForScrews();
    }

    void OnTriggerExit(Collider col)
    {
        removable = true;
        if (col.gameObject.tag == "item" || col.gameObject.tag == "MB")
        {
            if (handFree)
                item = null;
        }
    }

    void Start()
    {
        caseParent = GameObject.Find("CaseCover_placed");
        GameObject.Find("CaseCover_placed").SetActive(false);
        tO = GameObject.Find("TEST OBJECTS").transform;
    }

    void Update()
    {
        if (item && (item.name.StartsWith("mb") || item.name.StartsWith("hdd") || item.name.StartsWith("per") || item.name.StartsWith("psu") || item.name.StartsWith("Case")))
            maxdist = 0.6f;
        else
            maxdist = 0.3f;
        if (Input.GetMouseButtonDown(0))
        {
            if (!handFree)
                if (distance < maxdist)
                    if (item.name != "CaseCover")
                        place();
                    else
                        closeCase();
                else
                    drop();
            else
                pickup();
        }
        if (Input.GetMouseButtonDown(1) && tempItem && item)
        {
			if(handFree)
				if (item.name == "CaseCover_placed")
					openCase();
        }


        if (!handFree && item)
        {
            item.transform.position = guide.position;
            //if (!item.GetComponent<PartProperties>().isMB)
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
            //item.GetComponent<Collider>().enabled = false;
            item.transform.SetParent(guide);
            item.GetComponent<Rigidbody>().useGravity = false;
            if (item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().freezeRotation = true;
            }
            item.transform.position = guide.position;
            handFree = false;
        }
    }

    private void drop()
    {
        if (item)
        {
            item.GetComponent<Collider>().isTrigger = false;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().freezeRotation = false;
            guide.GetChild(0).parent = tO;
            handFree = true;
        }
    }

    private void place()
    {
        if (item && (item.name.StartsWith("mb") || MB || (item.name.StartsWith("p") || (item.name.StartsWith("h")))))
        {
            if(!item.name.StartsWith("mb") && !item.name.StartsWith("ram") && !item.name.StartsWith("v"))
                item.GetComponent<PartProperties>().target.GetComponent<Renderer>().material.color = item.GetComponent<Renderer>().material.color;
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
        /*
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
        */
    }

    private void checkPartForScrews()
    {
        removable = true;
        for (int i = 0; i < item.transform.GetChild(0).childCount; i++)
        {
            if (!item.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.activeSelf)
            {
                removable = false;
            }
        }
    }

    private void closeCase()
    {
        caseParent.GetComponent<Renderer>().material.color = item.GetComponent<Renderer>().material.color;
        caseParent.SetActive(true);
        GameObject.FindGameObjectWithTag("case").GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("case").transform.SetParent(caseParent.transform);
        tempItem = item;
        drop();
        tempItem.transform.position = new Vector3(caseParent.transform.position.x, caseParent.transform.position.y + 0.1f, caseParent.transform.position.z);
        tempItem.transform.rotation = caseParent.transform.rotation;
        tempItem.SetActive(false);
        item = null;
    }

    private void openCase()
    {
        tempItem.GetComponent<Renderer>().material.color = caseParent.GetComponent<Renderer>().material.color;
        GameObject.FindGameObjectWithTag("case").transform.SetParent(null);
        GameObject.FindGameObjectWithTag("case").GetComponent<Collider>().enabled = true;
        caseParent.SetActive(false);
        tempItem.SetActive(true);
        tempItem.transform.position = new Vector3(caseParent.transform.position.x, caseParent.transform.position.y + 0.1f, caseParent.transform.position.z);
        tempItem.transform.rotation = caseParent.transform.rotation;
        item = null;
    }
}