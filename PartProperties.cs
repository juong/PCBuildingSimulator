using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartProperties : MonoBehaviour {

    //informational set these on instantiation
    public string partName;         //what kind of part is it? (cpu, cpuFan, vidCard, ram, periph, psu, or hdd)
    public string fitment;      //ATX, NLX, MINI-ATX, if none in particular put "ALL"
    public GameObject target;
    public GameObject dupe;
    public bool dead = false;
    public string specs;

    public bool placed = false;
    public bool isMB = false;
    //keeps track of which slots are taken
    //only used if isMB is true
    bool hasCPU = false;
    bool hasCPUFan = false;
    bool hasVidCard = false;
    bool hasRAM = false;
    bool hasHDD = false;
    bool hasPSU = false;
    bool hasPeriph = false;
    int RAMCount = 0;
    int VCCount = 0;

    private void Start()
    {
        if (this.tag == "MB")
            isMB = true;
    }

    public void duplicate()
    {
        if (!isMB)
        {
            dupe = Instantiate(this.gameObject);
            dupe.GetComponent<PartProperties>().placed = false;
            //this.gameObject.SetActive(false);
        }
    }

}
