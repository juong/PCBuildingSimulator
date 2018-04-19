using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetScript : MonoBehaviour {

    public GameObject point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
            other.transform.position = point.transform.position;
        if (other.tag == "Player")
            Application.Quit();
    }
}
