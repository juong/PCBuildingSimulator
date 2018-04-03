using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class SaveLoadData : MonoBehaviour{
    public GameObject prefM, prefCPU, prefCPUF, prefGPU, prefS, prefRAM, prefPSU;
    public static PartCollection partCollection = new PartCollection();
    public void Load(string path)
    {
        GameObject p;
        var oldP = GameObject.FindGameObjectsWithTag("item");
        foreach(GameObject o in oldP)
        {
            GameObject.Destroy(o);
        }
        partCollection = LoadParts(path);
        foreach(PartProperties part in partCollection.parts)
        {
            if(part.partName == "Motherboard")
            {
                Debug.Log("YAY1" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefM, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "CPU")
            {
                Debug.Log("YAY2" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefCPU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "CPUFan")
            {
                Debug.Log("YAY3" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefCPUF, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "GPU")
            {
                Debug.Log("YAY4" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefGPU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "Storage")
            {
                Debug.Log("YAY5" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefS, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "RAM")
            {
                Debug.Log("YAY6" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefRAM, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else
            {
                Debug.Log("Yay7" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefPSU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
        }
    }

    public static void Save(string path, PartCollection parts)
    {
        var p = GameObject.FindObjectsOfType<PartProperties>();
        foreach (PartProperties o in p)
        {
            Vector3 ppos = o.transform.position;
            o.posX = ppos.x;
            o.posY = ppos.y;
            o.posZ = ppos.z;
            Debug.Log(o.partName + " " + o.posX + " " + o.posY + " " + o.posZ);
            partCollection.parts.Add(o);
        }
        SaveParts(path, parts);
        ClearParts();
    }

    public static void AddPartData(PartProperties properties)
    {
        partCollection.parts.Add(properties);
    }

    public static void ClearParts()
    {
        partCollection.parts.Clear();
    }

    private static PartCollection LoadParts(string path)
    {
        StreamReader reader = new StreamReader(path);
        string s = reader.ReadLine();
        while (s != null)
        {
            char[] delimiter = {' '};
            string[] fields = s.Split(delimiter);
            PartProperties p = new PartProperties
            {
                partName = fields[0],
                posX = float.Parse(fields[1]),
                posY = float.Parse(fields[2]),
                posZ = float.Parse(fields[3])
            };
            partCollection.parts.Add(p);
            s = reader.ReadLine();
        }
        return partCollection;
    }

    private static void SaveParts(string path, PartCollection parts)
    {
        StreamWriter writer = new StreamWriter(path);
        foreach(PartProperties p in partCollection.parts)
        {
            string str = p.partName + " " + p.posX + " " + p.posY + " " + p.posZ;
            writer.WriteLine(str);
        }
        writer.Close();
    }
}
