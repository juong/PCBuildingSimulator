using UnityEngine;
using System.IO;
using System.Collections; 

public class SaveLoadData : MonoBehaviour{
    public GameObject prefM;
    public GameObject prefCPU;
    public GameObject prefCPUF;
    public GameObject prefGPU;
    public GameObject prefS;
    public GameObject prefRAM;
    public GameObject prefPSU;
    public GameObject prefMOU;
    public GameObject prefKBD;
    public GameObject prefCD;
    public static PartCollection partCollection = new PartCollection();
    public static ArrayList caseP = new ArrayList();

    //Loading in all game objects from the save text file
    public void Load(string path, string casePath)
    {
        GameObject p;
        GameObject parentp = GameObject.Find("TEST OBJECTS");
        GameObject b = GameObject.Find("pc_tower_el_mierde");
        GameObject c = GameObject.Find("monitor");
        GameObject d = GameObject.Find("CaseCover_placed");
        GameObject e = GameObject.Find("CaseCover");
        var oldP = GameObject.FindGameObjectsWithTag("item");
        foreach(GameObject o in oldP)
        {
            if (!o.transform.IsChildOf(b.transform) && !o.name.Contains("Case") && !o.name.Contains("plug")
                && !o.name.Contains("paint") && !o.transform.IsChildOf(c.transform)
                && !o.name.Contains("Cover") && !o.name.Contains("notepad"))
            {
                GameObject.Destroy(o);
            }
        }
        ArrayList caseProp = new ArrayList();
        StreamReader reader = new StreamReader(casePath);
        string s = reader.ReadLine();
        if (s == "True")
        {
            parentp.transform.Find("CaseCover_placed").gameObject.SetActive(true);
            parentp.transform.Find("CaseCover").gameObject.SetActive(false);
            if (d == null)
            {
                parentp.transform.Find("pc_tower_el_mierde").SetParent(parentp.transform.Find("CaseCover_placed"));
                parentp.transform.Find("CaseCover_placed").Find("pc_tower_el_mierde").GetComponent<MeshCollider>().convex = true;
            }
            s = reader.ReadLine();
            char[] delimiter1 = { ' ' };
            string[] fields = s.Split(delimiter1);
            parentp.transform.Find("CaseCover_placed").position = new Vector3(float.Parse(fields[0]), float.Parse(fields[1]), float.Parse(fields[2]));
        }
        else
        {
            s = reader.ReadLine();
            char[] delimiter1 = { ' ' };
            string[] fields = s.Split(delimiter1);
            parentp.transform.Find("CaseCover_placed").position = new Vector3(float.Parse(fields[0]), float.Parse(fields[1]), float.Parse(fields[2]));
            parentp.transform.Find("CaseCover").gameObject.SetActive(true);
            b.transform.SetParent(parentp.transform);
            parentp.transform.Find("CaseCover").SetParent(parentp.transform);
            parentp.transform.Find("CaseCover_placed").gameObject.SetActive(false);
            s = reader.ReadLine();
            fields = s.Split(delimiter1);
            b.transform.position = new Vector3(float.Parse(fields[0]), float.Parse(fields[1]), float.Parse(fields[2]));
            s = reader.ReadLine();
            fields = s.Split(delimiter1);
            parentp.transform.Find("CaseCover").position = new Vector3(float.Parse(fields[0]), float.Parse(fields[1]), float.Parse(fields[2]));
        }
        s = reader.ReadLine();
        while (s != null)
        {
            char[] delimiter2 = { ' ' };
            string[] fields2 = s.Split(delimiter2);
            PartProperties cp = new PartProperties
            {
                partName = fields2[0]
            };
            if (fields2[1] == "True")
                cp.placed = true;
            else
                cp.placed = false;
            caseProp.Add(cp);
            s = reader.ReadLine();
        }
        reader.Close();
        LoadCaseProp(b.transform, caseProp, 0);
        partCollection = LoadParts(path);
        foreach(PartProperties part in partCollection.parts)
        {
            if (part.partName == "MB")
            {
                p = Instantiate(prefM, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "CPU")
            {
                p = Instantiate(prefCPU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "CPUFan")
            {
                p = Instantiate(prefCPUF, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "GPU")
            {
                p = Instantiate(prefGPU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "Storage")
            {
                p = Instantiate(prefS, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "RAM")
            {
                p = Instantiate(prefRAM, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if(part.partName == "PSU")
            {
                p = Instantiate(prefPSU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if(part.partName == "Keyboard")
            {
                p = Instantiate(prefKBD, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "Mouse")
            {
                p = Instantiate(prefMOU, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
            else if (part.partName == "CDDrive")
            {
                p = Instantiate(prefCD, new Vector3(part.posX, part.posY, part.posZ), Quaternion.Euler(part.rotX, part.rotY, part.rotZ));
                p.transform.parent = parentp.transform;
                p.GetComponent<Renderer>().material.SetColor("_Color", part.c);
            }
        }
        ClearParts();
    }

    //Saving all item objects within the game with type 'PartProperties'
    public static void Save(string path, string casePath, PartCollection parts)
    {
        GameObject parentp = GameObject.Find("TEST OBJECTS");
        GameObject ccase = GameObject.Find("pc_tower_el_mierde");
        GameObject coverp = GameObject.Find("CaseCover_placed");
        GameObject cover = GameObject.Find("CaseCover");
        SaveCaseProp(ccase.transform, 0);
        StreamWriter writer = new StreamWriter(casePath);
        if (coverp != null)
        {
            writer.WriteLine(true);
            writer.WriteLine(coverp.transform.position.x + " " + coverp.transform.position.y + " " + coverp.transform.position.z);
        }
        else
        {
            writer.WriteLine(false);
            writer.WriteLine(parentp.transform.Find("CaseCover_placed").position.x + " " + parentp.transform.Find("CaseCover_placed").position.y + " " + parentp.transform.Find("CaseCover_placed").position.z);
            writer.WriteLine(ccase.transform.position.x + " " + ccase.transform.position.y + " " + ccase.transform.position.z);
            writer.WriteLine(cover.transform.position.x + " " + cover.transform.position.y + " " + cover.transform.position.z);
        }
        foreach (string c in caseP)
        {
            writer.WriteLine(c);
        }
        writer.Close();
        ClearCase();
        var p = GameObject.FindObjectsOfType<PartProperties>();
        foreach (PartProperties o in p)
        {
            Vector3 ppos = o.transform.position;
            o.posX = ppos.x;
            o.posY = ppos.y;
            o.posZ = ppos.z;
            if(o.name != "mb_atx_placed")
                o.c = o.GetComponent<Renderer>().material.GetColor("_Color");
            else
                o.c = o.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_Color");
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

    public static void ClearCase()
    {
        caseP.Clear();
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
                posZ = float.Parse(fields[3]),
                rotX = float.Parse(fields[4]),
                rotY = float.Parse(fields[5]),
                rotZ = float.Parse(fields[6])
            };
            partCollection.parts.Add(p);
            s = reader.ReadLine();
        }
        reader.Close();
        return partCollection;
    }

    //Directly writing data received into text file.
    private static void SaveParts(string path, PartCollection parts)
    {
        StreamWriter writer = new StreamWriter(path);
        foreach(PartProperties p in partCollection.parts)
        {
            string str = p.partName + " " + p.posX + " " + p.posY + " " + p.posZ + " " 
                + p.transform.eulerAngles.x + " " + p.transform.eulerAngles.y + " " + p.transform.eulerAngles.z + " " + p.c;
            writer.WriteLine(str);
        }
        writer.Close();
    }

    //Saves all properties of case to a string array
    private static void SaveCaseProp(Transform child, int index)
    {
        if (child.childCount <= 0)
        {
            return;
        }
        foreach (Transform c in child.transform)
        {
            if (!c.gameObject.name.Contains("Cable"))
            {
                caseP.Add(c.gameObject.name + " " + c.gameObject.activeSelf);
                SaveCaseProp(c, index++);
            }
        }
    }

    //Loads all properties of the case from a text file
    private static void LoadCaseProp(Transform child, ArrayList caseProp, int index)
    {
        if (child.childCount <= 0)
        {
            return;
        }
        foreach (Transform c in child.transform)
        {
            foreach(PartProperties p in caseProp)
            {
                if(c.gameObject.name == p.partName)
                {
                    c.gameObject.SetActive(p.placed);
                    LoadCaseProp(c, caseProp, index++);
                }
            }
        }
    }
}
