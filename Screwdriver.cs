using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour {

    GameObject item;
    float scroll;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "screw")
        {
            if (!item)
            {
                item = col.gameObject;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "screw")
        {
            if (!item)
            {
                item = col.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "screw")
        {
            item = null;
        }
    }

    void Update () {

        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (item && ((item.transform.GetChild(1).gameObject.activeSelf && scroll < 0) || (item.transform.position.y <= item.GetComponent<ScrewProperties>().topDist && scroll > 0))/*&& !(item.transform.position.y >= item.GetComponent<ScrewProperties>().topDist && scroll < 0)*/)
        {
            item.transform.Rotate(0, 5 * Time.deltaTime * scroll, 0);
            item.transform.Translate(0, 0.3f * Time.deltaTime * scroll, 0);
        }
    }
}
