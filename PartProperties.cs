using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartProperties : MonoBehaviour {

    //informational set these on instantiation
    public string partName;         //what kind of part is it? (cpu, cpuFan, vidCard, ram, periph, psu, or hdd)
    public string fitment;      //ATX, NLX, MINI-ATX, if none in particular put "ALL"
    public GameObject target;
    public bool dead = false;
    public string specs;

    //do not show on GUI
    public int index;
    /*indexes are as follows:
     * cpu = 1, cpufan = 2, ram = 3, vc = 4, mb = 5, psu = 6, per = 7, hdd = 8
     */

    public bool placed = false;
    public GameObject dupe;

    public bool isMB = false;
    //keeps track of which slots are taken
    //only used if isMB is true
    /*
    bool hasCPU = false;
    bool hasCPUFan = false;
    bool hasVidCard = false;
    bool hasRAM = false;
    bool hasHDD = false;
    bool hasPSU = false;
    bool hasPeriph = false;
    int RAMCount = 0;
    int VCCount = 0;
    */
    private void Awake()
    {
        if (this.tag == "MB")
            isMB = true;
        if (!placed)
        {
            if (index == 5)
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(0).gameObject;
            }
            else if (index < 5)
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(0).transform.GetChild(0).GetChild(1).GetChild(index - 1).gameObject;
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("case").transform.GetChild(index - 5).gameObject;
            }
        }
    }

}
