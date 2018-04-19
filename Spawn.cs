using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    private int randy;
    public Transform SpawnPoint;
    public GameObject prefabM, prefabCPU, prefabCPUF, prefabGPU, prefabS, prefabRAM, prefabPSU;
    public GameObject prefabMB, prefabCD, prefabKBD, prefabMOU;
    public PartProperties propz;

    private void RNG()
    {
        randy = Random.Range(1, 5);
        if (randy == 1)
        {
            propz.dead = true;
        }
        else
        {
            propz.dead = false;
        }
    }

    public void SpawnM()
    {
        Instantiate(prefabM, SpawnPoint.position, Quaternion.identity);
        propz = prefabCPU.GetComponent<PartProperties>();
        propz.partName = "Motherboard";
        propz.fitment = "All";
        RNG();
    }

    public void SpawnCPU()
    {
        Instantiate(prefabCPU, SpawnPoint.position, Quaternion.identity);
        propz = prefabCPU.GetComponent<PartProperties>();
        propz.partName = "CPU";
        propz.fitment = "All";
        RNG();
    }

    public void SpawnCPUF()
    {
        Instantiate(prefabCPUF, SpawnPoint.position, Quaternion.identity);
        propz = prefabCPUF.GetComponent<PartProperties>();
        propz.partName = "CPUFan";
        propz.fitment = "All";
        RNG();
    }

    public void SpawnGPU()
    {
        Instantiate(prefabGPU, SpawnPoint.position, Quaternion.identity);
        propz = prefabGPU.GetComponent<PartProperties>();
        propz.partName = "GPU";
        propz.fitment = "All";
        RNG();
    }

    public void SpawnStorage()
    {
        Instantiate(prefabS, SpawnPoint.position, Quaternion.identity);
        propz = prefabS.GetComponent<PartProperties>();
        propz.partName = "Storage";
        propz.fitment = "All";
        RNG();
    }
    public void SpawnRAM()
    {
        Instantiate(prefabRAM, SpawnPoint.position, Quaternion.identity);
        propz = prefabRAM.GetComponent<PartProperties>();
        propz.partName = "RAM";
        propz.fitment = "All";
        RNG();
    }
    public void SpawnPSU()
    {
        Instantiate(prefabPSU, SpawnPoint.position, Quaternion.identity);
        propz = prefabPSU.GetComponent<PartProperties>();
        propz.partName = "PSU";
        propz.fitment = "All";
        RNG();
    }
    public void SpawnMB()
    {
        Instantiate(prefabMB, SpawnPoint.position, Quaternion.identity);
        propz = prefabMB.GetComponent<PartProperties>();
        propz.partName = "MB";
        propz.fitment = "All";
        RNG();
    }
    public void SpawnCD()
    {
        Instantiate(prefabCD, SpawnPoint.position, Quaternion.identity);
        propz = prefabCD.GetComponent<PartProperties>();
        propz.partName = "CDDrive";
        propz.fitment = "All";
        RNG();
    }
    public void SpawnKBD()
    {
        Instantiate(prefabKBD, SpawnPoint.position, Quaternion.identity);
        propz = prefabKBD.GetComponent<PartProperties>();
        propz.partName = "Keyboard";
        propz.fitment = "All";
        RNG();
    }
    public void SpawnMOU()
    {
        Instantiate(prefabMOU, SpawnPoint.position, Quaternion.identity);
        propz = prefabMOU.GetComponent<PartProperties>();
        propz.partName = "Mouse";
        propz.fitment = "All";
        RNG();
    }
}