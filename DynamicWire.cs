using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWire : MonoBehaviour {

    public GameObject plugPrefab;
    public GameObject wirePrefab;
    GameObject plugChild;
    GameObject wireChild;
    Vector3 center;
    Transform wireEndpoint;

    GameObject monitor;
    GameObject psu;
    GameObject mb;
    public GameObject outlet;
    public GameObject selectedMonitor;

    public AudioClip click;
    AudioSource a;

    void OnTriggerEnter(Collider col)
    {
        if (!outlet && col.tag == "Outlet")
            outlet = col.gameObject;
        if (!selectedMonitor && col.name == "monitor")
            selectedMonitor = col.gameObject;
    }

    void OnTriggerStay(Collider col)
    {
        if (!outlet && col.tag == "Outlet")
            outlet = col.gameObject;
        if (!selectedMonitor && col.name == "monitor")
            selectedMonitor = col.gameObject;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Outlet")
            outlet = null;
        if (col.name == "monitor")
            selectedMonitor = null;
    }


    void Start () {
        monitor = GameObject.Find("monitor");
        psu = GameObject.Find("pc_tower_el_mierde").transform.GetChild(1).gameObject;
        mb = GameObject.Find("pc_tower_el_mierde").transform.GetChild(0).gameObject;
        a = GetComponent<AudioSource>();
    }

    void MonitorToPC()
    {
        if (mb.activeSelf)
        {
            a.PlayOneShot(click);
            wireChild = Instantiate(wirePrefab);
            wireChild.GetComponent<WireProperties>().dynamic = true;
            wireChild.GetComponent<Renderer>().material.color = Color.black;
            wireChild.GetComponent<WireProperties>().start = monitor.transform.GetChild(2).gameObject;
            wireChild.GetComponent<WireProperties>().finish = mb.transform.GetChild(4).gameObject;
            wireChild.name = "DisplayCable";
            wireChild.transform.parent = psu.transform.parent.GetChild(7);
        }
    }

    public void PowerWireToPC()
    {
        if (psu.activeSelf)
        {
            a.PlayOneShot(click);
            wireChild = Instantiate(wirePrefab);
            wireChild.GetComponent<WireProperties>().dynamic = true;
            wireChild.GetComponent<Renderer>().material.color = Color.black;
            wireChild.GetComponent<WireProperties>().start = outlet.transform.GetChild(1).gameObject;
            wireChild.GetComponent<WireProperties>().finish = psu.transform.GetChild(2).gameObject;
            wireChild.name = "PSUPowerCable";
            wireChild.transform.parent = psu.transform.parent.GetChild(7);
        }
    }

    public void PowerWireToMonitor()
    {
        a.PlayOneShot(click);
        wireChild = Instantiate(wirePrefab);
        wireChild.GetComponent<WireProperties>().dynamic = true;
        wireChild.GetComponent<Renderer>().material.color = Color.black;
        wireChild.GetComponent<WireProperties>().start = outlet;
        wireChild.GetComponent<WireProperties>().finish = monitor.transform.GetChild(2).gameObject;
        wireChild.name = "MonPowerCable";
        wireChild.transform.parent = psu.transform.parent.GetChild(7);
    }

    void Update () {
        if (outlet && !selectedMonitor)
        {
            if (Input.GetMouseButtonDown(0))
                PowerWireToPC();
            if (Input.GetMouseButtonDown(1))
                PowerWireToMonitor();
        }
        if (selectedMonitor)
        {
            if (Input.GetMouseButtonDown(0))
                MonitorToPC();
        }
	}
}