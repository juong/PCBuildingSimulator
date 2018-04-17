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
    public bool dynamic;
	
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
            if (dynamic)
            {
                this.transform.position = (finish.transform.position + start.transform.position) / 2;
                transform.LookAt(start.transform);
                transform.localScale = (new Vector3(0.05f, 0.01f, Vector3.Distance(start.transform.position, finish.transform.position)));
                if (Vector3.Distance(start.transform.position, finish.transform.position) > 10)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (!startParent || !finParent)
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
}
