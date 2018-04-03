using UnityEngine;
using System.IO;

public class SaveLoadData : MonoBehaviour{
    public GameObject prefM, prefCPU, prefCPUF, prefGPU, prefS, prefRAM, prefPSU;
    public static PartCollection partCollection = new PartCollection();
    //Loading in all game objects from the save text file
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
                p = Instantiate(prefM, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "CPU")
            {
                p = Instantiate(prefCPU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "CPUFan")
            {
                p = Instantiate(prefCPUF, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "GPU")
            {
                p = Instantiate(prefGPU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "Storage")
            {
                Debug.Log("YAY5" + new Vector3(part.posX, part.posY, part.posZ));
                p = Instantiate(prefS, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else if (part.partName == "RAM")
            {
                p = Instantiate(prefRAM, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
            else
            {
                p = Instantiate(prefPSU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.identity);
            }
        }
    }

    //Saving all item objects within the game with type 'PartProperties'
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

    //Adds parts within the game to the data array
    public static void AddPartData(PartProperties properties)
    {
        partCollection.parts.Add(properties);
    }

    //Clears the arraylist with all game data
    public static void ClearParts()
    {
        partCollection.parts.Clear();
    }

    //Directly reading from text file and returning it to main load method
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

    //Directly writing data received into text file.
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
