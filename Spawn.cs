using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public Transform SpawnPoint;
    public GameObject prefabM, prefabCPU, prefabCPUF, prefabGPU, prefabS, prefabRAM;


    public List<IPart> part = new List<IPart>();
    PartFactory pFactory = new PartFactory();

    private void Types()
    {
        part.Add(pFactory.GetPart(PartType.Motherboard));
        part.Add(pFactory.GetPart(PartType.CPU));
        part.Add(pFactory.GetPart(PartType.CPUFan));
        part.Add(pFactory.GetPart(PartType.GPU));
        part.Add(pFactory.GetPart(PartType.Storage));
        part.Add(pFactory.GetPart(PartType.RAM));
    }
	
	//EXPERIMENTAL Spawn Part Menu Method
	public void SpawnPart(){
		Instantiate(prefabM, SpawnPoint.position, Quaternion.identity);
	}
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
            Instantiate(prefabM, SpawnPoint.position, Quaternion.identity);
        else if(Input.GetKeyDown("2"))
        {
            Instantiate(prefabCPU, SpawnPoint.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown("3"))
        {
            Instantiate(prefabCPUF, SpawnPoint.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown("4"))
        {
            Instantiate(prefabGPU, SpawnPoint.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown("5"))
        {
            Instantiate(prefabS, SpawnPoint.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown("6"))
        {
            Instantiate(prefabRAM, SpawnPoint.position, Quaternion.identity);
        }
    }
}



// Partz
public enum PartType
{
    Motherboard,
    CPU,
    CPUFan,
    GPU,
    Storage,
    RAM
}
//The actual Factory
public class PartFactory
{
    public IPart GetPart(PartType partType)
    {
        switch (partType)
        {
            case PartType.Motherboard:
                return new Motherboard();
            case PartType.CPU:
                return new CPU();
            case PartType.CPUFan:
                return new CPUFan();
            case PartType.GPU:
                return new GPU();
            case PartType.Storage:
                return new Storage();
            case PartType.RAM:
                return new RAM();
            default:
                return new GPU();
        }
    }
}

//Interface where parts inherit from
public interface IPart
{
    void Attach();
    void Name();
    void Stats();
    void Spawn();
}

//List of parts
public class Motherboard : IPart
{
    public void Attach() { }
    public void Name() { }
    public void Stats() { }
    public void Spawn() { }
}
public class CPU : IPart
{
    public void Attach() { }
    public void Name() { }
    public void Stats() { }
    public void Spawn() { }
}
public class CPUFan : IPart
{
    public void Attach() { }
    public void Name() { }
    public void Stats() { }
    public void Spawn() { }
}
public class GPU : IPart
{
    public void Attach() { }
    public void Name() { }
    public void Stats() { }
    public void Spawn() { }
}
public class Storage : IPart
{
    public void Attach() { }
    public void Name() { }
    public void Stats() { }
    public void Spawn() { }
}
public class RAM : IPart
{
    public void Attach() { }
    public void Name() { }
    public void Stats() { }
    public void Spawn() { }
}
