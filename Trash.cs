using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "item" && !other.name.StartsWith("Case")) || other.gameObject.tag == "tool" )
        {
            Destroy(other.gameObject);
        }
    }
}
