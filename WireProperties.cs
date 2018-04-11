using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireProperties : MonoBehaviour {


    public GameObject start;
    public GameObject finish;
    public int identity;
    GameObject startParent;
    GameObject finParent;

    public WiringScript parentScript;
	
    public void RemoveCable()
    {
        start.SetActive(true);
        finish.SetActive(true);
        parentScript.index.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

	void Update () {
        if (start && finish)
        {
            if(!startParent || !finParent)
            {
                startParent = start.transform.parent.gameObject;
                finParent = finish.transform.parent.gameObject;
                if (!start.name.Contains("PSU_to") && !start.name.Contains("MB_to_HDD"))
                {
                    start.SetActive(false);
                }
                if (!finish.name.Contains("PSU_to") && !finish.name.Contains("MB_to_HDD"))
                {
                    finish.SetActive(false);
                }

            }
            if (!startParent.activeSelf || !finParent.activeSelf)
            {
                RemoveCable();
            }
        }
	}
}
