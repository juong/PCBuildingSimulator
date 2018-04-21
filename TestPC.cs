using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPC : MonoBehaviour {

    public GameObject tfy;
    GameObject pc;
    GameObject player;
    bool inRange = false;
    bool pcOk = false;
    bool casePowered = false, monitorPowered = false;
    bool hddOk = false, hddPowered = false, hddToMB = false;
    bool cdInOk = false, cdPowered = false, cdToMB = false;
    bool mbPowered = false;
    bool monToPC = false;
    int ramCount = 0;
    int vcCount = 0;
    GameObject testMenu;
    bool checking = false;
    public AudioClip click;
    AudioSource a;

	void Start () {
        pc = GameObject.Find("pc_tower_el_mierde");
        player = GameObject.FindGameObjectWithTag("Player");
        testMenu = GameObject.Find("Simulator Menus").transform.GetChild(1).gameObject;
        a = GetComponent<AudioSource>();
	}
	
	void Update () {
        if(Vector3.Distance(player.transform.position, this.transform.position) < 3)
        {
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.cyan;
            inRange = true;
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
        }
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            a.PlayOneShot(click);
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
        }
        if(inRange && Input.GetKeyUp(KeyCode.F) && !checking)
        {
            checking = true;
            CheckPC();
            if (monToPC && monitorPowered)
            {
                Time.timeScale = 0;
                DisplayResults();
            }
            else
                checking = false;
        }
        if(checking && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            testMenu.SetActive(false);
            checking = false;
        }
	}

    void CheckPC()
    {
        pcOk = false;
        mbPowered = false;
        casePowered = false;
        monitorPowered = false;
        cdPowered = false;
        hddPowered = false;
        cdToMB = false;
        hddToMB = false;
        ramCount = 0;
        vcCount = 0;
        //cases that would cause this to be false
        cdInOk = pc.transform.GetChild(2).gameObject.activeSelf;
        hddOk = pc.transform.GetChild(3).gameObject.activeSelf;
        for(int i = 0; i < 4; i++)
        {
            if (pc.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i+4).gameObject.activeSelf)
                ramCount++;
        }
        for (int i = 0; i < 4; i++)
        {
            if (pc.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i + 8).gameObject.activeSelf)
                vcCount++;
        }

        for(int i = 0; i < pc.transform.GetChild(6).childCount; i++)
        {
            if(!mbPowered)
                mbPowered = (pc.transform.GetChild(6).GetChild(i).name.Contains("MB_to_PSU"));
            if (!cdPowered)
                cdPowered = pc.transform.GetChild(6).GetChild(i).name.Contains("CD_to_PSU");
            if (!hddPowered)
                hddPowered = pc.transform.GetChild(6).GetChild(i).name.Contains("HDD_to_PSU");
            if (!cdToMB)
                cdToMB = pc.transform.GetChild(6).GetChild(i).name.Contains("CD_to_MB");
            if (!hddToMB)
                hddToMB = pc.transform.GetChild(6).GetChild(i).name.Contains("HDD_to_MB");
        }
        for (int i = 0; i < pc.transform.GetChild(7).childCount; i++)
        {
            if (!casePowered)
                casePowered = pc.transform.GetChild(7).GetChild(i).name.Contains("PSUPowerCable");
            if (!monitorPowered)
                monitorPowered = pc.transform.GetChild(7).GetChild(i).name.Contains("MonPowerCable");
            if (!monToPC)
                monToPC = pc.transform.GetChild(7).GetChild(i).name.Contains("DisplayCable");
        }
        //the important stuff
        pcOk = pc.transform.GetChild(0).gameObject.activeSelf && pc.transform.GetChild(1).gameObject.activeSelf
            && pc.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject.activeSelf && pc.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject.activeSelf
            && ramCount > 0 && mbPowered && casePowered;
        hddOk = hddToMB && hddPowered;
        cdInOk = cdToMB && cdPowered;
        //monitorConnected already checked
    }

    void DisplayResults()
    {
        string ok = "";
        testMenu.SetActive(true);
        if (pcOk)
            ok = "OK";
        else
            ok = "FAILED";
        testMenu.transform.GetChild(2).GetComponent<Text>().text = ".MOTHERBOARD CHECK: " + ok;
        if (ramCount > 0)
            ok = "OK";
        else
            ok = "FAILED";
        testMenu.transform.GetChild(3).GetComponent<Text>().text = ".RAM MEMORY CHECK: " + ok;
        if (vcCount > 0)
            ok = "OK";
        else
            ok = "FAILED";
        testMenu.transform.GetChild(4).GetComponent<Text>().text = ".VIDEO CHECK: " + ok;

        if (hddOk && cdInOk)
            ok = "2";
        else if (!hddOk && !cdInOk)
            ok = "0";
        else
            ok = "1";
        testMenu.transform.GetChild(5).GetComponent<Text>().text = ".CONNECTED SATA DEVICES: " + ok;
        if (ok == "2" || (ok == "1" && hddOk))
            ok = "HARD DISK";
        else if (ok == "1" && !hddOk)
            ok = "CD DRIVE";
        else
            ok = "NONE";
        testMenu.transform.GetChild(6).GetComponent<Text>().text = ".PRIMARY SATA DEVICES: " + ok;
        testMenu.transform.GetChild(7).GetComponent<Text>().text = ".MEMORY AVAILABLE: " + (ramCount * 2048) + "MB";
        if (vcCount > 0)
            ok = "EXTERNAL VIDEO CARD";
        else
            ok = "INTERNAL GRAPHICS MEMORY";
        if (!pcOk)
            ok = "NONE";
        testMenu.transform.GetChild(8).GetComponent<Text>().text = ".PRIMARY VIDEO DEVICE: " + ok;
        if (pcOk && monitorPowered && monToPC && casePowered && cdInOk && hddOk && vcCount > 0)
            tfy.SetActive(true);
    }
}
