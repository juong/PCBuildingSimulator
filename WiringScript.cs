using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiringScript : MonoBehaviour {

    GameObject item;
    public GameObject cable;
    //Color c;
    GameObject start;
    GameObject finish;
    public GameObject wireSource;
    GameObject wire;
    GameObject wireParent;

    Vector3 center;
    bool firstSet = false;
    int tempId;
    public ArrayList index;
    bool dupe = false;
    bool connect = false;

    public AudioClip click;
    AudioSource a;

    void OnTriggerEnter(Collider col)
    {
        if (!item && col.tag == "CableAnchor")
            item = col.gameObject;
        if (!cable && col.name.Contains("CableLoc"))
        {
            if (col.transform.parent.name == "Wires")
            {
                cable = col.gameObject;
                //c = cable.GetComponent<Renderer>().material.color;
                cable.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!item && col.tag == "CableAnchor")
            item = col.gameObject;
        if (!cable && col.name.Contains("CableLoc"))
        {
            if (col.transform.parent.name == "Wires")
            {
                cable = col.gameObject;
                //c = cable.GetComponent<Renderer>().material.color;
                cable.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "CableAnchor")
            item = null;
        if (col.name.Contains("CableLoc"))
        {
            if (col.transform.parent.name == "Wires")
            {
                col.GetComponent<Renderer>().material.color = Color.gray;
                cable = null;
            }
        }
    }

    void DrawLine()
    {
        center = (finish.transform.position + start.transform.position) / 2;
        wire = Instantiate(wireSource, center, wireSource.transform.rotation);
        wire.transform.LookAt(finish.transform);
        wire.transform.localScale = (new Vector3(0.05f, 0.01f, Vector3.Distance(start.transform.position, finish.transform.position)));
        wire.name = start.name + "_to_" + finish.name;
        tempId++;
        wire.GetComponent<WireProperties>().identity = tempId;
        index.Add(wire);
        wire.GetComponent<WireProperties>().parentScript = this;
        wire.GetComponent<WireProperties>().start = this.start;
        wire.GetComponent<WireProperties>().finish = this.finish;
        wire.transform.parent = wireParent.transform;
        start = null;
        finish = null;
    }

    void DisconnectAll(string name)
    {
        GameObject tmp;
        for(int i = 0; i < index.Count; i++)
        {
            tmp = (GameObject)index[i];
            if (tmp.name.Contains(name))
            {
                index.RemoveAt(i);
                tmp.GetComponent<WireProperties>().start.SetActive(true);
                tmp.GetComponent<WireProperties>().finish.SetActive(true);
                Destroy(tmp);
            }
        }
    }
    
    void CheckForDupes(string name1, string name2)
    {
        dupe = false;
        GameObject tmp;
        for (int i = 0; i < index.Count - 1; i++)
        {
            tmp = (GameObject)index[i];
            if (tmp.name == name1 || tmp.name == name2)
            {
                dupe = true;
            }
        }
    }

    void CheckConnectivity()
    {
        connect = false;

        if (item.name.Contains("to_HDD"))
            connect = start.name.Contains("HDD_to") || start.name.Contains("CD_to");
        if (start.name.Contains("to_HDD"))
            connect = item.name.Contains("HDD_to") || item.name.Contains("CD_to");

        if (item.name.Contains("to_all"))
            connect = start.name.Contains("to_PSU");
        if (start.name.Contains("to_all"))
            connect = item.name.Contains("to_PSU");

        if (item.name.Contains("HDD_to_MB"))
            connect = start.name.Contains("MB_to_HDD");
        if (start.name.Contains("HDD_to_MB"))
            connect = item.name.Contains("MB_to_HDD");
        if (item.name.Contains("CD_to_MB"))
            connect = start.name.Contains("MB_to_HDD");
        if (start.name.Contains("CD_to_MB"))
            connect = item.name.Contains("MB_to_HDD");

        if (item.name.Contains("HDD_to_PSU"))
            connect = start.name.Contains("to_all");
        if (start.name.Contains("HDD_to_PSU"))
            connect = item.name.Contains("to_all");
        if (item.name.Contains("CD_to_PSU"))
            connect = start.name.Contains("to_all");
        if (start.name.Contains("CD_to_PSU"))
            connect = item.name.Contains("to_all");



        if (item.name.Substring(1, item.name.Length - 3) == start.name.Substring(1, start.name.Length - 3))
            connect = false;
    }
    

    void Awake () {
        index = new ArrayList();
        a = GetComponent<AudioSource>();
        wireParent = GameObject.FindGameObjectWithTag("case").transform.GetChild(6).gameObject;
	}
	
	void Update () {
        if (!start)
            firstSet = false;
        if (Input.GetMouseButtonDown(0))
        {
            if (item) {
                a.PlayOneShot(click);
                if (firstSet)
                {
                    finish = item;
                    CheckForDupes(start.name + "_to_" + finish.name, finish.name + "_to_" + start.name);
                    CheckConnectivity();
                    if (!dupe && connect && finish.name != start.name)
                    {
                        DrawLine();
                    }
                    else
                    {
                        start = null;
                        finish = null;
                    }
                }
                else
                {
                    firstSet = true;
                    start = item;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (cable)
            {
                a.PlayOneShot(click);
                //DisconnectAll(item.name.Substring(0, 3));
                cable.GetComponent<WireProperties>().RemoveCable();
            }
            start = null;
            finish = null;
        }
	}
}
