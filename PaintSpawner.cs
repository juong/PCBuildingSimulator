using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSpawner : MonoBehaviour {

    public GameObject can;
    GameObject storedCan;
    Vector3 spot;
    float spotX;

    public bool randomGenerator;
    Color[] defColors = { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta, Color.gray, Color.white };
    GameObject[] spawned = { null, null, null, null, null, null, null};

	void Start () {
        spot = this.transform.position;
        spotX = this.transform.position.x;
	}

    public void newSet()
    {
        spot.x = spotX;
        for (int j = 0; j < spawned.Length; j++)
        {
            if(spawned[j])
                Destroy(spawned[j]);
        }
        for (int i = 0; i < spawned.Length; i++)
        {
            storedCan = Instantiate(can, spot, this.transform.rotation);
            spawned[i] = storedCan;
            if (randomGenerator)
                storedCan.GetComponent<Renderer>().material.color = Random.ColorHSV(0, 1.0f);
            else
                storedCan.GetComponent<Renderer>().material.color = defColors[i];
            spot.x -= 0.5f;
        }
    }
}
