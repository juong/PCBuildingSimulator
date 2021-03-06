﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartProperties : MonoBehaviour {

    //informational set these on instantiation
    public string partName;         //what kind of part is it? (cpu, cpuFan, vidCard, ram, periph, psu, or hdd)
    public float posX;
    public float posY;
    public float posZ;
    public string fitment;      //ATX, NLX, MINI-ATX, if none in particular put "ALL"
    public GameObject target;
    public bool dead = false;
    public string specs;

    //do not show on GUI
    public int index;
    /*indexes are as follows:
     * cpu = 1, cpufan = 2, ram = 3, vc = 4, mb = 5, psu = 6, per = 7, hdd = 8
     * 0 is saved for non-placeable stuff or other special cases
     */

    public bool placed = false;
    public GameObject dupe;
    GameObject TO;

    public bool isMB = false;

    private void Awake()
    {
        if (this.tag == "MB")
            isMB = true;
        if (!placed)
        {
            target = GameObject.Find("NullTarget");
            if (index == 5)
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(0).gameObject;
            }
            else if (index < 5 && index != 0)
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(0).transform.GetChild(0).GetChild(1).GetChild(index - 1).gameObject;
            }
            else if(index > 5)
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(index - 5).gameObject;
            }
            if(name == "CaseCover")
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(5).gameObject;
            }
        }
        TO = GameObject.Find("TEST OBJECTS");
    }
    

    void Update()
    {
        if (placed)
        {
            this.GetComponent<Collider>().enabled = GameObject.FindGameObjectWithTag("case").GetComponent<Collider>().enabled;
        }
        if (this.transform.parent == null && (!this.name.Contains("paint") && this.name != "LocalSpawn"))
            this.transform.parent = TO.transform;
    }

}
