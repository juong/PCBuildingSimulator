using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour {

    GameObject guide;
    public GameObject[] obj;
    public bool isItem;

    int complexity = 50; //has to be larger than size, or it makes weird shapes, higher number causes higher load
    int size = 25;
    GameObject par;

    Vector3 rotationSpeed;

    void Start () {
        guide = GameObject.FindGameObjectWithTag("tool");
        if (!isItem)
        {
            par = guide.transform.GetChild(0).gameObject;
            guide.transform.Translate(0, -size, 0);
            for (int i = 1; i < size * 2; i++)
            {
                //Instantiate(obj[Random.Range(0, obj.Length - 1)], guide.transform.position, Random.rotation);
                for (int j = 0; j < complexity; j++)
                {
                    Instantiate(obj[Random.Range(0, obj.Length - 1)], par.transform.position, Random.rotation);
                    guide.transform.Rotate(0, 360 / complexity + Random.Range(-360 / (complexity / 2), 360 / (complexity / 2)), 0);
                }
                guide.transform.Translate(0, size / (complexity / 2), 0);
                if (guide.transform.position.y <= 0)
                {
                    par.transform.Translate(0, 0, 2 * size / (complexity / 2));
                }
                else
                {
                    par.transform.Translate(0, 0, 2 * -size / (complexity / 2));
                }
            }
        }
        else
        {
            rotationSpeed = new Vector3(Random.Range(0.01f, 1.0f), Random.Range(0.01f, 1.0f), Random.Range(0.01f, 1.0f));
        }
	}
	
	
	void Update () {
        if (isItem)
        {
            this.transform.RotateAround(guide.transform.position, guide.transform.position, 0.1f);
            transform.Rotate(rotationSpeed);
        }
	}
}
